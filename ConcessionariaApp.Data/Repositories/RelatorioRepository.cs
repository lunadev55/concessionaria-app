using ConcessionariaApp.Application.Dtos;
using ConcessionariaApp.Application.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Extensions;

namespace ConcessionariaApp.Data.Repositories;

public class RelatorioRepository : IRelatorioRepository
{
    private readonly ApplicationDbContext _context;

    public RelatorioRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<RelatorioVendasDto>> GerarRelatorioMensalAsync(int ano, int mes)
    {
        var vendas = await _context.Vendas
            .Where(v => v.DataVenda.Year == ano && v.DataVenda.Month == mes)
            .Include(v => v.Veiculo)
            .Include(v => v.Veiculo.Fabricante)
            .Include(v => v.Concessionaria)
            .ToListAsync();

        return vendas
            .GroupBy(v => new { v.Veiculo.Tipo, v.Veiculo.Fabricante.Nome, v.Concessionaria.NomeConcessionaria })
            .Select(g => new RelatorioVendasDto
            {
                TipoVeiculo = g.Key.Tipo.GetDisplayName(),
                Fabricante = g.Key.Nome,
                Concessionaria = g.Key.NomeConcessionaria, // Corrigido o nome duplicado
                TotalVendas = g.Count(),
                ValorTotal = g.Sum(v => v.PrecoVenda)
            })
            .ToList();
    }
}
