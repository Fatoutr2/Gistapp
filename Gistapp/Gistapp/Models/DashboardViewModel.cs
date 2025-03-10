using DocumentFormat.OpenXml.Spreadsheet;
using System.Collections.Generic;
namespace Gistapp.Models
{
    public class DashboardViewModel
    {
        public required IEnumerable<Projects> Projects { get; set; }
        public required IEnumerable<Task> Tasks { get; set; }
        public required IEnumerable<Member> Members { get; set; }

        public static implicit operator DashboardViewModel(DashboardViewModel v)
        {
            throw new NotImplementedException();
        }
    }
}
