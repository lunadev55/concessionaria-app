using ConcessionariaApp.Application.Interfaces.Repositories;
using ConcessionariaApp.Application.Interfaces.Services;
using ConcessionariaApp.Models;
using Microsoft.Extensions.Logging;

namespace ConcessionariaApp.Services;

public class FabricanteService : IFabricanteService
{
    private readonly IFabricanteRepository _repository;
    private readonly ILogger<FabricanteService> _logger;

    public FabricanteService(IFabricanteRepository repository, ILogger<FabricanteService> logger)
    {
        _repository = repository;
        _logger = logger;
    }

    public async Task<IEnumerable<Fabricante>> GetAllAsync()
    {
        _logger.LogInformation("Buscando todos os fabricantes.");
        var result = await _repository.GetAllAsync();
        _logger.LogInformation("Encontrados {Count} fabricantes.", result.Count());
        return result;
    }

    public async Task<Fabricante?> GetByIdAsync(int id)
    {
        _logger.LogInformation("Buscando fabricante com ID: {Id}", id);
        var result = await _repository.GetByIdAsync(id);

        if (result == null)
        {
            _logger.LogWarning("Nenhum fabricante encontrado com ID: {Id}", id);
        }
        else
        {
            _logger.LogInformation("Fabricante encontrado: {Nome} (ID: {Id})", result.Nome, id);
        }

        return result;
    }

    public async Task<Fabricante> AddAsync(Fabricante fabricante)
    {
        _logger.LogInformation("Adicionando novo fabricante: {Nome}", fabricante.Nome);
        var result = await _repository.AddAsync(fabricante);

        if (result != null)
        {
            _logger.LogInformation("Fabricante adicionado com sucesso: {Nome} (ID: {Id})", result.Nome, result.Id);
        }
        else
        {
            _logger.LogError("Erro ao adicionar fabricante: {Nome}", fabricante.Nome);
        }

        return result;
    }

    public async Task<Fabricante> UpdateAsync(Fabricante fabricante)
    {
        _logger.LogInformation("Atualizando fabricante: {Nome} (ID: {Id})", fabricante.Nome, fabricante.Id);
        var result = await _repository.UpdateAsync(fabricante);

        if (result != null)
        {
            _logger.LogInformation("Fabricante atualizado com sucesso: {Nome} (ID: {Id})", result.Nome, result.Id);
        }
        else
        {
            _logger.LogError("Erro ao atualizar fabricante: {Nome} (ID: {Id})", fabricante.Nome, fabricante.Id);
        }

        return result;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        _logger.LogInformation("Excluindo fabricante com ID: {Id}", id);
        var success = await _repository.DeleteAsync(id);

        if (success)
        {
            _logger.LogInformation("Fabricante excluído com sucesso: ID {Id}", id);
        }
        else
        {
            _logger.LogError("Erro ao excluir fabricante: ID {Id}", id);
        }

        return success;
    }
}

