using System.ComponentModel.DataAnnotations;

namespace Gistapp.Models
{
    public class Users
    {
        [Key]
        public int UserId { get; set; }

        [Required]
        public required string Username { get; set; }

        [Required, EmailAddress]
        public required string Email { get; set; }

        [Required]
        public required string PasswordHash { get; set; } // Hash du mot de passe pour la sécurité
    }
}
