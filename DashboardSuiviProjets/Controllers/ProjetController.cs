using Microsoft.AspNetCore.Mvc;
using DashboardSuiviProjets.Data;
using DashboardSuiviProjets.Models;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace DashboardSuiviProjets.Controllers
{
    public class ProjetController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProjetController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Affiche la liste des projets
        public async Task<IActionResult> Index()
        {
            var projets = await _context.Projets.ToListAsync();
            return View(projets);
        }

        // Ajoute d'autres actions (Création, Modification, Détail, Suppression) selon tes besoins
    }
}
