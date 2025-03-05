using ConcessionariaApp.Application.Dtos;
using ConcessionariaApp.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Moq;

namespace ConcessionariaApp.Tests;

public class AuthServiceTests
{
    private readonly Mock<UserManager<IdentityUser>> _userManagerMock;
    private readonly Mock<IConfiguration> _configurationMock;
    private readonly Mock<ILogger<AuthService>> _loggerMock;
    private readonly AuthService _authService;

    public AuthServiceTests()
    {
        _userManagerMock = new Mock<UserManager<IdentityUser>>(
            new Mock<IUserStore<IdentityUser>>().Object,
            null, null, null, null, null, null, null, null);
        _configurationMock = new Mock<IConfiguration>();
        _loggerMock = new Mock<ILogger<AuthService>>();
        _authService = new AuthService(_userManagerMock.Object, _configurationMock.Object, _loggerMock.Object);
    }

    [Fact]
    public async Task RegisterUserAsync_ShouldReturnToken_WhenUserIsSuccessfullyRegistered()
    {
        // Arrange
        var userDto = new UserRegisterDto
        {
            Username = "testuser",
            Password = "Password123",
            Role = "Admin"
        };

        var identityResult = IdentityResult.Success;
        _userManagerMock.Setup(x => x.CreateAsync(It.IsAny<IdentityUser>(), It.IsAny<string>())).ReturnsAsync(identityResult);
        _userManagerMock.Setup(x => x.AddToRoleAsync(It.IsAny<IdentityUser>(), It.IsAny<string>())).ReturnsAsync(IdentityResult.Success);

        _configurationMock.Setup(x => x.GetSection("JwtSettings")["SecretKey"]).Returns("F0GWWQ39ssXQewnpXPlLvRNjgPYbHQyTyxgRXDvyHlkrtD61B8EQSG3/j0+t/qAo");
        _configurationMock.Setup(x => x.GetSection("JwtSettings")["Issuer"]).Returns("myissuer");
        _configurationMock.Setup(x => x.GetSection("JwtSettings")["Audience"]).Returns("myaudience");
        _configurationMock.Setup(x => x.GetSection("JwtSettings")["TokenExpirationMinutes"]).Returns("60");

        // Act
        var result = await _authService.RegisterUserAsync(userDto);

        // Assert
        Assert.NotNull(result);
        _userManagerMock.Verify(x => x.CreateAsync(It.IsAny<IdentityUser>(), It.IsAny<string>()), Times.Once);
        _userManagerMock.Verify(x => x.AddToRoleAsync(It.IsAny<IdentityUser>(), It.IsAny<string>()), Times.Once);
    }

    [Fact]
    public async Task RegisterUserAsync_ShouldReturnNull_WhenRegistrationFails()
    {
        // Arrange
        var userDto = new UserRegisterDto
        {
            Username = "testuser",
            Password = "Password123"
        };

        var identityResult = IdentityResult.Failed(new IdentityError { Description = "Error registering user." });
        _userManagerMock.Setup(x => x.CreateAsync(It.IsAny<IdentityUser>(), It.IsAny<string>())).ReturnsAsync(identityResult);

        // Act
        var result = await _authService.RegisterUserAsync(userDto);

        // Assert
        Assert.Null(result);
        _userManagerMock.Verify(x => x.CreateAsync(It.IsAny<IdentityUser>(), It.IsAny<string>()), Times.Once);
    }

    [Fact]
    public async Task LoginUserAsync_ShouldReturnToken_WhenCredentialsAreValid()
    {
        // Arrange
        var loginDto = new UserLoginDto
        {
            Username = "testuser",
            Password = "Password123"
        };

        var user = new IdentityUser { UserName = "testuser" };
        _userManagerMock.Setup(x => x.FindByNameAsync(It.IsAny<string>())).ReturnsAsync(user);
        _userManagerMock.Setup(x => x.CheckPasswordAsync(It.IsAny<IdentityUser>(), It.IsAny<string>())).ReturnsAsync(true);
        _userManagerMock.Setup(x => x.GetRolesAsync(It.IsAny<IdentityUser>())).ReturnsAsync(new List<string> { "Admin" });

        _configurationMock.Setup(x => x.GetSection("JwtSettings")["SecretKey"]).Returns("F0GWWQ39ssXQewnpXPlLvRNjgPYbHQyTyxgRXDvyHlkrtD61B8EQSG3/j0+t/qAo");
        _configurationMock.Setup(x => x.GetSection("JwtSettings")["Issuer"]).Returns("myissuer");
        _configurationMock.Setup(x => x.GetSection("JwtSettings")["Audience"]).Returns("myaudience");
        _configurationMock.Setup(x => x.GetSection("JwtSettings")["TokenExpirationMinutes"]).Returns("60");

        // Act
        var result = await _authService.LoginUserAsync(loginDto);

        // Assert
        Assert.NotNull(result);
        _userManagerMock.Verify(x => x.FindByNameAsync(It.IsAny<string>()), Times.Once);
        _userManagerMock.Verify(x => x.CheckPasswordAsync(It.IsAny<IdentityUser>(), It.IsAny<string>()), Times.Once);
        _userManagerMock.Verify(x => x.GetRolesAsync(It.IsAny<IdentityUser>()), Times.Once);
    }

    [Fact]
    public async Task LoginUserAsync_ShouldReturnNull_WhenCredentialsAreInvalid()
    {
        // Arrange
        var loginDto = new UserLoginDto
        {
            Username = "testuser",
            Password = "WrongPassword"
        };

        _userManagerMock.Setup(x => x.FindByNameAsync(It.IsAny<string>())).ReturnsAsync((IdentityUser)null);

        // Act
        var result = await _authService.LoginUserAsync(loginDto);

        // Assert
        Assert.Null(result);
        _userManagerMock.Verify(x => x.FindByNameAsync(It.IsAny<string>()), Times.Once);
    }
}
