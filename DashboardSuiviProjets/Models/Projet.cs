using System;
using System.Collections.Generic;

namespace DashboardSuiviProjets.Models
{
    public class Projet
    {
        public int Id { get; set; }
        public required string Nom { get; set; }
        public required string Description { get; set; }
        public required string Statut { get; set; }

        // Date de début du projet
        public DateTime? DateDebut { get; set; }

        // Date de fin du projet
        public DateTime? DateFin { get; set; }

        // Indicateur si le projet est terminé
        public bool EstTermine { get; set; }

        // Propriétés de relation avec Tache (1 projet peut avoir plusieurs tâches)
        public required List<Tache> Taches { get; set; }
    }
}
