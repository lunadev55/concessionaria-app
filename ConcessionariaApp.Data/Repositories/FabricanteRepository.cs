using ConcessionariaApp.Application.Interfaces.Repositories;
using ConcessionariaApp.Models;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace ConcessionariaApp.Data.Repositories;

public class FabricanteRepository : IFabricanteRepository
{
    private readonly ApplicationDbContext _context;

    public FabricanteRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Fabricante>> GetAllAsync()
        => await _context.Fabricantes.Where(f => !f.IsDeleted).ToListAsync();

    public async Task<Fabricante?> GetByIdAsync(int id)
        => await _context.Fabricantes.FindAsync(id);

    public async Task<Fabricante> AddAsync(Fabricante fabricante)
    {
        if (await _context.Fabricantes.AnyAsync(f => f.Nome == fabricante.Nome))
            throw new DuplicateNameException("Um Fabricante com o mesmo nome já existe!");

        _context.Fabricantes.Add(fabricante);
        await _context.SaveChangesAsync();
        return fabricante;
    }

    public async Task<Fabricante> UpdateAsync(Fabricante fabricante)
    {
        var existing = await _context.Fabricantes.FindAsync(fabricante.Id);
        if (existing == null)
            throw new KeyNotFoundException("Fabricante não encontrado!");

        existing.Nome = fabricante.Nome;
        existing.PaisOrigem = fabricante.PaisOrigem;
        existing.AnoFundacao = fabricante.AnoFundacao;
        existing.Website = fabricante.Website;

        await _context.SaveChangesAsync();
        return existing;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var fabricante = await _context.Fabricantes.FindAsync(id);
        if (fabricante == null) return false;
       
        fabricante.IsDeleted = true;
        await _context.SaveChangesAsync();
        return true;
    }
}
