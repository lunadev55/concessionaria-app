using ConcessionariaApp.Models;

namespace ConcessionariaApp.Application.Interfaces.Repositories;

public interface IVendaRepository
{
    Task<IEnumerable<Venda>> GetAllAsync();
    Task<Venda?> GetByIdAsync(int id);
    Task<bool> ClientePossuiVendaAsync(string cpfCliente);
    Task<Veiculo?> GetVeiculoByIdAsync(int veiculoId);
    Task AddAsync(Venda venda);
    Task<bool> DeleteAsync(int id);
}
