// Implémentation de la logique métier qui utilise le repository

using gistapp.Models;
using gistapp.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace gistapp.Services
{
    public class ProjectService : IProjectService
    {
        private readonly IProjectRepository _projectRepository;
        private readonly ILogger<ProjectService> _logger;

        public ProjectService(IProjectRepository projectRepository, ILogger<ProjectService> logger)
        {
            _projectRepository = projectRepository;
            _logger = logger;
        }

        /// Récupère la liste de tous les projets.
        public Task<IEnumerable<Project>> GetProjectsAsync()=>
            _projectRepository.GetAllAsync();

        /*{
            return await _context.Projects.ToListAsync();
        }*/
        
        /// Récupère un projet par son identifiant.
        public Task<Project> GetProjectAsync(int id) =>
            _projectRepository.GetByIdAsync(id);

        /// Ajoute un nouveau projet.
        public Task CreateProjectAsync(Project project) =>
            _projectRepository.AddAsync(project);

        /// Met à jour un projet existant.
        public Task UpdateProjectAsync(Project project) =>
            _projectRepository.UpdateAsync(project);

        /// Supprime un projet par son identifiant.
        public Task DeleteProjectAsync(int id) =>
            _projectRepository.DeleteAsync(id);
    }
}
