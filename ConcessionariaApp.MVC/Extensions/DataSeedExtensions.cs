using ConcessionariaApp.Data;
using ConcessionariaApp.Models;
using Microsoft.AspNetCore.Identity;

public static class DataSeedExtensions
{
    public static async Task SeedRolesAsync(IServiceProvider serviceProvider)
    {
        var scope = serviceProvider.CreateScope();
        var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
        var userManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();

        string[] roles = { "Administrador", "Gerente", "Vendedor" };
        foreach (var role in roles)
        {
            if (!await roleManager.RoleExistsAsync(role))
            {
                await roleManager.CreateAsync(new IdentityRole(role));
            }
        }

        await CriarUsuarioSeNaoExistir(userManager, "admin@concessionaria.com", "Admin123!", "Administrador");
        await CriarUsuarioSeNaoExistir(userManager, "gerente@concessionaria.com", "Gerente123!", "Gerente");
        await CriarUsuarioSeNaoExistir(userManager, "vendedor@concessionaria.com", "Vendedor123!", "Vendedor");
    }

    public static async Task SeedDatabaseAsync(IServiceProvider serviceProvider)
    {
        var scope = serviceProvider.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        context.Database.EnsureCreated();

        if (!context.Fabricantes.Any())
        {
            context.Fabricantes.AddRange(
                new Fabricante { Nome = "Toyota", PaisOrigem = "Japão", AnoFundacao = 1937, Website = "https://www.toyota.com" },
                new Fabricante { Nome = "Ford", PaisOrigem = "EUA", AnoFundacao = 1903, Website = "https://www.ford.com" }
                // Adicione mais fabricantes conforme necessário
            );
            context.SaveChanges();
        }
    }

    private static async Task CriarUsuarioSeNaoExistir(UserManager<IdentityUser> userManager, string email, string senha, string role)
    {
        if (await userManager.FindByEmailAsync(email) == null)
        {
            var user = new IdentityUser { UserName = email, Email = email, EmailConfirmed = true };
            var result = await userManager.CreateAsync(user, senha);
            if (result.Succeeded)
            {
                await userManager.AddToRoleAsync(user, role);
                Console.WriteLine($"Usuário '{email}' criado com sucesso e atribuído ao perfil '{role}'.");
            }
        }
    }
}
