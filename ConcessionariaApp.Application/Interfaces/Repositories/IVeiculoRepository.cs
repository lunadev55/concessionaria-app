using ConcessionariaApp.Models;

namespace ConcessionariaApp.Application.Interfaces.Repositories;

public interface IVeiculoRepository
{
    Task<IEnumerable<Veiculo>> GetAllAsync();
    Task<Veiculo?> GetByIdAsync(int id);
    Task<bool> AddAsync(Veiculo veiculo);
    Task<bool> UpdateAsync(Veiculo veiculo);
    Task<bool> DeleteAsync(int id);
}
