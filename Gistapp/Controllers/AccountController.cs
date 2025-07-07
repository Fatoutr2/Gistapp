
using Gistapp.Models;
using Gistapp.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Security.Claims;
using System.Threading.Tasks;
using gistapp.Services;
using gistapp.Models;

namespace Gistapp.Controllers
{
    // Ce contrôleur gère à la fois l'inscription, la connexion et la déconnexion
    public class AccountController : Controller
    {
        private readonly IUserService _userService;
        private readonly IProjectService _projectService;
        private readonly ILogger<AccountController> _logger;

        // Injecte IUserService et ILogger<AccountController>
        public AccountController(IUserService userService, IProjectService projectService, ILogger<AccountController> logger)
        {
            _userService = userService;
            _projectService = projectService;
            _logger = logger;
        }

        // --------------------------
        // INSCRIPTION
        // --------------------------

        // GET: /Account/Register
        [HttpGet]
        public IActionResult Register()
        {
            return View(); // Affiche le formulaire d'inscription
        }

        // POST: /Account/Register
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Vérifier si un utilisateur avec cet email existe déjà
                var existingUser = await _userService.FindByEmailAsync(model.Email);
                if (existingUser != null)
                {
                    ModelState.AddModelError("", "Un compte avec cet e-mail existe déjà.");
                    return View(model);
                }

                // Créer un nouvel utilisateur en utilisant le ViewModel.
                // Note : Nous hachons le mot de passe avec BCrypt.
                var user = new ApplicationUser
                {
                    UserName = model.UserName,
                    Email = model.Email,
                    FullName = model.FirstName + " " + model.LastName,
                    PasswordHash = BCrypt.Net.BCrypt.HashPassword(model.Password),
                    Role = model.Role  // Stocke l'enum directement (vérifiez le type de stockage, string ou int)
                };

                bool isCreated = await _userService.CreateUserAsync(user);
                if (isCreated)
                {
                    _logger.LogInformation("Utilisateur enregistré avec succès.");
                    // Après inscription, rediriger vers la page de connexion
                    return RedirectToAction("Login", "Account");
                }
                else
                {
                    _logger.LogWarning("Échec de l'inscription.");
                    ModelState.AddModelError("", "Échec de l'inscription. Veuillez réessayer.");
                }
            }
            return View(model);
        }

        // --------------------------
        // CONNEXION
        // --------------------------

        // GET: /Account/Login
        [HttpGet]
        public IActionResult Login()
        {
            return View(); // Affiche le formulaire de connexion
        }

        

        // POST: /Account/Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                _logger.LogInformation("Tentative de connexion pour : {UserName}", model.UserName);
                var user = await _userService.FindByUserNameAsync(model.UserName);

                if (user == null || !BCrypt.Net.BCrypt.Verify(model.Password, user.PasswordHash))
                {
                    _logger.LogWarning("Échec de connexion pour : {UserName}", model.UserName);
                    ModelState.AddModelError("", "Utilisateur ou mot de passe incorrect.");
                }
                else
                {
                    _logger.LogInformation("Connexion réussie pour : {UserName}", user.UserName);

                    var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, user.Role.ToString())
            };

                    var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimsPrincipal);

                    // Stocker les infos en session
                    HttpContext.Session.SetString("CurrentUser", user.UserName);
                    HttpContext.Session.SetInt32("UserRole", (int)user.Role); // Stocke en tant qu'int pour éviter les erreurs

                    // Redirection selon le rôle
                    if ((int)user.Role == 0)
                    {
                        _logger.LogInformation("Chef : {UserName}", user.UserName);

                        // Récupération des projets (ou liste vide)
                        IEnumerable<Project> projects = await _projectService.GetProjectsAsync()
                                                           ?? Enumerable.Empty<Project>();

                        _logger.LogInformation("Nombre de projets récupérés : {Count}", projects.Count());

                        return View("~/Views/Members/Chef/Index.cshtml", projects);
                        //return RedirectToAction("Index", "chef", projects);
                    }
                    else if ((int)user.Role == 1)
                    {
                        _logger.LogInformation("Membre : {UserName}", user.UserName);
                        return RedirectToAction("Index", "Membre");
                    }
                }
            }
            return View(model);
        }

        // --------------------------
        // DÉCONNEXION
        // --------------------------

        // POST: /Account/Logout
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Logout()
        {
            // Effacer la session pour déconnecter l'utilisateur
            HttpContext.Session.Clear();
            _logger.LogInformation("Déconnexion réussie.");
            return RedirectToAction("Index", "Home");
        }
    }
}

