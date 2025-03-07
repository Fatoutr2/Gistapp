using DashboardSuiviProjets.Data;
using DashboardSuiviProjets.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DashboardSuiviProjets.Controllers
{
    public class TacheController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TacheController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Tache
        public async Task<IActionResult> Index()
        {
            return View(await _context.Taches.ToListAsync());
        }

        // GET: Tache/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Tache/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Titre,Description,DateDebut,DateFin,Statut")] Tache tache)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tache);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tache);
        }

        // GET: Tache/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tache = await _context.Taches.FindAsync(id);
            if (tache == null)
            {
                return NotFound();
            }
            return View(tache);
        }

        // POST: Tache/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Titre,Description,DateDebut,DateFin,Statut")] Tache tache)
        {
            if (id != tache.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tache);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.Taches.Any(e => e.Id == tache.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(tache);
        }

        // GET: Tache/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tache = await _context.Taches
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tache == null)
            {
                return NotFound();
            }

            return View(tache);
        }

        // POST: Tache/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tache = await _context.Taches.FindAsync(id);
            _context.Taches.Remove(tache);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
