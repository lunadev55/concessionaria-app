using ConcessionariaApp.Application.Dtos;
using ConcessionariaApp.Application.Interfaces.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ConcessionariaApp.Services;

public class AuthService : IAuthService
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly IConfiguration _configuration;
    private readonly ILogger<AuthService> _logger;

    public AuthService(UserManager<IdentityUser> userManager, IConfiguration configuration, ILogger<AuthService> logger)
    {
        _userManager = userManager;
        _configuration = configuration;
        _logger = logger;
    }

    public async Task<string?> RegisterUserAsync(UserRegisterDto userDto)
    {
        _logger.LogInformation("Tentativa de registro do usu�rio: {Username}", userDto.Username);

        var user = new IdentityUser { UserName = userDto.Username };
        var result = await _userManager.CreateAsync(user, userDto.Password);

        if (result.Succeeded)
        {
            var role = string.IsNullOrEmpty(userDto.Role) ? "Administrador" : userDto.Role;
            await _userManager.AddToRoleAsync(user, role);
            _logger.LogInformation("Usu�rio {Username} registrado com sucesso na fun��o {Role}", userDto.Username, role);

            return GenerateJwtToken(user, role);
        }

        _logger.LogWarning("Falha ao registrar usu�rio {Username}. Erros: {Errors}", userDto.Username, string.Join(", ", result.Errors.Select(e => e.Description)));
        return null;
    }

    public async Task<string?> LoginUserAsync(UserLoginDto loginDto)
    {
        _logger.LogInformation("Tentativa de login do usu�rio: {Username}", loginDto.Username);

        var user = await _userManager.FindByNameAsync(loginDto.Username);
        if (user != null && await _userManager.CheckPasswordAsync(user, loginDto.Password))
        {
            var roles = await _userManager.GetRolesAsync(user);
            var role = roles.FirstOrDefault() ?? "Vendedor";
            _logger.LogInformation("Login bem-sucedido para o usu�rio {Username}, fun��o: {Role}", loginDto.Username, role);

            return GenerateJwtToken(user, role);
        }

        _logger.LogWarning("Falha no login para o usu�rio {Username}. Credenciais inv�lidas.", loginDto.Username);
        return null;
    }

    private string GenerateJwtToken(IdentityUser user, string role)
    {
        _logger.LogInformation("Gerando token JWT para o usu�rio {Username}", user.UserName);

        var jwtSettings = _configuration.GetSection("JwtSettings");
        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.Id),
            new Claim(JwtRegisteredClaimNames.UniqueName, user.UserName!),
            new Claim(ClaimTypes.Role, role)
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["SecretKey"]!));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: jwtSettings["Issuer"],
            audience: jwtSettings["Audience"],
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(double.Parse(jwtSettings["TokenExpirationMinutes"]!)),
            signingCredentials: creds
        );

        string tokenString = new JwtSecurityTokenHandler().WriteToken(token);
        _logger.LogInformation("Token JWT gerado com sucesso para o usu�rio {Username}", user.UserName);

        return tokenString;
    }
}
