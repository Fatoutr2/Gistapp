using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
namespace Gistapp.Models
{
    public class Members
    {
        [Key]
        public int MemberId { get; set; }

        [Required]
        [MaxLength(100)]
        public required string FullName { get; set; }

        [Required]
        [EmailAddress]
        public required string Email { get; set; }

        // Relation avec Projects (Many-to-Many)
        public required List<ProjectMembers> ProjectMembers { get; set; }

        // Relation avec Tasks (One-to-Many)
        public required List<Tasks> Tasks { get; set; }
    }
}
