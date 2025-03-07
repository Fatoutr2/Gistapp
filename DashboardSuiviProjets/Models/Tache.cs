using System;

namespace DashboardSuiviProjets.Models
{
    public class Tache
    {
        public int Id { get; set; }
        public required string Nom { get; set; }
        public required string Description { get; set; }
        public DateTime DateEcheance { get; set; }
        public bool EstTerminee { get; set; }

        // Clé étrangère pour Projet
        public int ProjetId { get; set; }
        public required Projet Projet { get; set; }
    }
}
