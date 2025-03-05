using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace ConcessionariaApp.Middlewares;
public class JwtCookieAuthenticationMiddleware
{
    private readonly RequestDelegate _next;
    private readonly string _jwtSecret;

    public JwtCookieAuthenticationMiddleware(RequestDelegate next, IConfiguration configuration)
    {
        _next = next;
        _jwtSecret = configuration["JwtSettings:SecretKey"] ?? throw new ArgumentNullException("Nao existe JWT Secret no arquivo de configuracao.");
    }

    public async Task Invoke(HttpContext context)
    {
        var token = context.Request.Cookies["AuthToken"];

        if (!string.IsNullOrEmpty(token))
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtSecret);

            try
            {
                var principal = tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true
                }, out _);

                context.User = principal;
            }
            catch
            {
                
            }
        }

        await _next(context);
    }
}