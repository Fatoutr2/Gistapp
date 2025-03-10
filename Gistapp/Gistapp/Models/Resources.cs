using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
namespace Gistapp.Models
{
    public class Resources
    {
        [Key]
        public int ResourceId { get; set; }

        [Required]
        [MaxLength(100)]
        public required string Name { get; set; }

        [MaxLength(50)]
        public required string Role { get; set; }

        [EmailAddress]
        public required string Email { get; set; }

        // Relation avec Projects (Many-to-Many)
        public required List<ProjectResources> ProjectResources { get; set; }
    }
}
