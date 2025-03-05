using ConcessionariaApp.Application.Dtos;

namespace ConcessionariaApp.Application.Interfaces.Repositories;

public interface IRelatorioRepository
{
    Task<IEnumerable<RelatorioVendasDto>> GerarRelatorioMensalAsync(int ano, int mes);
}
