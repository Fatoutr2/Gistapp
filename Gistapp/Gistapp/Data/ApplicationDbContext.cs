// Classe de contexte pour Entity Framework Core qui gère l’accès à la base de données

using Microsoft.EntityFrameworkCore;
using Gistapp.Models;

namespace Gistapp.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        { }
        public DbSet<Users> Users { get; set; }
        public DbSet<Projects> Projects { get; set; }
        public DbSet<Members> Members { get; set; }
        public DbSet<Tasks> Tasks { get; set; }
        public DbSet<Resources> Resources { get; set; }
        public DbSet<ProjectMembers> ProjectMembers { get; set; }
        public DbSet<ProjectResources> ProjectResources { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Clés composites Many-to-Many
            modelBuilder.Entity<ProjectMembers>()
                .HasKey(pm => new { pm.ProjectId, pm.MemberId });

            modelBuilder.Entity<ProjectResources>()
                .HasKey(pr => new { pr.ProjectId, pr.ResourceId });
        }


        // Ajoutez d'autres DbSet selon vos besoins
    }
}
