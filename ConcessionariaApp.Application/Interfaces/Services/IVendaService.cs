using ConcessionariaApp.Models;

namespace ConcessionariaApp.Application.Interfaces.Services;

public interface IVendaService
{
    Task<IEnumerable<Venda>> GetAllAsync();
    Task<Venda?> GetByIdAsync(int id);
    Task<string?> AddAsync(Venda venda);
    Task<bool> DeleteAsync(int id);
}
