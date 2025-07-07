using System.ComponentModel.DataAnnotations;

namespace Gistapp.Models
{
    public class LoginViewModel
    {

        [Required]
        [Display(Name = "Username")]
        public required string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Mot de passe")]
        public required string Password { get; set; }
        public bool RememberMe { get; set; }
    }
}
