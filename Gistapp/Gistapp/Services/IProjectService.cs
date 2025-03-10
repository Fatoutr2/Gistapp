// Interface pour définir la logique métier liée aux projets

using Gistapp.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Gistapp.Services
{
    public interface IProjectService
    {
        IEnumerable<Projects> GetAllProjects();
        Projects GetProjectById(int id);
        void CreateProject(Projects project);
        void UpdateProject(Projects project);
        void DeleteProject(int id);
    }
}
