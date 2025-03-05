using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ConcessionariaApp.Models;

public class Venda
{
    public int Id { get; set; }

    [Required]
    public int VeiculoID { get; set; }

    public Veiculo? Veiculo { get; set; }

    [Required]
    public int ConcessionariaID { get; set; }

    public Concessionaria? Concessionaria { get; set; }

    [Required]
    public int ClienteID { get; set; }

    public Cliente? Cliente { get; set; }

    [Required]
    public DateTime DataVenda { get; set; }

    [Column(TypeName = "decimal(10,2)")]
    public decimal PrecoVenda { get; set; }

    [Required, MaxLength(20)]
    public string Protocolo { get; set; } = string.Empty;
    public bool IsDeleted { get; set; } = false;
}
