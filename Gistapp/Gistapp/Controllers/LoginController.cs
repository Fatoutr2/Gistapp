using Microsoft.AspNetCore.Mvc;
using Gistapp.Models;
using Gistapp.Services;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
namespace Gistapp.Controllers
{
    public class LoginController : Controller
    {
        private readonly IUserService _userService;

        public LoginController(IUserService userService)
        {
            _userService = userService;
        }

        public IActionResult Login() => View();

        [HttpPost]
        public IActionResult Login(string email, string password)
        {
            var user = _userService.Authentication(email, password);
            if (user != null)
                return RedirectToAction("Index", "Home");

            ViewBag.Error = "Email ou mot de passe incorrect.";
            return View();
        }

        public IActionResult Inscription() => View();

        [HttpPost]
        public IActionResult Inscription(string username, string email, string password)
        {
            if (_userService.EmailExists(email))
            {
                ViewBag.Error = "Email déjà utilisé.";
                return View();
            }

            _userService.Inscription(username, email, password);
            return RedirectToAction("Login");
        }
    }
}
