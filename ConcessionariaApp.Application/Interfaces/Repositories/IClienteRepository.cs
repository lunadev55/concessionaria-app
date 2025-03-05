using ConcessionariaApp.Models;

namespace ConcessionariaApp.Application.Interfaces.Repositories
{
    public interface IClienteRepository
    {
        Task<Cliente> ObterPorCPFAsync(string cpf);
        Task AdicionarAsync(Cliente cliente);        
    }
}
