using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using CrudCarreras1.Models;
using CrudCarreras1.Datos;
using Microsoft.EntityFrameworkCore;

namespace CrudCarreras1.Controllers
{
    public class CarrerasController : Controller
    {
        private readonly ICarreraRepository _repo;
        private readonly AppDbContext _context;

        public CarrerasController(ICarreraRepository repo, AppDbContext context)
        {
            _repo = repo;
            _context = context;
        }

        // GET: Carreras
        public async Task<IActionResult> Index()
        {
            var carreras = await _repo.GetAllAsync();

            var agrupadas = carreras
                .GroupBy(c => c.Instituto.Nombre)
                .OrderBy(g => g.Key) // ordena por nombre de instituto
                .ToList();

            return View(agrupadas);
        }


        // GET: Carreras/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
                return NotFound();

            var carrera = await _context.Carreras
                .Include(c => c.Instituto)
                .FirstOrDefaultAsync(c => c.ID == id);

            if (carrera == null)
                return NotFound();

            return View(carrera);
        }

        // GET: Carreras/Create
        public async Task<IActionResult> Create()
        {
            ViewBag.Institutos = new SelectList(await _context.Institutos.ToListAsync(), "Id", "Nombre");
  
            return View();
        }

        // POST: Carreras/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Carrera carrera)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Institutos = new SelectList(await _context.Institutos.ToListAsync(), "Id", "Nombre");
                return View(carrera);
            }

            await _repo.AddAsync(carrera);
            return RedirectToAction(nameof(Index));
        }

        // GET: Carreras/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();

            var carrera = await _repo.GetByIdAsync(id.Value);
            if (carrera == null)
                return NotFound();

            ViewBag.Institutos = new SelectList(await _context.Institutos.ToListAsync(), "Id", "Nombre", carrera.InstitutoId);
            return View(carrera);
        }

        // POST: Carreras/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Nombre,Cuatrimestres,InstitutoId")] Carrera carrera)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Institutos = new SelectList(await _context.Institutos.ToListAsync(), "Id", "Nombre", carrera.InstitutoId);
                return View(carrera);
            }

            if (id != carrera.ID)
                return BadRequest();

            

            await _repo.UpdateAsync(carrera);
            return RedirectToAction(nameof(Index));
        }

        // GET: Carreras/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();

            var carrera = await _repo.GetByIdAsync(id.Value);
            if (carrera == null)
                return NotFound();

            return View(carrera);
        }

        // POST: Carreras/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _repo.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
