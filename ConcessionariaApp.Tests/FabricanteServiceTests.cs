using ConcessionariaApp.Application.Interfaces.Repositories;
using ConcessionariaApp.Models;
using ConcessionariaApp.Services;
using Moq;

namespace ConcessionariaApp.Tests;

public class FabricanteServiceTests
{
    private readonly Mock<IFabricanteRepository> _mockRepository;
    private readonly FakeLogger<FabricanteService> _fakeLogger;
    private readonly FabricanteService _service;

    public FabricanteServiceTests()
    {
        _mockRepository = new Mock<IFabricanteRepository>();
        _fakeLogger = new FakeLogger<FabricanteService>();
        _service = new FabricanteService(_mockRepository.Object, _fakeLogger);
    }

    [Fact]
    public async Task GetAllAsync_ShouldReturnFabricantes_WhenDataExists()
    {
        // Arrange
        var fabricantes = new List<Fabricante>
        {
            new Fabricante { Id = 1, Nome = "Fabricante 1" },
            new Fabricante { Id = 2, Nome = "Fabricante 2" }
        };
        _mockRepository.Setup(repo => repo.GetAllAsync()).ReturnsAsync(fabricantes);

        // Act
        var result = await _service.GetAllAsync();

        // Assert
        Assert.NotNull(result);
        Assert.Equal(2, result.Count());
        Assert.Equal("Fabricante 1", result.First().Nome);
        Assert.Equal("Fabricante 2", result.Last().Nome);
    }

    [Fact]
    public async Task GetByIdAsync_ShouldReturnNull_WhenFabricanteNotFound()
    {
        // Arrange
        var id = 1;
        _mockRepository.Setup(repo => repo.GetByIdAsync(id)).ReturnsAsync((Fabricante?)null);

        // Act
        var result = await _service.GetByIdAsync(id);

        // Assert
        Assert.Null(result);
        Assert.Contains("Nenhum fabricante encontrado com ID", _fakeLogger.Logs.Last());
    }

    [Fact]
    public async Task AddAsync_ShouldReturnFabricante_WhenSuccessfullyAdded()
    {
        // Arrange
        var fabricante = new Fabricante { Nome = "Fabricante Teste" };
        _mockRepository.Setup(repo => repo.AddAsync(fabricante)).ReturnsAsync(fabricante);

        // Act
        var result = await _service.AddAsync(fabricante);

        // Assert
        Assert.NotNull(result);
        Assert.Equal("Fabricante Teste", result.Nome);
        Assert.Contains("Fabricante adicionado com sucesso", _fakeLogger.Logs.Last());
    }

    [Fact]
    public async Task AddAsync_ShouldReturnNull_WhenFailedToAdd()
    {
        // Arrange
        var fabricante = new Fabricante { Nome = "Fabricante Teste" };
        _mockRepository.Setup(repo => repo.AddAsync(fabricante)).ReturnsAsync((Fabricante?)null);

        // Act
        var result = await _service.AddAsync(fabricante);

        // Assert
        Assert.Null(result);
        Assert.Contains("Erro ao adicionar fabricante", _fakeLogger.Logs.Last());
    }

    [Fact]
    public async Task UpdateAsync_ShouldReturnFabricante_WhenSuccessfullyUpdated()
    {
        // Arrange
        var fabricante = new Fabricante { Id = 1, Nome = "Fabricante Atualizado" };
        _mockRepository.Setup(repo => repo.UpdateAsync(fabricante)).ReturnsAsync(fabricante);

        // Act
        var result = await _service.UpdateAsync(fabricante);

        // Assert
        Assert.NotNull(result);
        Assert.Equal("Fabricante Atualizado", result.Nome);
        Assert.Contains("Fabricante atualizado com sucesso", _fakeLogger.Logs.Last());
    }

    [Fact]
    public async Task UpdateAsync_ShouldReturnNull_WhenFailedToUpdate()
    {
        // Arrange
        var fabricante = new Fabricante { Id = 1, Nome = "Fabricante Atualizado" };
        _mockRepository.Setup(repo => repo.UpdateAsync(fabricante)).ReturnsAsync((Fabricante?)null);

        // Act
        var result = await _service.UpdateAsync(fabricante);

        // Assert
        Assert.Null(result);
        Assert.Contains("Erro ao atualizar fabricante", _fakeLogger.Logs.Last());
    }

    [Fact]
    public async Task DeleteAsync_ShouldReturnTrue_WhenSuccessfullyDeleted()
    {
        // Arrange
        var id = 1;
        _mockRepository.Setup(repo => repo.DeleteAsync(id)).ReturnsAsync(true);

        // Act
        var result = await _service.DeleteAsync(id);

        // Assert
        Assert.True(result);
        Assert.Contains("Fabricante excluído com sucesso", _fakeLogger.Logs.Last());
    }

    [Fact]
    public async Task DeleteAsync_ShouldReturnFalse_WhenFailedToDelete()
    {
        // Arrange
        var id = 1;
        _mockRepository.Setup(repo => repo.DeleteAsync(id)).ReturnsAsync(false);

        // Act
        var result = await _service.DeleteAsync(id);

        // Assert
        Assert.False(result);
        Assert.Contains("Erro ao excluir fabricante", _fakeLogger.Logs.Last());
    }
}
