using System.ComponentModel.DataAnnotations;

namespace ConcessionariaApp.Models;

public class Concessionaria
{
    public int Id { get; set; }

    [Required, MaxLength(100)]
    public string NomeConcessionaria { get; set; } = string.Empty;

    [Required, MaxLength(255)]
    public string Endereco { get; set; } = string.Empty;

    [Required, MaxLength(50)]
    public string Cidade { get; set; } = string.Empty;

    [Required, MaxLength(50)]
    public string Estado { get; set; } = string.Empty;

    [Required, MaxLength(10)]
    public string CEP { get; set; } = string.Empty;

    [Required, MaxLength(15)]
    public string Telefone { get; set; } = string.Empty;

    [Required, EmailAddress, MaxLength(100)]
    public string Email { get; set; } = string.Empty;

    [Range(1, int.MaxValue)]
    public int CapacidadeMaximaVeiculos { get; set; }

    public bool IsDeleted { get; set; } = false;
}
