using System.ComponentModel.DataAnnotations;
using ConcessionariaApp.Models.Enums;

namespace ConcessionariaApp.Models;

public class Usuario
{
    public int UsuarioID { get; set; }

    [Required, MaxLength(50)]
    public string NomeUsuario { get; set; } = string.Empty;

    [Required, EmailAddress, MaxLength(100)]
    public string Email { get; set; } = string.Empty;

    [Required, MaxLength(255)]
    public string Senha { get; set; } = string.Empty;

    [Required]
    public NivelAcesso NivelAcesso { get; set; }
}