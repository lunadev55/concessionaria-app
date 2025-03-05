using System.ComponentModel.DataAnnotations;

namespace ConcessionariaApp.Models;

public class Fabricante
{
    public int Id { get; set; }

    [Required, MaxLength(100), Display(Name = "Nome do Fabricante")]
    public string Nome { get; set; } = string.Empty;

    [MaxLength(50), Display(Name = "País de Origem")]
    public string PaisOrigem { get; set; } = string.Empty;

    [Range(1800, 2100), Display(Name = "Ano de Fundação")]
    public int AnoFundacao { get; set; }

    [Url, Display(Name = "Website")]
    public string? Website { get; set; }
    public ICollection<Veiculo>? Veiculos { get; set; }
    public bool IsDeleted { get; set; } = false;
}
