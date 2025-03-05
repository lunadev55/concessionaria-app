namespace ConcessionariaApp.Application.Dtos;
public class RelatorioVendasDto
{
    public string TipoVeiculo { get; set; } = string.Empty;
    public string Fabricante { get; set; } = string.Empty;
    public string Concessionaria { get; set; } = string.Empty;
    public int TotalVendas { get; set; }
    public decimal ValorTotal { get; set; }
}
