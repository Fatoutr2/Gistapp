using Gistapp.Data;
using Gistapp.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace Gistapp.Services
{
    public class TaskService : ITaskService
    {
        private readonly ApplicationDbContext _context;

        public TaskService(ApplicationDbContext context)
        {
            _context = context;
        }

        public void AssignTaskToUser(int taskId, int memberId)
        {
            var task = _context.ProjectTask.FirstOrDefault(t => t.TaskId == taskId);
            if (task != null)
            {
                task.AssignedToUserId = memberId;
                _context.SaveChanges();
            }
        }

        public void CreateTasks(ProjectTask task)
        {
            _context.ProjectTask.Add(task);
            _context.SaveChanges();
        }

        public List<ProjectTask> GetAllTasks()
        {
            return _context.ProjectTask.ToList();
        }

        public List<ProjectTask> GetTasksByProject(int projectId)
        {
            return _context.ProjectTask
                           .Where(t => t.TaskId == projectId)
                           .ToList();
        }

        public List<ProjectTask> GetTasksByUser(string userName)
        {
            var user = _context.Users.FirstOrDefault(u => u.UserName == userName);
            if (user == null)
            {
                return new List<ProjectTask>();  // Retourne une liste vide si l'utilisateur n'existe pas
            }

            return _context.ProjectTask
                           .Where(t => t.AssignedToUserId == user.UserId)
                           .ToList();
        }

        public object GetTasksByUser(int userId)
        {
            throw new NotImplementedException();
        }

        public List<Task> GetTasksForUser()
        {
            throw new NotImplementedException();
        }

        public void MarkTaskAsCompleted(int taskId)
        {
            var task = _context.ProjectTask.FirstOrDefault(t => t.TaskId == taskId);
            if (task != null)
            {
                task.IsCompleted = true;
                _context.SaveChanges();
            }
        }
    }
}
