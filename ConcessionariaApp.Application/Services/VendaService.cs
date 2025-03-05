using ConcessionariaApp.Application.Interfaces.Repositories;
using ConcessionariaApp.Application.Interfaces.Services;
using ConcessionariaApp.Models;
using Microsoft.Extensions.Logging;

namespace ConcessionariaApp.Services;

public class VendaService : IVendaService
{
    private readonly IVendaRepository _vendaRepository;
    private readonly ILogger<VendaService> _logger;

    public VendaService(IVendaRepository vendaRepository, ILogger<VendaService> logger)
    {
        _vendaRepository = vendaRepository;
        _logger = logger;
    }

    public async Task<IEnumerable<Venda>> GetAllAsync()
    {
        _logger.LogInformation("Obtendo todas as vendas.");
        try
        {
            var vendas = await _vendaRepository.GetAllAsync();
            _logger.LogInformation("Total de vendas obtidas: {Count}", vendas.Count());
            return vendas;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao obter a lista de vendas.");
            throw;
        }
    }

    public async Task<Venda?> GetByIdAsync(int id)
    {
        _logger.LogInformation("Buscando venda com ID {Id}.", id);
        try
        {
            var venda = await _vendaRepository.GetByIdAsync(id);
            if (venda == null)
                _logger.LogWarning("Nenhuma venda encontrada com ID {Id}.", id);
            else
                _logger.LogInformation("Venda encontrada: {Protocolo}.", venda.Protocolo);

            return venda;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao buscar venda com ID {Id}.", id);
            throw;
        }
    }

    public async Task<string?> AddAsync(Venda venda)
    {
        _logger.LogInformation("Iniciando cadastro de nova venda.");

        try
        {
            if (venda.Cliente == null || string.IsNullOrWhiteSpace(venda.Cliente.CPF))
            {
                _logger.LogWarning("Tentativa de cadastro de venda com CPF inválido.");
                return "Cliente inválido ou CPF não informado.";
            }

            if (await _vendaRepository.ClientePossuiVendaAsync(venda.Cliente.CPF))
            {
                _logger.LogWarning("Cliente com CPF {CPF} já possui uma venda registrada.", venda.Cliente.CPF);
                return "CPF do cliente já existe em outra venda.";
            }

            var veiculo = await _vendaRepository.GetVeiculoByIdAsync(venda.VeiculoID);
            if (veiculo == null)
            {
                _logger.LogWarning("Tentativa de venda para um veículo inexistente. Veículo ID: {VeiculoID}", venda.VeiculoID);
                return "Veículo não encontrado.";
            }

            if (venda.PrecoVenda > veiculo.Preco)
            {
                _logger.LogWarning("Tentativa de venda com preço superior ao preço do veículo. Veículo ID: {VeiculoID}", venda.VeiculoID);
                return "Preço de venda inválido ou superior ao preço do veículo.";
            }
            venda.Protocolo = Guid.NewGuid().ToString("N").ToUpper().Substring(0, 20);
            venda.DataVenda = DateTime.UtcNow;

            await _vendaRepository.AddAsync(venda);
            _logger.LogInformation("Venda cadastrada com sucesso. Protocolo: {Protocolo}", venda.Protocolo);
            return null;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao cadastrar venda.");
            throw;
        }
    }

    public async Task<bool> DeleteAsync(int id)
    {
        _logger.LogInformation("Tentando excluir venda com ID {Id}.", id);
        try
        {
            var result = await _vendaRepository.DeleteAsync(id);
            if (result)
                _logger.LogInformation("Venda ID {Id} excluída com sucesso.", id);
            else
                _logger.LogWarning("Falha ao excluir venda ID {Id}.", id);

            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao excluir venda ID {Id}.", id);
            throw;
        }
    }
}
