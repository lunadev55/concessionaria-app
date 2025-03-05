﻿using ConcessionariaApp.Models;

namespace ConcessionariaApp.Application.Interfaces.Repositories;

public interface IFabricanteRepository
{
    Task<IEnumerable<Fabricante>> GetAllAsync();
    Task<Fabricante?> GetByIdAsync(int id);
    Task<Fabricante> AddAsync(Fabricante fabricante);
    Task<Fabricante> UpdateAsync(Fabricante fabricante);
    Task<bool> DeleteAsync(int id);
}
