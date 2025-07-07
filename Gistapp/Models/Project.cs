// Définition de l'entité « Project » avec ses propriétés (ex : Id, Name, Progress)

using Gistapp.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace gistapp.Models
{
    public class Project
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("ProjectId")]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }
        public string Description { get; set; }
        [Required]
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        [Required]
        [MaxLength(50)]
        public required string Category { get; set; }
        public virtual ICollection<ProjectTask> Tasks { get; set; } = new List<ProjectTask>();
        //[NotMapped]
        //public List<int> SelectedMemberIds { get; set; }
        //public int CreatedByUserId { get; set; }  // Clé étrangère

        //[ForeignKey("CreatedByUserId")]
        //public ApplicationUser CreatedByUser { get; set; }  // Navigation property





        // Ajoutez d'autres propriétés (dates, description, etc.)
    }
}

