using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gistapp.Models
{
    public enum UserRole
    {
        Chef_De_Projet,
        Membre_De_Projet,
    }
    public class ApplicationUser 
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("UserId")] // Correspond à la colonne UserId dans la base
        public int UserId { get; set; } // Remplace 'Id' par 'UserId'

        [Required]
        [MaxLength(100)]
        [Column("Fullname")]
        public required string FullName { get; set; }  // Ajout du champ Fullname

        [Required]
        [MaxLength(50)]
        [Column("Username")]
        public required string UserName { get; set; } // Username obligatoire

        [Required]
        [MaxLength(100)]
        public required string Email { get; set; }  // Email obligatoire

        public string? PasswordHash { get; set; } // Mot de passe hashé


        [Required]
        public UserRole Role { get; set; } // Utilisation de l'énumération
        public virtual ICollection<ProjectTask> AssignedTasks { get; set; } = new List<ProjectTask>();
        public ICollection<ProjectTaskMember> ProjectTaskMembers { get; set; }

    }
}
