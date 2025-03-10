// Implémentation concrète de IProjectRepository qui communique avec le DbContext

using Gistapp.Data;
using Gistapp.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Gistapp.Repositories
{
    public class ProjectRepository : IProjectRepository
    {
        private readonly ApplicationDbContext _context;

        public ProjectRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Projects> GetAll() => _context.Projects.ToList();
        public Projects GetById(int id) => _context.Projects.Find(id);
        public void Add(Projects project) { _context.Projects.Add(project); _context.SaveChanges(); }
        public void Update(Projects project) { _context.Projects.Update(project); _context.SaveChanges(); }
        public void Delete(int id)
        {
            var project = _context.Projects.Find(id);
            if (project != null) { _context.Projects.Remove(project); _context.SaveChanges(); }
        }
    }
}
