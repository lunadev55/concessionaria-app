namespace ConcessionariaApp.Application.Dtos;
public class FabricanteDto
{
    public int Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public string PaisOrigem { get; set; } = string.Empty;
    public int AnoFundacao { get; set; }
    public string? Website { get; set; }
}
