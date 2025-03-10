// Implémentation de la logique métier qui utilise le repository

using Gistapp.Models;
using Gistapp.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Gistapp.Services
{
    public class ProjectService : IProjectService
    {
        private readonly IProjectRepository _projectRepository;

        public ProjectService(IProjectRepository projectRepository)
        {
            _projectRepository = projectRepository;
        }

        public IEnumerable<Projects> GetAllProjects() => _projectRepository.GetAll();
        public Projects GetProjectById(int id) => _projectRepository.GetById(id);
        public void CreateProject(Projects project) => _projectRepository.Add(project);
        public void UpdateProject(Projects project) => _projectRepository.Update(project);
        public void DeleteProject(int id) => _projectRepository.Delete(id);
    }
}

