using ConcessionariaApp.Application.Interfaces.Repositories;
using ConcessionariaApp.Models;
using ConcessionariaApp.Services;
using Moq;

namespace ConcessionariaApp.Tests;

public class VeiculoServiceTests
{
    private readonly Mock<IVeiculoRepository> _mockRepository;
    private readonly FakeLogger<VeiculoService> _fakeLogger;
    private readonly VeiculoService _service;

    public VeiculoServiceTests()
    {
        _mockRepository = new Mock<IVeiculoRepository>();
        _fakeLogger = new FakeLogger<VeiculoService>();
        _service = new VeiculoService(_mockRepository.Object, _fakeLogger);
    }

    [Fact]
    public async Task GetAllAsync_ShouldReturnVeiculos_WhenDataExists()
    {
        // Arrange
        var veiculos = new List<Veiculo>
        {
            new Veiculo { Id = 1, Modelo = "Modelo 1", AnoFabricacao = 2020 },
            new Veiculo { Id = 2, Modelo = "Modelo 2", AnoFabricacao = 2021 }
        };
        _mockRepository.Setup(repo => repo.GetAllAsync()).ReturnsAsync(veiculos);

        // Act
        var result = await _service.GetAllAsync();

        // Assert
        Assert.NotNull(result);
        Assert.Equal(2, result.Count());
        Assert.Equal("Modelo 1", result.First().Modelo);
        Assert.Equal("Modelo 2", result.Last().Modelo);
    }

    [Fact]
    public async Task GetByIdAsync_ShouldReturnNull_WhenVeiculoNotFound()
    {
        // Arrange
        var id = 1;
        _mockRepository.Setup(repo => repo.GetByIdAsync(id)).ReturnsAsync((Veiculo?)null);

        // Act
        var result = await _service.GetByIdAsync(id);

        // Assert
        Assert.Null(result);
        Assert.Contains("Nenhum veículo encontrado com ID", _fakeLogger.Logs.Last());
    }

    [Fact]
    public async Task AddAsync_ShouldReturnTrue_WhenSuccessfullyAdded()
    {
        // Arrange
        var veiculo = new Veiculo { Modelo = "Modelo Teste", AnoFabricacao = 2022 };
        _mockRepository.Setup(repo => repo.AddAsync(veiculo)).ReturnsAsync(true);

        // Act
        var result = await _service.AddAsync(veiculo);

        // Assert
        Assert.True(result);
        Assert.Contains("Veículo adicionado com sucesso", _fakeLogger.Logs.Last());
    }

    [Fact]
    public async Task AddAsync_ShouldReturnFalse_WhenFailedToAdd()
    {
        // Arrange
        var veiculo = new Veiculo { Modelo = "Modelo Teste", AnoFabricacao = 2022 };
        _mockRepository.Setup(repo => repo.AddAsync(veiculo)).ReturnsAsync(false);

        // Act
        var result = await _service.AddAsync(veiculo);

        // Assert
        Assert.False(result);
        Assert.Contains("Falha ao adicionar veículo", _fakeLogger.Logs.Last());
    }

    [Fact]
    public async Task UpdateAsync_ShouldReturnTrue_WhenSuccessfullyUpdated()
    {
        // Arrange
        var veiculo = new Veiculo { Id = 1, Modelo = "Modelo Atualizado", AnoFabricacao = 2023 };
        _mockRepository.Setup(repo => repo.UpdateAsync(veiculo)).ReturnsAsync(true);

        // Act
        var result = await _service.UpdateAsync(veiculo);

        // Assert
        Assert.True(result);
        Assert.Contains("Veículo atualizado com sucesso", _fakeLogger.Logs.Last());
    }

    [Fact]
    public async Task UpdateAsync_ShouldReturnFalse_WhenFailedToUpdate()
    {
        // Arrange
        var veiculo = new Veiculo { Id = 1, Modelo = "Modelo Atualizado", AnoFabricacao = 2023 };
        _mockRepository.Setup(repo => repo.UpdateAsync(veiculo)).ReturnsAsync(false);

        // Act
        var result = await _service.UpdateAsync(veiculo);

        // Assert
        Assert.False(result);
        Assert.Contains("Falha ao atualizar veículo ID", _fakeLogger.Logs.Last());
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
        Assert.Contains("Veículo ID", _fakeLogger.Logs.Last());
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
        Assert.Contains("Falha ao excluir veículo ID", _fakeLogger.Logs.Last());
    }
}
