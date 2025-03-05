using ConcessionariaApp.Application.Dtos;
using ConcessionariaApp.Application.Interfaces.Repositories;
using ConcessionariaApp.Application.Interfaces.Services;
using Microsoft.Extensions.Logging;

namespace ConcessionariaApp.Services;

public class RelatorioService : IRelatorioService
{
    private readonly IRelatorioRepository _repository;
    private readonly ILogger<RelatorioService> _logger;

    public RelatorioService(IRelatorioRepository repository, ILogger<RelatorioService> logger)
    {
        _repository = repository;
        _logger = logger;
    }

    public async Task<IEnumerable<RelatorioVendasDto>> GerarRelatorioMensalAsync(int ano, int mes)
    {
        _logger.LogInformation("Iniciando gera��o do relat�rio de vendas para {Mes}/{Ano}.", mes, ano);

        try
        {
            var relatorio = await _repository.GerarRelatorioMensalAsync(ano, mes);

            if (!relatorio.Any())
            {
                _logger.LogWarning("Nenhum dado encontrado para o relat�rio de {Mes}/{Ano}.", mes, ano);
            }
            else
            {
                _logger.LogInformation("Relat�rio gerado com sucesso. Total de registros: {Count}.", relatorio.Count());
            }

            return relatorio;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao gerar relat�rio de vendas para {Mes}/{Ano}.", mes, ano);
            throw;
        }
    }
}

