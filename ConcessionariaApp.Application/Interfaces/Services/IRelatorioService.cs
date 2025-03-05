using ConcessionariaApp.Application.Dtos;

namespace ConcessionariaApp.Application.Interfaces.Services;

public interface IRelatorioService
{
    Task<IEnumerable<RelatorioVendasDto>> GerarRelatorioMensalAsync(int ano, int mes);
}
