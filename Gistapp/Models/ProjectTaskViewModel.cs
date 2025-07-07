// Models/ViewModels/ProjectTaskViewModel.cs

using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Gistapp.Models.ViewModels
{
    public class ProjectTaskViewModel
    {
        // Champs de la tâche
        public int TaskId { get; set; }

        [Required]
        [MaxLength(50)]
        public string Title { get; set; }

        [MaxLength(300)]
        public string Description { get; set; }

        [Required]
        public DateTime DueDate { get; set; }

        [Required]
        public int ProjectId { get; set; }

        public bool IsCompleted { get; set; }

        // Champs supplémentaires
        public List<int> SelectedMemberIds { get; set; } = new(); // Pour la sélection multiple

        public List<SelectListItem> AvailableUsers { get; set; }
        public List<SelectListItem> AvailableProjects { get; set; }
    }
}
