using gistapp.Models;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gistapp.Models
{
    public class ProjectTask
    {
        [Key]  // Définit cette propriété comme clé primaire
        public int TaskId { get; set; }

        [Required]

        [MaxLength(50)]
        public required string Title { get; set; }

        [MaxLength(300)]
        public required string Description { get; set; }

        [Required]
        public DateTime DueDate { get; set; }

        [Required]
        public bool IsCompleted { get; set; }  // Indique si la tâche est terminée

        [Required]
        public int AssignedToUserId { get; set; }  // Référence à l'utilisateur assigné
        public int ProjectId { get; set; }

        [ForeignKey("ProjectId")]
        public Project? Project { get; set; }  // retire "required"

        [ForeignKey("AssignedToUserId")]
        public ApplicationUser? AssignedUser { get; set; }  // retire "required"

        [NotMapped]
        public List<string> SelectedMemberIds { get; set; } = new();
        public ICollection<ProjectTaskMember> ProjectTaskMembers { get; set; }

    }
}
