using ConcessionariaApp.Application.Interfaces.Repositories;
using ConcessionariaApp.Application.Interfaces.Services;
using ConcessionariaApp.Application.Services;
using ConcessionariaApp.Data.Repositories;
using ConcessionariaApp.Mappings;
using ConcessionariaApp.Services;
using Microsoft.Extensions.DependencyInjection;

namespace ConcessionariaApp.DependencyInjection
{
    public static class DependencyInjectionConfig
    {
        public static IServiceCollection RegisterDependencies(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(MappingProfile));

            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IConcessionariaService, ConcessionariaService>();
            services.AddScoped<IFabricanteService, FabricanteService>();
            services.AddScoped<IRelatorioService, RelatorioService>();
            services.AddScoped<IVeiculoService, VeiculoService>();
            services.AddScoped<IVendaService, VendaService>();
            services.AddScoped<IClienteService, ClienteService>();

            services.AddScoped<IConcessionariaRepository, ConcessionariaRepository>();
            services.AddScoped<IFabricanteRepository, FabricanteRepository>();
            services.AddScoped<IRelatorioRepository, RelatorioRepository>();
            services.AddScoped<IVeiculoRepository, VeiculoRepository>();
            services.AddScoped<IVendaRepository, VendaRepository>();
            services.AddScoped<IClienteRepository, ClienteRepository>();

            return services;
        }
    }
}
