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
        _logger.LogInformation("Obtendo todos os ve�culos.");
        try
        {
            var veiculos = await _repository.GetAllAsync();
            _logger.LogInformation("Total de ve�culos obtidos: {Count}", veiculos.Count());
            return veiculos;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao obter lista de ve�culos.");
            throw;
        }
    }

    public async Task<Veiculo?> GetByIdAsync(int id)
    {
        _logger.LogInformation("Obtendo ve�culo com ID {Id}.", id);
        try
        {
            var veiculo = await _repository.GetByIdAsync(id);
            if (veiculo == null)
                _logger.LogWarning("Nenhum ve�culo encontrado com ID {Id}.", id);
            else
                _logger.LogInformation("Ve�culo encontrado: {Modelo} ({Ano}).", veiculo.Modelo, veiculo.AnoFabricacao);

            return veiculo;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao obter ve�culo com ID {Id}.", id);
            throw;
        }
    }

    public async Task<bool> AddAsync(Veiculo veiculo)
    {
        _logger.LogInformation("Adicionando novo ve�culo: {Modelo} ({Ano}).", veiculo.Modelo, veiculo.AnoFabricacao);
        try
        {
            var result = await _repository.AddAsync(veiculo);
            if (result)
                _logger.LogInformation("Ve�culo adicionado com sucesso.");
            else
                _logger.LogWarning("Falha ao adicionar ve�culo.");

            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao adicionar ve�culo.");
            throw;
        }
    }

    public async Task<bool> UpdateAsync(Veiculo veiculo)
    {
        _logger.LogInformation("Atualizando ve�culo ID {Id}: {Modelo} ({Ano}).", veiculo.Id, veiculo.Modelo, veiculo.AnoFabricacao);
        try
        {
            var result = await _repository.UpdateAsync(veiculo);
            if (result)
                _logger.LogInformation("Ve�culo atualizado com sucesso.");
            else
                _logger.LogWarning("Falha ao atualizar ve�culo ID {Id}.", veiculo.Id);

            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao atualizar ve�culo ID {Id}.", veiculo.Id);
            throw;
        }
    }

    public async Task<bool> DeleteAsync(int id)
    {
        _logger.LogInformation("Excluindo ve�culo com ID {Id}.", id);
        try
        {
            var result = await _repository.DeleteAsync(id);
            if (result)
                _logger.LogInformation("Ve�culo ID {Id} exclu�do com sucesso.", id);
            else
                _logger.LogWarning("Falha ao excluir ve�culo ID {Id}.", id);

            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao excluir ve�culo ID {Id}.", id);
            throw;
        }
    }
}

