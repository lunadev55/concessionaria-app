using ConcessionariaApp.Application.Interfaces.Repositories;
using ConcessionariaApp.Models;
using Microsoft.EntityFrameworkCore;

namespace ConcessionariaApp.Data.Repositories;

public class ConcessionariaRepository : IConcessionariaRepository
{
    private readonly ApplicationDbContext _context;

    public ConcessionariaRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Concessionaria>> GetAllAsync()
        => await _context.Concessionarias
        .Where(c => !c.IsDeleted)
        .ToListAsync();

    public async Task<Concessionaria?> GetByIdAsync(int id)
        => await _context.Concessionarias
        .Where(c => !c.IsDeleted && c.Id.Equals(id))
        .FirstOrDefaultAsync();

    public async Task<bool> AddAsync(Concessionaria concessionaria)
    {
        if (await _context.Concessionarias.AnyAsync(c => c.NomeConcessionaria == concessionaria.NomeConcessionaria))
            return false;

        _context.Concessionarias.Add(concessionaria);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> UpdateAsync(Concessionaria concessionaria)
    {
        var existing = await _context.Concessionarias.FindAsync(concessionaria.Id);
        if (existing == null) return false;

        existing.NomeConcessionaria = concessionaria.NomeConcessionaria;
        existing.Endereco = concessionaria.Endereco;
        existing.CEP = concessionaria.CEP;
        existing.Telefone = concessionaria.Telefone;
        existing.Email = concessionaria.Email;
        existing.CapacidadeMaximaVeiculos = concessionaria.CapacidadeMaximaVeiculos;

        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var concessionaria = await _context.Concessionarias.FindAsync(id);
        if (concessionaria == null) return false;

        concessionaria.IsDeleted = true;        
        await _context.SaveChangesAsync();
        return true;
    }
}
