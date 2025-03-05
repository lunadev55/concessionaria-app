namespace ConcessionariaApp.Application.Dtos;

public class VendaDto
{
    public int Id { get; set; }
    public int VeiculoID { get; set; }
    public VeiculoDto? Veiculo{ get; set; }
    public int ConcessionariaID { get; set; }
    public ConcessionariaDto? Concessionaria { get; set; }
    public int ClienteID { get; set; }
    public ClienteDto? Cliente { get; set; }
    public DateTime DataVenda { get; set; }
    public decimal PrecoVenda { get; set; }
    public string Protocolo { get; set; } = string.Empty;
}
