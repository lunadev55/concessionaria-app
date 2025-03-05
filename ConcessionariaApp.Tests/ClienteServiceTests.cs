using ConcessionariaApp.Application.Interfaces.Repositories;
using ConcessionariaApp.Application.Services;
using ConcessionariaApp.Models;
using Moq;

namespace ConcessionariaApp.Tests;

public class ClienteServiceTests
{
    private readonly Mock<IClienteRepository> _clienteRepositoryMock;
    private readonly FakeLogger<ClienteService> _fakeLogger;
    private readonly ClienteService _clienteService;

    public ClienteServiceTests()
    {
        _clienteRepositoryMock = new Mock<IClienteRepository>();
        _fakeLogger = new FakeLogger<ClienteService>();
        _clienteService = new ClienteService(_clienteRepositoryMock.Object, _fakeLogger);
    }

    [Fact]
    public async Task GetClienteByCpf_ShouldReturnCliente_WhenClienteExists()
    {
        // Arrange
        var cpf = "12345678900";
        var cliente = new Cliente { Nome = "John Doe", CPF = cpf, Telefone = "987654321" };
        _clienteRepositoryMock.Setup(r => r.ObterPorCPFAsync(cpf)).ReturnsAsync(cliente);

        // Act
        var result = await _clienteService.GetClienteByCpf(cpf);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(cpf, result.CPF);
        _clienteRepositoryMock.Verify(r => r.ObterPorCPFAsync(cpf), Times.Once);

        // Verificar se o log contém a mensagem esperada
        Assert.Contains(_fakeLogger.Logs, log => log.Contains($"Buscando cliente com CPF: {cpf}"));
    }

    [Fact]
    public async Task GetClienteByCpf_ShouldReturnNull_WhenClienteDoesNotExist()
    {
        // Arrange
        var cpf = "12345678900";
        _clienteRepositoryMock.Setup(r => r.ObterPorCPFAsync(cpf)).ReturnsAsync((Cliente)null);

        // Act
        var result = await _clienteService.GetClienteByCpf(cpf);

        // Assert
        Assert.Null(result);
        _clienteRepositoryMock.Verify(r => r.ObterPorCPFAsync(cpf), Times.Once);

        // Verificar se o log contém a mensagem esperada
        Assert.Contains(_fakeLogger.Logs, log => log.Contains($"Nenhum cliente encontrado com o CPF: {cpf}"));
    }

    [Fact]
    public async Task ValidarOuCadastrarClienteAsync_ShouldThrowException_WhenClienteExists()
    {
        // Arrange
        var nome = "John Doe";
        var cpf = "12345678900";
        var telefone = "987654321";
        var clienteExistente = new Cliente { Nome = nome, CPF = cpf, Telefone = telefone };
        _clienteRepositoryMock.Setup(r => r.ObterPorCPFAsync(cpf)).ReturnsAsync(clienteExistente);

        // Act & Assert
        var exception = await Assert.ThrowsAsync<InvalidOperationException>(() => _clienteService.ValidarOuCadastrarClienteAsync(nome, cpf, telefone));
        Assert.Equal("Cliente já cadastrado com esse CPF.", exception.Message);
        _clienteRepositoryMock.Verify(r => r.ObterPorCPFAsync(cpf), Times.Once);

        // Verificar se o log contém a mensagem esperada
        Assert.Contains(_fakeLogger.Logs, log => log.Contains($"Tentativa de cadastro falhou. Cliente com CPF {cpf} já existe."));
    }

    [Fact]
    public async Task ValidarOuCadastrarClienteAsync_ShouldReturnNovoCliente_WhenClienteDoesNotExist()
    {
        // Arrange
        var nome = "Jane Doe";
        var cpf = "98765432100";
        var telefone = "912345678";
        var clienteExistente = (Cliente)null;
        _clienteRepositoryMock.Setup(r => r.ObterPorCPFAsync(cpf)).ReturnsAsync(clienteExistente);

        var novoCliente = new Cliente { Nome = nome, CPF = cpf, Telefone = telefone };
        _clienteRepositoryMock.Setup(r => r.AdicionarAsync(It.IsAny<Cliente>())).Returns(Task.CompletedTask);

        // Act
        var result = await _clienteService.ValidarOuCadastrarClienteAsync(nome, cpf, telefone);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(cpf, result.CPF);
        _clienteRepositoryMock.Verify(r => r.ObterPorCPFAsync(cpf), Times.Once);
        _clienteRepositoryMock.Verify(r => r.AdicionarAsync(It.IsAny<Cliente>()), Times.Once);

        // Verificar se o log contém a mensagem esperada
        Assert.Contains(_fakeLogger.Logs, log => log.Contains($"Novo cliente cadastrado com sucesso: Nome={nome}, CPF={cpf}"));
    }
}
