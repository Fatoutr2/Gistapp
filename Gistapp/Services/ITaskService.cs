using Gistapp.Models;
using System.Collections.Generic;

namespace Gistapp.Services
{
    public interface ITaskService
    {
        void AssignTaskToUser(int taskId, int memberId);
        void CreateTasks(ProjectTask task);
        List<ProjectTask> GetAllTasks();
        List<ProjectTask> GetTasksByProject(int projectId);
        List<ProjectTask> GetTasksByUser(string userName);
        object GetTasksByUser(int userId);
        List<Task> GetTasksForUser();
        void MarkTaskAsCompleted(int taskId);
    }
}
