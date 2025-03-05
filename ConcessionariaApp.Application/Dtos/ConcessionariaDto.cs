namespace ConcessionariaApp.Application.Dtos;

public class ConcessionariaDto
{
    public int Id { get; set; }
    public string NomeConcessionaria { get; set; } = string.Empty;
    public string Endereco { get; set; } = string.Empty;
    public string Cidade { get; set; } = string.Empty;
    public string Estado { get; set; } = string.Empty;
    public string CEP { get; set; } = string.Empty;
    public string Telefone { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public int CapacidadeMaximaVeiculos { get; set; }
}

