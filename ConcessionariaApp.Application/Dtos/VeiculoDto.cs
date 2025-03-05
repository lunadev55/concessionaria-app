using ConcessionariaApp.Models.Enums;

namespace ConcessionariaApp.Application.Dtos;

public class VeiculoDto
{
    public int Id { get; set; }
    public string Modelo { get; set; } = string.Empty;
    public int AnoFabricacao { get; set; }
    public decimal Preco { get; set; }
    public int FabricanteID { get; set; }
    public FabricanteDto? Fabricante { get; set; }
    public TipoVeiculo Tipo { get; set; }
    public string? Descricao { get; set; }
}

