using Gistapp.Models;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Gistapp.Models
{
    public class Tasks
    {
        [Key]
        public int TaskId { get; set; }

        [Required]
        [MaxLength(100)]
        public required string Title { get; set; }

        public required string Description { get; set; }

        public DateTime? DueDate { get; set; }

        [Required]
        public bool IsCompleted { get; set; }

        // Clé étrangère vers Project
        [Required]
        public int ProjectId { get; set; }
        [ForeignKey("ProjectId")]
        public required Projects Projects { get; set; }

        // Clé étrangère vers Member (optionnelle)
        public int? MemberId { get; set; }
        [ForeignKey("MemberId")]
        public required Members Members { get; set; }
    }
}
