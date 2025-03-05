using ConcessionariaApp.Models;

namespace ConcessionariaApp.Services;

public interface IVeiculoService
{
    Task<IEnumerable<Veiculo>> GetAllAsync();
    Task<Veiculo?> GetByIdAsync(int id);
    Task<bool> AddAsync(Veiculo veiculo);
    Task<bool> UpdateAsync(Veiculo veiculo);
    Task<bool> DeleteAsync(int id);
}
