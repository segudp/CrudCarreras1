using CrudCarreras1.Models;

namespace CrudCarreras1.Datos
{
    public interface ICarreraRepository
    {
        Task<IEnumerable<Carrera>> GetAllAsync();
        Task<Carrera?> GetByIdAsync(int id);
        Task AddAsync(Carrera carrera);
        Task UpdateAsync(Carrera carrera);
        Task DeleteAsync(int id);
    }
}
