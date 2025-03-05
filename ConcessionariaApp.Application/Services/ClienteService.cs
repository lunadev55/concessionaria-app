using ConcessionariaApp.Application.Interfaces.Repositories;
using ConcessionariaApp.Application.Interfaces.Services;
using ConcessionariaApp.Models;
using Microsoft.Extensions.Logging;

namespace ConcessionariaApp.Application.Services
{
    public class ClienteService : IClienteService
    {
        private readonly IClienteRepository _clienteRepository;
        private readonly ILogger<ClienteService> _logger;

        public ClienteService(IClienteRepository clienteRepository, ILogger<ClienteService> logger)
        {
            _clienteRepository = clienteRepository;
            _logger = logger;
        }

        public async Task<Cliente> GetClienteByCpf(string cpf)
        {
            _logger.LogInformation("Buscando cliente com CPF: {CPF}", cpf);

            var cliente = await _clienteRepository.ObterPorCPFAsync(cpf);

            if (cliente == null)
            {
                _logger.LogWarning("Nenhum cliente encontrado com o CPF: {CPF}", cpf);
            }
            else
            {
                _logger.LogInformation("Cliente encontrado: {Nome} ({CPF})", cliente.Nome, cliente.CPF);
            }

            return cliente;
        }

        public async Task<Cliente> ValidarOuCadastrarClienteAsync(string nome, string cpf, string telefone)
        {
            _logger.LogInformation("Validando ou cadastrando cliente: Nome={Nome}, CPF={CPF}, Telefone={Telefone}", nome, cpf, telefone);

            var clienteExistente = await _clienteRepository.ObterPorCPFAsync(cpf);

            if (clienteExistente != null)
            {
                _logger.LogWarning("Tentativa de cadastro falhou. Cliente com CPF {CPF} já existe.", cpf);
                throw new InvalidOperationException("Cliente já cadastrado com esse CPF.");
            }

            var novoCliente = new Cliente
            {
                Nome = nome,
                CPF = cpf,
                Telefone = telefone
            };

            await _clienteRepository.AdicionarAsync(novoCliente);
            _logger.LogInformation("Novo cliente cadastrado com sucesso: Nome={Nome}, CPF={CPF}", nome, cpf);

            return novoCliente;
        }
    }
}
