// Interface pour définir la logique métier liée aux projets

using gistapp.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace gistapp.Services
{
    public interface IProjectService
    {
        Task<IEnumerable<Project>> GetProjectsAsync();
        Task<Project> GetProjectAsync(int id);
        Task CreateProjectAsync(Project project);
        Task UpdateProjectAsync(Project project);
        Task DeleteProjectAsync(int id);
    }
}
