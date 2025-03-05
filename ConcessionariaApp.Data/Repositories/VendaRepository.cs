using ConcessionariaApp.Application.Interfaces.Repositories;
using ConcessionariaApp.Models;
using Microsoft.EntityFrameworkCore;

namespace ConcessionariaApp.Data.Repositories;

public class VendaRepository : IVendaRepository
{
    private readonly ApplicationDbContext _context;

    public VendaRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Venda>> GetAllAsync()
        => await _context.Vendas
                                .Where(v => !v.IsDeleted)
                                .Include(v => v.Veiculo)
                                .Include(v => v.Concessionaria)
                                .Include(v => v.Cliente)
                                .ToListAsync();

    public async Task<Venda?> GetByIdAsync(int id)
        => await _context.Vendas
            .Where(v => !v.IsDeleted)
            .Include(v => v.Veiculo)
            .Include(v => v.Concessionaria)
            .Include(v => v.Cliente)
            .FirstOrDefaultAsync(v => v.Id == id);

    public async Task<bool> ClientePossuiVendaAsync(string cpfCliente)
        => await _context.Vendas.AnyAsync(v => v.Cliente.CPF == cpfCliente);

    public async Task<Veiculo?> GetVeiculoByIdAsync(int veiculoId)
        => await _context.Veiculos.FindAsync(veiculoId);

    public async Task AddAsync(Venda venda)
    {
        _context.Vendas.Add(venda);
        await _context.SaveChangesAsync();
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var venda = await _context.Vendas.FindAsync(id);
        if (venda == null) return false;

        venda.IsDeleted = true;
        
        await _context.SaveChangesAsync();
        return true;
    }
}
