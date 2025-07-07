// Classe de contexte pour Entity Framework Core qui gère l’accès à la base de données

using gistapp.Models;
using Gistapp.Models; // Assurez-vous que le namespace correspond à celui de vos modèles
using Microsoft.EntityFrameworkCore;

namespace Gistapp.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<ApplicationUser> Users { get; set; }

        // DbSets pour vos entités personnalisées
        public DbSet<Project> Projects { get; set; }
        //public DbSet<Resource> Resources { get; set; }
        //public DbSet<Member> Members { get; set; }
        public DbSet<ProjectTaskMember> ProjectTaskMembers { get; set; }

        public DbSet<ProjectTask> ProjectTask { get; set; }
        //public DbSet<ProjectMember> ProjectMembers { get; set; }
        //public DbSet<ProjectResource> ProjectResources { get; set; }

        // Optionnel : Override pour configurer les relations et contraintes personnalisées
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            //builder.Entity<Project>()
    //.HasOne(p => p.CreatedByUser)
    //.WithMany() // ou .WithMany(u => u.ProjectsCreated) si tu veux naviguer depuis l'utilisateur
    //.HasForeignKey(p => p.CreatedByUserId)
    //.OnDelete(DeleteBehavior.Restrict); // ou NoAction
            builder.Entity<ProjectTask>()
    .HasOne(pt => pt.AssignedUser)
    .WithMany()
    .HasForeignKey(pt => pt.AssignedToUserId)
    .OnDelete(DeleteBehavior.Restrict); // ou DeleteBehavior.NoAction
            builder.Entity<ProjectTaskMember>()
        .HasKey(ptm => new { ptm.TaskId, ptm.UserId }); // Clé composite
            // Exemple : configuration de la clé composite pour ProjectMember
            /* builder.Entity<ProjectMember>()
                 .HasKey(pm => new { pm.ProjectId, pm.MemberId });

             // Exemple : configuration de la clé composite pour ProjectResource
             builder.Entity<ProjectResource>()
                 .HasKey(pr => new { pr.ProjectId, pr.ResourceId });*/
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.LogTo(Console.WriteLine, Microsoft.Extensions.Logging.LogLevel.Information);
        }
    }
}

