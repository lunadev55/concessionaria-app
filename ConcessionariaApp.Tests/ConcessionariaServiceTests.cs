using ConcessionariaApp.Application.Interfaces.Repositories;
using ConcessionariaApp.Models;
using ConcessionariaApp.Services;
using Moq;

namespace ConcessionariaApp.Tests;

public class ConcessionariaServiceTests
{
    private readonly Mock<IConcessionariaRepository> _mockRepository;
    private readonly FakeLogger<ConcessionariaService> _fakeLogger;
    private readonly ConcessionariaService _service;

    public ConcessionariaServiceTests()
    {
        _mockRepository = new Mock<IConcessionariaRepository>();
        _fakeLogger = new FakeLogger<ConcessionariaService>();
        _service = new ConcessionariaService(_mockRepository.Object, _fakeLogger);
    }

    [Fact]
    public async Task GetAllAsync_ShouldReturnConcessionarias_WhenDataExists()
    {
        // Arrange
        var concessionarias = new List<Concessionaria>
        {
            new Concessionaria { Id = 1, NomeConcessionaria = "Concessionária 1" },
            new Concessionaria { Id = 2, NomeConcessionaria = "Concessionária 2" }
        };
        _mockRepository.Setup(repo => repo.GetAllAsync()).ReturnsAsync(concessionarias);

        // Act
        var result = await _service.GetAllAsync();

        // Assert
        Assert.NotNull(result);
        Assert.Equal(2, result.Count());
        Assert.Equal("Concessionária 1", result.First().NomeConcessionaria);
        Assert.Equal("Concessionária 2", result.Last().NomeConcessionaria);
    }

    [Fact]
    public async Task GetByIdAsync_ShouldReturnNull_WhenConcessionariaNotFound()
    {
        // Arrange
        var id = 1;
        _mockRepository.Setup(repo => repo.GetByIdAsync(id)).ReturnsAsync((Concessionaria?)null);

        // Act
        var result = await _service.GetByIdAsync(id);

        // Assert
        Assert.Null(result);
        Assert.Contains("Nenhuma concessionária encontrada com ID", _fakeLogger.Logs.Last());
    }

    [Fact]
    public async Task AddAsync_ShouldReturnTrue_WhenConcessionariaAddedSuccessfully()
    {
        // Arrange
        var concessionaria = new Concessionaria { NomeConcessionaria = "Concessionária Teste" };
        _mockRepository.Setup(repo => repo.AddAsync(concessionaria)).ReturnsAsync(true);

        // Act
        var result = await _service.AddAsync(concessionaria);

        // Assert
        Assert.True(result);
        Assert.Contains("Concessionária adicionada com sucesso", _fakeLogger.Logs.Last());
    }

    [Fact]
    public async Task AddAsync_ShouldReturnFalse_WhenFailedToAddConcessionaria()
    {
        // Arrange
        var concessionaria = new Concessionaria { NomeConcessionaria = "Concessionária Teste" };
        _mockRepository.Setup(repo => repo.AddAsync(concessionaria)).ReturnsAsync(false);

        // Act
        var result = await _service.AddAsync(concessionaria);

        // Assert
        Assert.False(result);
        Assert.Contains("Erro ao adicionar concessionária", _fakeLogger.Logs.Last());
    }

    [Fact]
    public async Task DeleteAsync_ShouldReturnTrue_WhenConcessionariaDeletedSuccessfully()
    {
        // Arrange
        var id = 1;
        _mockRepository.Setup(repo => repo.DeleteAsync(id)).ReturnsAsync(true);

        // Act
        var result = await _service.DeleteAsync(id);

        // Assert
        Assert.True(result);
        Assert.Contains("Concessionária excluída com sucesso", _fakeLogger.Logs.Last());
    }

    [Fact]
    public async Task DeleteAsync_ShouldReturnFalse_WhenFailedToDeleteConcessionaria()
    {
        // Arrange
        var id = 1;
        _mockRepository.Setup(repo => repo.DeleteAsync(id)).ReturnsAsync(false);

        // Act
        var result = await _service.DeleteAsync(id);

        // Assert
        Assert.False(result);
        Assert.Contains("Erro ao excluir concessionária", _fakeLogger.Logs.Last());
    }
}

