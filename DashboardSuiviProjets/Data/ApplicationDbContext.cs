using Microsoft.EntityFrameworkCore;
using DashboardSuiviProjets.Models;

namespace DashboardSuiviProjets.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Projet> Projets { get; set; }
        public DbSet<Tache> Taches { get; set; }
        public DbSet<Utilisateur> Utilisateurs { get; set; }
    }
}
