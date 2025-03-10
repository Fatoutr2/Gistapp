using Microsoft.AspNetCore.Mvc;
using Gistapp.Models;
using Gistapp.Services;

namespace Gistapp.Controllers
{
    public class DashboardController : Controller
    {
        private readonly IProjectService _projectService;
        private readonly ITaskService _taskService;
        private readonly IMemberService _memberService;

        public DashboardController(IProjectService projectService, ITaskService taskService, IMemberService memberService)
        {
            _projectService = projectService;
            _taskService = taskService;
            _memberService = memberService;
        }

        public IMemberService Get_memberService()
        {
            return _memberService;
        }

        public IActionResult Index(IMemberService _memberService)
        {
            var viewModel = new DashboardViewModel
            {
                Projects = _projectService.GetAllProjects(),
                Tasks = (IEnumerable<Task>)_taskService.GetAllTasks(),
            };

            return View(viewModel);
        }
    }
}
