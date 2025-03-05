using ConcessionariaApp.Application.Interfaces.Repositories;
using ConcessionariaApp.Application.Interfaces.Services;
using ConcessionariaApp.Models;
using Microsoft.Extensions.Logging;

namespace ConcessionariaApp.Services;

public class ConcessionariaService : IConcessionariaService
{
    private readonly IConcessionariaRepository _repository;
    private readonly ILogger<ConcessionariaService> _logger;

    public ConcessionariaService(IConcessionariaRepository repository, ILogger<ConcessionariaService> logger)
    {
        _repository = repository;
        _logger = logger;
    }

    public async Task<IEnumerable<Concessionaria>> GetAllAsync()
    {
        _logger.LogInformation("Buscando todas as concession�rias.");
        var result = await _repository.GetAllAsync();
        _logger.LogInformation("Encontradas {Count} concession�rias.", result.Count());
        return result;
    }

    public async Task<Concessionaria?> GetByIdAsync(int id)
    {
        _logger.LogInformation("Buscando concession�ria com ID: {Id}", id);
        var result = await _repository.GetByIdAsync(id);

        if (result == null)
        {
            _logger.LogWarning("Nenhuma concession�ria encontrada com ID: {Id}", id);
        }
        else
        {
            _logger.LogInformation("Concession�ria encontrada: {Nome} (ID: {Id})", result.NomeConcessionaria, id);
        }

        return result;
    }

    public async Task<bool> AddAsync(Concessionaria concessionaria)
    {
        _logger.LogInformation("Adicionando nova concession�ria: {Nome}", concessionaria.NomeConcessionaria);
        var success = await _repository.AddAsync(concessionaria);

        if (success)
        {
            _logger.LogInformation("Concession�ria adicionada com sucesso: {Nome} (ID: {Id})", concessionaria.NomeConcessionaria, concessionaria.Id);
        }
        else
        {
            _logger.LogError("Erro ao adicionar concession�ria: {Nome}", concessionaria.NomeConcessionaria);
        }

        return success;
    }

    public async Task<bool> UpdateAsync(Concessionaria concessionaria)
    {
        _logger.LogInformation("Atualizando concession�ria: {Nome} (ID: {Id})", concessionaria.NomeConcessionaria, concessionaria.Id);
        var success = await _repository.UpdateAsync(concessionaria);

        if (success)
        {
            _logger.LogInformation("Concession�ria atualizada com sucesso: {Nome} (ID: {Id})", concessionaria.NomeConcessionaria, concessionaria.Id);
        }
        else
        {
            _logger.LogError("Erro ao atualizar concession�ria: {Nome} (ID: {Id})", concessionaria.NomeConcessionaria, concessionaria.Id);
        }

        return success;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        _logger.LogInformation("Excluindo concession�ria com ID: {Id}", id);
        var success = await _repository.DeleteAsync(id);

        if (success)
        {
            _logger.LogInformation("Concession�ria exclu�da com sucesso: ID {Id}", id);
        }
        else
        {
            _logger.LogError("Erro ao excluir concession�ria: ID {Id}", id);
        }

        return success;
    }
}
