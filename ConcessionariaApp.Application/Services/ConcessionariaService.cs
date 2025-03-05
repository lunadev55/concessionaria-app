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
        _logger.LogInformation("Buscando todas as concessionárias.");
        var result = await _repository.GetAllAsync();
        _logger.LogInformation("Encontradas {Count} concessionárias.", result.Count());
        return result;
    }

    public async Task<Concessionaria?> GetByIdAsync(int id)
    {
        _logger.LogInformation("Buscando concessionária com ID: {Id}", id);
        var result = await _repository.GetByIdAsync(id);

        if (result == null)
        {
            _logger.LogWarning("Nenhuma concessionária encontrada com ID: {Id}", id);
        }
        else
        {
            _logger.LogInformation("Concessionária encontrada: {Nome} (ID: {Id})", result.NomeConcessionaria, id);
        }

        return result;
    }

    public async Task<bool> AddAsync(Concessionaria concessionaria)
    {
        _logger.LogInformation("Adicionando nova concessionária: {Nome}", concessionaria.NomeConcessionaria);
        var success = await _repository.AddAsync(concessionaria);

        if (success)
        {
            _logger.LogInformation("Concessionária adicionada com sucesso: {Nome} (ID: {Id})", concessionaria.NomeConcessionaria, concessionaria.Id);
        }
        else
        {
            _logger.LogError("Erro ao adicionar concessionária: {Nome}", concessionaria.NomeConcessionaria);
        }

        return success;
    }

    public async Task<bool> UpdateAsync(Concessionaria concessionaria)
    {
        _logger.LogInformation("Atualizando concessionária: {Nome} (ID: {Id})", concessionaria.NomeConcessionaria, concessionaria.Id);
        var success = await _repository.UpdateAsync(concessionaria);

        if (success)
        {
            _logger.LogInformation("Concessionária atualizada com sucesso: {Nome} (ID: {Id})", concessionaria.NomeConcessionaria, concessionaria.Id);
        }
        else
        {
            _logger.LogError("Erro ao atualizar concessionária: {Nome} (ID: {Id})", concessionaria.NomeConcessionaria, concessionaria.Id);
        }

        return success;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        _logger.LogInformation("Excluindo concessionária com ID: {Id}", id);
        var success = await _repository.DeleteAsync(id);

        if (success)
        {
            _logger.LogInformation("Concessionária excluída com sucesso: ID {Id}", id);
        }
        else
        {
            _logger.LogError("Erro ao excluir concessionária: ID {Id}", id);
        }

        return success;
    }
}
