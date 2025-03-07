using System.ComponentModel.DataAnnotations;

namespace DashboardSuiviProjets.Models
{
    public class Utilisateur
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public required string Nom { get; set; }

        [Required]
        public required string Email { get; set; }

        // Par exemple : "Administrateur", "Chef de projet", "Développeur", etc.
        public required string Role { get; set; }
    }
}
