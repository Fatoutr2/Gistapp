// Contrôleur pour gérer la logique autour des projets (liste, création, modification, etc.)
//Gère toutes les opérations sur les projets (affichage de la liste, création, modification, suppression).

using DocumentFormat.OpenXml.Office2010.Excel;
using Gistapp.Models;
using Gistapp.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Gistapp.Controllers
{
    public class ProjectController : Controller
    {
        private readonly IProjectService _projectService;

        public ProjectController(IProjectService projectService)
        {
            _projectService = projectService;
        }

        // Action pour afficher la liste des projets
        public IActionResult Index()
        {
            var projects = _projectService.GetAllProjects();
            return View(projects);
        }

        // Action pour afficher le formulaire de création d’un projet
        public IActionResult Create() => View();

        [HttpPost]
        public IActionResult Create(Projects project)
        {
            if (ModelState.IsValid)
            {
                _projectService.CreateProject(project);
                return RedirectToAction("Index");
            }
            return View(project);
        }

        // Action pour afficher les détails d'un projet
        public IActionResult Details(int id)
        {
            var project = _projectService.GetProjectById(id);
            if (project == null)
            {
                return NotFound(); // Si le projet n'existe pas, retourne une erreur 404
            }
            return View(project);
        }

        // Action pour afficher le formulaire de modification d’un projet
        public IActionResult Edit(int id)
        {
            var project = _projectService.GetProjectById(id);
            if (project == null)
            {
                return NotFound(); // Si le projet n'existe pas, retourne une erreur 404
            }
            return View(project);
        }

        public IProjectService Get_projectService()
        {
            return _projectService;
        }

        [HttpPost]
        public IActionResult Edit(int id, Projects project, IProjectService _ProjectService)
        {
            if (id != project.ProjectId)
            {
                return BadRequest(); // Si l'ID ne correspond pas, retourne une erreur
            }

            if (ModelState.IsValid)
            {
                _ProjectService.UpdateProject(project);
                return RedirectToAction("Index");
            }
            return View(project);
        }
        //Supprimer un projet
        public IActionResult Delete(int id)
        {
            var project = _projectService.GetProjectById(id);
            return project == null ? NotFound() : View(project);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            _projectService.DeleteProject(id);
            return RedirectToAction("Index");
        }
    }
}

