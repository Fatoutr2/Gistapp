using Microsoft.AspNetCore.Mvc;
using Gistapp.Models;
using Gistapp.Services;
using Microsoft.AspNetCore.Authorization;
using Gistapp.Data;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Gistapp.Controllers
{
    [Authorize] // Seuls les utilisateurs authentifiés peuvent accéder
    public class MembreController : Controller
    {
        private readonly ITaskService _taskService;
        private readonly IUserService _userService;
        private readonly ILogger<AccountController> _logger;
        private readonly ApplicationDbContext _context;

      
        public async Task<IActionResult> MyTasks()
        {
            // 👇 Récupérer l'ID du user connecté
            var userIdStr = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            if (!int.TryParse(userIdStr, out int userId))
            {
                return Unauthorized(); // ou autre gestion d'erreur
            }

            // 👇 Récupérer les tâches associées via la table de liaison
            var tasks = await _context.ProjectTaskMembers
                .Where(ptm => ptm.UserId == userId)
                .Include(ptm => ptm.Task)
                    .ThenInclude(t => t.Project) // si tu veux aussi le nom du projet
                .Select(ptm => ptm.Task)
                .ToListAsync();

            return View(tasks); // 👈 Tu passes la liste à la vue
        }
        public MembreController(ITaskService taskService)
        {
            _taskService = taskService;
        }

        public IActionResult Index()
        {
            var role = HttpContext.Session.GetInt32("UserRole");
            if (role != 1) // Seulement les membres
            {
                return RedirectToAction("AccessDenied", "Account");
            }
            //return View();
            return View("~/Views/Members/Membre/Index.cshtml");
        }
        


        [HttpPost]
        public IActionResult CompleteTask(int taskId)
        {
            _taskService.MarkTaskAsCompleted(taskId);
            return RedirectToAction("Index");
        }
    }
}
