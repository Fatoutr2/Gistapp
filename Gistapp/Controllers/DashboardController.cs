using gistapp.Models;
using gistapp.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Gistapp.Controllers
{
    public class DashboardController : Controller
    {
        private readonly IProjectService _projectService;
        private readonly ILogger<DashboardController> _logger;

        public DashboardController(IProjectService projectService, ILogger<DashboardController> logger)
        {
            _projectService = projectService;
            _logger = logger;
        }

        // GET: /Dashboard/Index
        public async Task<IActionResult> Index()
        {
            var projects = await _projectService.GetProjectsAsync();
            // S'assurer que projects n'est jamais null (par exemple, retourner une liste vide au lieu de null)
            if (projects == null)
            {
                projects = Enumerable.Empty<Project>();
            }
            return View(projects);
        }

        public async Task<IActionResult> AddProject()
        {
            return View("/Views/Project/Create.cshtml");
        }

        // POST: /Account/Register
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> create()
        {
            return View("/Views/Project/Create.cshtml");
        }
        public async Task<IActionResult> Delete()
        {
            return View("/Views/Project/Delete.cshtml");
        }
        public async Task<IActionResult> Create()
        {
            return View("/Views/ProjectTask/Create.cshtml");
        }
        public async Task<IActionResult> Tasks()
        {
            return View("/Views/Project/Tasks.cshtml");
        }

        public async Task<IActionResult> Details(int id)
        {
            var project = await _projectService.GetProjectAsync(id);
            if (project == null)
            {
                _logger.LogWarning("Projet {Id} non trouvé", id);
                return NotFound();
            }
            return View("~/Views/Project/Details.cshtml", project);
        }
    }
}
