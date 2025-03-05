using ConcessionariaApp.Models;

namespace ConcessionariaApp.Application.Interfaces.Services;

public interface IConcessionariaService
{
    Task<IEnumerable<Concessionaria>> GetAllAsync();
    Task<Concessionaria?> GetByIdAsync(int id);
    Task<bool> AddAsync(Concessionaria concessionaria);
    Task<bool> UpdateAsync(Concessionaria concessionaria);
    Task<bool> DeleteAsync(int id);
}
