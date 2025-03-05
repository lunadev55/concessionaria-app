using ConcessionariaApp.Application.Interfaces.Repositories;
using ConcessionariaApp.Models;
using Microsoft.Extensions.Logging;

namespace ConcessionariaApp.Services;

public class VeiculoService : IVeiculoService
{
    private readonly IVeiculoRepository _repository;
    private readonly ILogger<VeiculoService> _logger;

    public VeiculoService(IVeiculoRepository repository, ILogger<VeiculoService> logger)
    {
        _repository = repository;
        _logger = logger;
    }

    public async Task<IEnumerable<Veiculo>> GetAllAsync()
    {
        _logger.LogInformation("Obtendo todos os veículos.");
        try
        {
            var veiculos = await _repository.GetAllAsync();
            _logger.LogInformation("Total de veículos obtidos: {Count}", veiculos.Count());
            return veiculos;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao obter lista de veículos.");
            throw;
        }
    }

    public async Task<Veiculo?> GetByIdAsync(int id)
    {
        _logger.LogInformation("Obtendo veículo com ID {Id}.", id);
        try
        {
            var veiculo = await _repository.GetByIdAsync(id);
            if (veiculo == null)
                _logger.LogWarning("Nenhum veículo encontrado com ID {Id}.", id);
            else
                _logger.LogInformation("Veículo encontrado: {Modelo} ({Ano}).", veiculo.Modelo, veiculo.AnoFabricacao);

            return veiculo;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao obter veículo com ID {Id}.", id);
            throw;
        }
    }

    public async Task<bool> AddAsync(Veiculo veiculo)
    {
        _logger.LogInformation("Adicionando novo veículo: {Modelo} ({Ano}).", veiculo.Modelo, veiculo.AnoFabricacao);
        try
        {
            var result = await _repository.AddAsync(veiculo);
            if (result)
                _logger.LogInformation("Veículo adicionado com sucesso.");
            else
                _logger.LogWarning("Falha ao adicionar veículo.");

            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao adicionar veículo.");
            throw;
        }
    }

    public async Task<bool> UpdateAsync(Veiculo veiculo)
    {
        _logger.LogInformation("Atualizando veículo ID {Id}: {Modelo} ({Ano}).", veiculo.Id, veiculo.Modelo, veiculo.AnoFabricacao);
        try
        {
            var result = await _repository.UpdateAsync(veiculo);
            if (result)
                _logger.LogInformation("Veículo atualizado com sucesso.");
            else
                _logger.LogWarning("Falha ao atualizar veículo ID {Id}.", veiculo.Id);

            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao atualizar veículo ID {Id}.", veiculo.Id);
            throw;
        }
    }

    public async Task<bool> DeleteAsync(int id)
    {
        _logger.LogInformation("Excluindo veículo com ID {Id}.", id);
        try
        {
            var result = await _repository.DeleteAsync(id);
            if (result)
                _logger.LogInformation("Veículo ID {Id} excluído com sucesso.", id);
            else
                _logger.LogWarning("Falha ao excluir veículo ID {Id}.", id);

            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao excluir veículo ID {Id}.", id);
            throw;
        }
    }
}

