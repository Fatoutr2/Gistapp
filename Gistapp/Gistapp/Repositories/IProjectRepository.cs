// Interface définissant les opérations (CRUD) sur les projets

using Gistapp.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Gistapp.Repositories
{
    public interface IProjectRepository
    {
        IEnumerable<Projects> GetAll();
        Projects GetById(int id);
        void Add(Projects project);
        void Update(Projects project);
        void Delete(int id);
    }
}
