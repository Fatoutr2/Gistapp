using System.ComponentModel.DataAnnotations.Schema;

namespace Gistapp.Models
{
    public class ProjectTaskMember
    {
        public int Id { get; set; }

        public int TaskId { get; set; }
        public ProjectTask Task { get; set; }

        public int UserId { get; set; }
        public ApplicationUser User { get; set; }
    }
}
