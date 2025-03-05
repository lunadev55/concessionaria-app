using ConcessionariaApp.Application.Interfaces.Repositories;
using ConcessionariaApp.Models;
using ConcessionariaApp.Services;
using Moq;

namespace ConcessionariaApp.Tests;

public class VendaServiceTests
{
    private readonly Mock<IVendaRepository> _mockVendaRepository;
    private readonly FakeLogger<VendaService> _fakeLogger;
    private readonly VendaService _service;

    public VendaServiceTests()
    {
        _mockVendaRepository = new Mock<IVendaRepository>();
        _fakeLogger = new FakeLogger<VendaService>();
        _service = new VendaService(_mockVendaRepository.Object, _fakeLogger);
    }

    [Fact]
    public async Task GetAllAsync_ShouldReturnVendas_WhenDataExists()
    {
        // Arrange
        var vendas = new List<Venda>
        {
            new Venda { Id = 1, Protocolo = "PROTOCOLO1", PrecoVenda = 50000 },
            new Venda { Id = 2, Protocolo = "PROTOCOLO2", PrecoVenda = 60000 }
        };
        _mockVendaRepository.Setup(repo => repo.GetAllAsync()).ReturnsAsync(vendas);

        // Act
        var result = await _service.GetAllAsync();

        // Assert
        Assert.NotNull(result);
        Assert.Equal(2, result.Count());
        Assert.Equal("PROTOCOLO1", result.First().Protocolo);
        Assert.Equal("PROTOCOLO2", result.Last().Protocolo);
    }

    [Fact]
    public async Task GetByIdAsync_ShouldReturnNull_WhenVendaNotFound()
    {
        // Arrange
        var id = 1;
        _mockVendaRepository.Setup(repo => repo.GetByIdAsync(id)).ReturnsAsync((Venda?)null);

        // Act
        var result = await _service.GetByIdAsync(id);

        // Assert
        Assert.Null(result);
        Assert.Contains("Nenhuma venda encontrada com ID", _fakeLogger.Logs.Last());
    }

    [Fact]
    public async Task AddAsync_ShouldReturnError_WhenClienteHasExistingVenda()
    {
        // Arrange
        var venda = new Venda
        {
            Cliente = new Cliente { CPF = "12345678900" },
            VeiculoID = 1,
            PrecoVenda = 50000
        };
        _mockVendaRepository.Setup(repo => repo.ClientePossuiVendaAsync(venda.Cliente.CPF)).ReturnsAsync(true);

        // Act
        var result = await _service.AddAsync(venda);

        // Assert
        Assert.Equal("CPF do cliente já existe em outra venda.", result);
        Assert.Contains("Cliente com CPF", _fakeLogger.Logs.Last());
    }

    [Fact]
    public async Task AddAsync_ShouldReturnError_WhenVeiculoNotFound()
    {
        // Arrange
        var venda = new Venda
        {
            Cliente = new Cliente { CPF = "12345678900" },
            VeiculoID = 1,
            PrecoVenda = 50000
        };
        _mockVendaRepository.Setup(repo => repo.ClientePossuiVendaAsync(venda.Cliente.CPF)).ReturnsAsync(false);
        _mockVendaRepository.Setup(repo => repo.GetVeiculoByIdAsync(venda.VeiculoID)).ReturnsAsync((Veiculo?)null);

        // Act
        var result = await _service.AddAsync(venda);

        // Assert
        Assert.Equal("Veículo não encontrado.", result);
        Assert.Contains("Tentativa de venda para um veículo inexistente", _fakeLogger.Logs.Last());
    }

    [Fact]
    public async Task AddAsync_ShouldReturnError_WhenPrecoVendaIsInvalid()
    {
        // Arrange
        var veiculo = new Veiculo { Preco = 50000 };
        var venda = new Venda
        {
            Cliente = new Cliente { CPF = "12345678900" },
            VeiculoID = 1,
            PrecoVenda = 60000 // Preço de venda maior que o do veículo
        };
        _mockVendaRepository.Setup(repo => repo.ClientePossuiVendaAsync(venda.Cliente.CPF)).ReturnsAsync(false);
        _mockVendaRepository.Setup(repo => repo.GetVeiculoByIdAsync(venda.VeiculoID)).ReturnsAsync(veiculo);

        // Act
        var result = await _service.AddAsync(venda);

        // Assert
        Assert.Equal("Preço de venda inválido ou superior ao preço do veículo.", result);
        Assert.Contains("Tentativa de venda com preço superior ao preço do veículo", _fakeLogger.Logs.Last());
    }

    [Fact]
    public async Task DeleteAsync_ShouldReturnTrue_WhenSuccessfullyDeleted()
    {
        // Arrange
        var id = 1;
        _mockVendaRepository.Setup(repo => repo.DeleteAsync(id)).ReturnsAsync(true);

        // Act
        var result = await _service.DeleteAsync(id);

        // Assert
        Assert.True(result);
        Assert.Contains("Venda ID", _fakeLogger.Logs.Last());
    }

    [Fact]
    public async Task DeleteAsync_ShouldReturnFalse_WhenFailedToDelete()
    {
        // Arrange
        var id = 1;
        _mockVendaRepository.Setup(repo => repo.DeleteAsync(id)).ReturnsAsync(false);

        // Act
        var result = await _service.DeleteAsync(id);

        // Assert
        Assert.False(result);
        Assert.Contains("Falha ao excluir venda ID", _fakeLogger.Logs.Last());
    }
}
