// Contrôleur pour gérer la logique autour des projets (liste, création, modification, etc.)
//Gère toutes les opérations sur les projets (affichage de la liste, création, modification, suppression).

using gistapp.Models;
using gistapp.Services;
using Gistapp.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace gistapp.Controllers
{
    public class ProjectController : Controller
    {
        private readonly IProjectService _projectService;
        private readonly ILogger<ProjectController> _logger;
        private readonly ApplicationDbContext _context;

        public ProjectController(IProjectService projectService, ILogger<ProjectController> logger, ApplicationDbContext context)
        {
            _projectService = projectService;
            _logger = logger;
            _context = context;
        }

        // Action pour afficher la liste des projets
        public async Task<IActionResult> Index()
        {
            var projects = await _projectService.GetProjectsAsync();
            _logger.LogInformation("Nombre de projets récupérés : {Count}", projects?.Count() ?? 0);
            _logger.LogInformation("Projets récupérés : {@projects}", projects);
            return View(projects);
        }
        
        // GET: Project/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var project = await _context.Projects
                .Include(p => p.Tasks)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (project == null)
            {
                return NotFound();
            }

            return View(project);
        }

        // GET: Project/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var project = await _context.Projects.FindAsync(id);
            if (project == null)
            {
                return NotFound();
            }
            return View(project);
        }
        public async Task<IActionResult> Tasks(int id)
        {
            var tasks = await _context.ProjectTask
                .Include(t => t.AssignedUser)
                .Where(t => t.ProjectId == id)
                .ToListAsync();

            return View(tasks); // Crée la vue Tasks.cshtml pour lister les tâches
        }
        // GET: Project/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var project = await _context.Projects.FindAsync(id);
            return View(project); // Affiche la vue de confirmation
        }

        // POST: Project/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var project = await _context.Projects.FindAsync(id);
            if (project != null)
            {
                _context.Projects.Remove(project);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction("Index", "Dashboard");
        }



        // POST: Project/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Project updatedProject)
        {
            if (id != updatedProject.Id) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(updatedProject);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.Projects.Any(p => p.Id == updatedProject.Id))
                        return NotFound();
                    else
                        throw;
                }
            }
            return View(updatedProject);
        }

        // Action pour afficher le formulaire de création d’un projet
        /*public IActionResult Create()
        {
            return View();
        }*/

        // Traitement de la soumission du formulaire de création
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Project project)
        {
            if (ModelState.IsValid)
            {
                await _projectService.CreateProjectAsync(project);
                _logger.LogInformation("Projet '{Name}' créé avec succès.", project.Name);
                // Récupération des projets (ou liste vide)
                IEnumerable<Project> projects = await _projectService.GetProjectsAsync()
                                                   ?? Enumerable.Empty<Project>();

                _logger.LogInformation("Nombre de projets récupérés : {Count}", projects.Count());

                return View("~/Views/Members/Chef/Index.cshtml", projects);
            }
            return View(project);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Details(int id, Project updatedProject)
        {
            if (id != updatedProject.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(updatedProject);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index)); // ou Details(id) si tu veux revenir à la page
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.Projects.Any(p => p.Id == updatedProject.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
            }

            return View(updatedProject);
        }


        


        // Ajoutez d'autres actions (Edit, Details, Delete) selon vos besoins
    }
}
