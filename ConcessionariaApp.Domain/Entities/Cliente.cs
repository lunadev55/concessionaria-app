using System.ComponentModel.DataAnnotations;

namespace ConcessionariaApp.Models;

public class Cliente
{
    public int Id { get; set; }

    [Required, MaxLength(100)]
    public string Nome { get; set; } = string.Empty;

    [Required, MaxLength(11), MinLength(11)]
    public string CPF { get; set; } = string.Empty;

    [Required, MaxLength(15)]
    public string Telefone { get; set; } = string.Empty;
}
