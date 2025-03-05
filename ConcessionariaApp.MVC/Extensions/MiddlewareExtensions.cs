using ConcessionariaApp.Middlewares;

namespace ConcessionariaApp.MVC.Extensions;
public static class MiddlewareExtensions
{
    public static void UseApplicationMiddlewares(this WebApplication app)
    {
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "Concessionária API v1");
            });
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();
        app.UseRouting();
        app.UseMiddleware<JwtCookieAuthenticationMiddleware>();
        app.UseAuthentication();
        app.UseAuthorization();
    }
}
