using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gistapp.Models
{
    public class RegisterViewModel
    {
        [Required]
        [Display(Name = "Prénom")]
        public required string FirstName { get; set; }

        [Required]
        [Display(Name = "Nom")]
        public required string LastName { get; set; }
        
        [Required]
        [Display(Name = "Username")]
        public required string UserName { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public required string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Mot de passe")]
        public required string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Confirmez le mot de passe")]
        [Compare("Password", ErrorMessage = "Les mots de passe ne correspondent pas.")]
        public required string ConfirmPassword { get; set; }

        [Required]
        [Display(Name = "Rôle")]
        public UserRole Role { get; set; } = UserRole.Membre_De_Projet; // Valeur par défaut
    }
}
