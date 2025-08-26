using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CrudCarreras1.Models;
using CrudCarreras1.Datos;

namespace CrudCarreras1.Controllers
{
    public class InstitutosController : Controller
    {
        private readonly AppDbContext _context;

        public InstitutosController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Institutos
        public async Task<IActionResult> Index()
        {
            var institutos = await _context.Institutos
                .OrderBy(i => i.Nombre)
                .ToListAsync();

            return View(institutos); // 👈 sin GroupBy
        }



        // GET: Institutos/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Institutos/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Instituto instituto)
        {
            if (!ModelState.IsValid)
                return View(instituto);

            _context.Add(instituto);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: Institutos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();

            var instituto = await _context.Institutos.FindAsync(id);
            if (instituto == null)
                return NotFound();

            return View(instituto);
        }

        // POST: Institutos/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Instituto instituto)
        {
            if (id != instituto.Id)
                return NotFound();

            if (!ModelState.IsValid)
                return View(instituto);

            try
            {
                _context.Update(instituto);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Institutos.Any(e => e.Id == instituto.Id))
                    return NotFound();
                else
                    throw;
            }

            return RedirectToAction(nameof(Index));
        }

        // GET: Institutos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
                return NotFound();

            var instituto = await _context.Institutos
                .Include(i => i.Carreras)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (instituto == null)
                return NotFound();

            return View(instituto);
        }


        // GET: Institutos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();

            var instituto = await _context.Institutos
                .FirstOrDefaultAsync(m => m.Id == id);

            if (instituto == null)
                return NotFound();

            return View(instituto);
        }

        // POST: Institutos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var instituto = await _context.Institutos.FindAsync(id);
            if (instituto == null)
            {
                return NotFound();
            }
            _context.Institutos.Remove(instituto);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
