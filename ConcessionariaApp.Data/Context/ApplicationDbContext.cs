using ConcessionariaApp.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ConcessionariaApp.Data;

public class ApplicationDbContext : IdentityDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    public DbSet<Fabricante> Fabricantes { get; set; }
    public DbSet<Veiculo> Veiculos { get; set; }
    public DbSet<Concessionaria> Concessionarias { get; set; }
    public DbSet<Cliente> Clientes { get; set; }
    public DbSet<Usuario> Usuarios { get; set; }
    public DbSet<Venda> Vendas { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        modelBuilder.Entity<Fabricante>()
            .HasIndex(f => f.Nome)
            .IsUnique();

        modelBuilder.Entity<Cliente>()
            .HasIndex(c => c.CPF)
            .IsUnique();

        modelBuilder.Entity<Veiculo>()
            .HasOne(v => v.Fabricante)
            .WithMany(f => f.Veiculos)
            .HasForeignKey(v => v.FabricanteID)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Venda>()
            .HasOne(v => v.Veiculo)
            .WithMany()
            .HasForeignKey(v => v.VeiculoID)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Venda>()
            .HasOne(v => v.Cliente)
            .WithMany()
            .HasForeignKey(v => v.ClienteID)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Venda>()
            .HasOne(v => v.Concessionaria)
            .WithMany()
            .HasForeignKey(v => v.ConcessionariaID)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Usuario>()
            .HasIndex(u => u.Email)
            .IsUnique();
    }
}
