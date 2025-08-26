using CrudCarreras1.Models;
using Microsoft.EntityFrameworkCore;

namespace CrudCarreras1.Datos
{
    public class CarreraRepository : ICarreraRepository
    {
        private readonly AppDbContext _context;
        private readonly DbSet<Carrera> _carreras;

        public CarreraRepository(AppDbContext context)
        {
            _context = context;
            _carreras = context.Set<Carrera>();
            // o directamente: context.Carreras
        }

        //public async Task<IEnumerable<Carrera>> GetAllAsync()
        //{
        //    return await _carreras
        //        .OrderBy(c => c.Nombre)
        //        .ToListAsync();
        //}

        public async Task<IEnumerable<Carrera>> GetAllAsync()
        {
            return await _context.Carreras
                .Include(c => c.Instituto)
                .OrderBy(c => c.Nombre)
                .ToListAsync();
        }



        public Task<Carrera?> GetByIdAsync(int id)
        {
            return _carreras.FindAsync(id).AsTask();
        }

        public async Task AddAsync(Carrera carrera)
        {
            await _carreras.AddAsync(carrera);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Carrera carrera)
        {
            _carreras.Update(carrera);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _carreras.FindAsync(id);
            if (entity == null) return;
            _carreras.Remove(entity);
            await _context.SaveChangesAsync();
        }
    }
}
