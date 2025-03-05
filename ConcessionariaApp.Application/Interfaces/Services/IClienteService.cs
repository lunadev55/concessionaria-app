using ConcessionariaApp.Models;

namespace ConcessionariaApp.Application.Interfaces.Services
{
    public interface IClienteService
    {
        Task<Cliente> ValidarOuCadastrarClienteAsync(string nome, string cpf, string telefone);
        Task<Cliente> GetClienteByCpf(string cpf);
    }
}
