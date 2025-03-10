using Gistapp.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gistapp.Models
{
    public class ProjectMembers
    {
        public int ProjectId { get; set; }
        [ForeignKey("ProjectId")]
        public required Projects Projects { get; set; }

        public int MemberId { get; set; }
        [ForeignKey("MemberId")]
        public required Members Members { get; set; }

        public required string Role { get; set; } // Rôle du membre dans le projet
    }
}
