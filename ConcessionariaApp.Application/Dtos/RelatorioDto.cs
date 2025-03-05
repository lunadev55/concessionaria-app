﻿namespace ConcessionariaApp.Application.Dtos;

public class RelatorioDto
{
    public int Id { get; set; }
    public DateTime DataGeracao { get; set; }
    public string Tipo { get; set; } = string.Empty;
    public string CaminhoArquivo { get; set; } = string.Empty;
}
