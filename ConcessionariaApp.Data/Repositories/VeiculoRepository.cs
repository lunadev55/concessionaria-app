using ConcessionariaApp.Application.Interfaces.Repositories;
using ConcessionariaApp.Models;
using Microsoft.EntityFrameworkCore;

namespace ConcessionariaApp.Data.Repositories;

public class VeiculoRepository : IVeiculoRepository
{
    private readonly ApplicationDbContext _context;

    public VeiculoRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Veiculo>> GetAllAsync()
        => await _context.Veiculos.Where(v => !v.IsDeleted).Include(v => v.Fabricante).ToListAsync();

    public async Task<Veiculo?> GetByIdAsync(int id)
        => await _context.Veiculos.Include(v => v.Fabricante).FirstOrDefaultAsync(v => v.Id == id);

    public async Task<bool> AddAsync(Veiculo veiculo)
    {
        if (await _context.Veiculos.AnyAsync(v => v.Modelo == veiculo.Modelo))
            return false;

        _context.Veiculos.Add(veiculo);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> UpdateAsync(Veiculo veiculo)
    {
        var existing = await _context.Veiculos.FindAsync(veiculo.Id);
        if (existing == null) return false;

        existing.Modelo = veiculo.Modelo;
        existing.AnoFabricacao = veiculo.AnoFabricacao;
        existing.Preco = veiculo.Preco;
        existing.Tipo = veiculo.Tipo;
        existing.Descricao = veiculo.Descricao;

        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var veiculo = await _context.Veiculos.FindAsync(id);
        if (veiculo == null) return false;

        veiculo.IsDeleted = true;
        await _context.SaveChangesAsync();
        return true;
    }
}
