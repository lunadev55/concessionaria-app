using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using ConcessionariaApp.Models.Enums;

namespace ConcessionariaApp.Models;

public class Veiculo
{
    public int Id { get; set; }

    [Required, MaxLength(100)]
    public string Modelo { get; set; } = string.Empty;

    [Range(1900, 2100)]
    public int AnoFabricacao { get; set; }

    [Column(TypeName = "decimal(10,2)")]
    public decimal Preco { get; set; }

    [Required]
    public int FabricanteID { get; set; }

    [JsonIgnore]
    public Fabricante? Fabricante { get; set; }

    [Required]
    public TipoVeiculo Tipo { get; set; }

    [MaxLength(500)]
    public string? Descricao { get; set; }
    public bool IsDeleted { get; set; } = false;
}
