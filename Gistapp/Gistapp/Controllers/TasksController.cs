using Microsoft.AspNetCore.Mvc;
using Gistapp.Models;
using Gistapp.Services;
using System;

namespace Gistapp.Controllers
{
    public class TasksController : Controller
    {
        private readonly ITaskService _taskService;

        public TasksController(ITaskService taskService)
        {
            _taskService = taskService;
        }

        public IActionResult Index()
        {
            var tasks = _taskService.GetAllTasks();
            return View(tasks); // Correction ici
        }

        public IActionResult Create() => View();

        [HttpPost]
        public IActionResult Create(Task task)
        {
            if (ModelState.IsValid) // Correction ici
            {
                _taskService.CreateTasks(task); // Correction ici
                return RedirectToAction("Index");
            }
            return View(task);
        }
    }
}
