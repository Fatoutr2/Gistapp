using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Gistapp.Models;
using System.Threading.Tasks;
using System.Linq;
using Gistapp.Data;

namespace Gistapp.Controllers
{
    public class ChefController : Controller
    {
        private readonly ILogger<ChefController> _logger;
        private readonly ApplicationDbContext _context;

        public ChefController(ILogger<ChefController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            return View("~/Views/Members/Chef/Index.cshtml");
        }

        // GET: Chef/CreateTask
        public async Task<IActionResult> CreateTask()
        {
            // Récupérer tous les utilisateurs
            var users = await _context.Users
                .Select(u => new { u.UserId, u.UserName })
                .ToListAsync();

            ViewBag.Users = new MultiSelectList(users, "Id", "UserName");

            // Récupérer les projets (si un chef peut en avoir plusieurs)
            ViewBag.Projects = new SelectList(await _context.Projects.ToListAsync(), "Id", "Name");

            return View("~/Views/Members/Chef/CreateTask.cshtml");
        }

        // POST: Chef/CreateTask
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateTask(ProjectTask model)
        {
            if (ModelState.IsValid)
            {
                _context.ProjectTask.Add(model);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            var users = await _context.Users
                .Select(u => new { u.UserId, u.UserName })
                .ToListAsync();

            ViewBag.Users = new MultiSelectList(users, "UserId", "UserName", model.SelectedMemberIds);
            ViewBag.Projects = new SelectList(await _context.Projects.ToListAsync(), "Id", "Name", model.ProjectId);

            return View("~/Views/Members/Chef/CreateTask.cshtml", model);
        }
    }
}
