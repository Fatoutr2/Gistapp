// Interface définissant les opérations (CRUD) sur les projets

using gistapp.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace gistapp.Repositories
{
    public interface IProjectRepository
    {
        Task<IEnumerable<Project>> GetAllAsync();
        Task<Project> GetByIdAsync(int id);
        Task AddAsync(Project project);
        Task UpdateAsync(Project project);
        Task DeleteAsync(int id);
    }
}
