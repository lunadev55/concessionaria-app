using ConcessionariaApp.MVC.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Adicionar serviços ao container
builder.Services.AddApplicationServices(builder.Configuration);
builder.Services.AddAuthenticationServices(builder.Configuration);
builder.Services.AddSwaggerServices();

var app = builder.Build();

// Inicialização de dados
await DataSeedExtensions.SeedRolesAsync(app.Services);
await DataSeedExtensions.SeedDatabaseAsync(app.Services);

// Configuração do pipeline de middleware
app.UseApplicationMiddlewares();

// Mapear rotas
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapRazorPages();
app.Run();
