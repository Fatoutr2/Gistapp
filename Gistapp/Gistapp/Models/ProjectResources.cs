using Gistapp.Models;
using System.ComponentModel.DataAnnotations.Schema;
namespace Gistapp.Models
{
    public class ProjectResources
    {
        public int ProjectId { get; set; }
        [ForeignKey("ProjectId")]
        public required Projects Projects { get; set; }

        public int ResourceId { get; set; }
        [ForeignKey("ResourceId")]
        public required Resources Resources { get; set; }

        public int Allocation { get; set; } // % de ressources allouées
    }
}
