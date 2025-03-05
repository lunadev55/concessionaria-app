using ConcessionariaApp.Application.Dtos;
using ConcessionariaApp.Application.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ConcessionariaApp.Controllers;

[ApiController]
[AllowAnonymous]
[Route("[controller]")]
public class AuthController : Controller
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }
    [HttpGet]
    public IActionResult Index()
    {
        return View("Login");
    }
    [HttpGet("register")]
    public IActionResult Register()
    {
        return View("Register");
    }
    [HttpPost("register")]       
    public async Task<IActionResult> Register(UserRegisterDto userDto)
    {
        var token = await _authService.RegisterUserAsync(userDto);
        if (token == null) return BadRequest("Falha no registro.");
        return Ok(new { Token = token });
    }
    
    [HttpPost("login")]
    public async Task<IActionResult> Login(UserLoginDto loginDto)
    {
        var token = await _authService.LoginUserAsync(loginDto);
        if (token == null) return Unauthorized("Credenciais inválidas.");

        Response.Cookies.Append("AuthToken", token, new CookieOptions
        {
            HttpOnly = true,
            Secure = true,
            SameSite = SameSiteMode.Strict
        });        

        return Ok(new { Token = token });
    }

    [HttpPost("logout")]
    public IActionResult Logout()
    {
        Response.Cookies.Delete("AuthToken");
        return Ok(new { message = "Logout realizado com sucesso" });
    }

}
