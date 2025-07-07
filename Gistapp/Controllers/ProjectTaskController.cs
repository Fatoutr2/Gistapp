using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Gistapp.Models;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Gistapp.Data;
using Gistapp.Models.ViewModels;

namespace Gistapp.Controllers
{
    public class ProjectTaskController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProjectTaskController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: ProjectTask/Create
        [HttpGet]
        public async Task<IActionResult> Create(int projectId)
        {
            var users = await _context.Users
                .Select(u => new SelectListItem
                {
                    Value = u.UserId.ToString(),
                    Text = u.UserName
                }).ToListAsync();

            var projects = await _context.Projects
                .Select(p => new SelectListItem
                {
                    Value = p.Id.ToString(),
                    Text = p.Name
                }).ToListAsync();

            var viewModel = new ProjectTaskViewModel
            {
                ProjectId = projectId,
                AvailableUsers = users,
                AvailableProjects = projects
            };

            return View(viewModel);
        }

        // POST: ProjectTask/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProjectTaskViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.AvailableUsers = await _context.Users
                    .Select(u => new SelectListItem
                    {
                        Value = u.UserId.ToString(),
                        Text = u.UserName
                    }).ToListAsync();

                model.AvailableProjects = await _context.Projects
                    .Select(p => new SelectListItem
                    {
                        Value = p.Id.ToString(),
                        Text = p.Name
                    }).ToListAsync();

                return View(model);
            }

            // Étape 1 : Créer et sauvegarder la tâche
            var newTask = new ProjectTask
            {
                Title = model.Title,
                Description = model.Description,
                DueDate = model.DueDate,
                ProjectId = model.ProjectId,
                IsCompleted = false,
                AssignedToUserId = (int)(model.SelectedMemberIds?.FirstOrDefault())
            };

            _context.ProjectTask.Add(newTask);
            await _context.SaveChangesAsync(); // 👈 Sauvegarde pour obtenir l'ID de la tâche

            // Étape 2 : Ajouter les membres liés à la tâche
            if (model.SelectedMemberIds != null && model.SelectedMemberIds.Any())
            {
                foreach (var memberId in model.SelectedMemberIds)
                {
                    _context.ProjectTaskMembers.Add(new ProjectTaskMember
                    {
                        TaskId = newTask.TaskId,
                        UserId = memberId // 👈 maintenant un int
                    });
                }


                await _context.SaveChangesAsync(); // 👈 Sauvegarde des membres
                if (newTask.TaskId == 0)
                {
                    throw new Exception("Tâche non insérée !");
                }
            }

            TempData["SuccessMessage"] = "Tâche créée avec succès !";
            return RedirectToAction("Index", "Chef");
        }
    }
}
