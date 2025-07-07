

namespace Gistapp.Models
{
    public class MemberDashboardViewModel
    {
        public required List<ProjectTask> Tasks { get; set; }
        public List<Task> ProjectTask { get; internal set; }
        //public required List<ProjectTask> Task { get; set; }
    }
}
