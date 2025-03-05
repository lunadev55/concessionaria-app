using ConcessionariaApp.Application.Dtos;

namespace ConcessionariaApp.Application.Interfaces.Services;
public interface IAuthService
{
    Task<string?> RegisterUserAsync(UserRegisterDto userDto);
    Task<string?> LoginUserAsync(UserLoginDto loginDto);
}
