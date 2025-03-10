// Définition de l'entité « Project » avec ses propriétés (ex : Id, Name, Progress)
using Gistapp.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Gistapp.Models
{
    public class Projects
    {
        [Key]
        public int ProjectId { get; set; }

        [Required]
        [MaxLength(100)]
        public required string Name { get; set; }

        public required string Description { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }

        [Required]
        public int Progress { get; set; } // Pourcentage de progression

        // Relation avec Members (Many-to-Many)
        public required List<ProjectMembers> ProjectMembers { get; set; }

        // Relation avec Resources (Many-to-Many)
        public required List<ProjectResources> ProjectResources { get; set; }

        // Relation avec Tasks (One-to-Many)
        public required List<Tasks> Tasks { get; set; }
    }
}
