using ConcessionariaApp.Application.Dtos;
using ConcessionariaApp.Application.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ConcessionariaApp.MVC.Controllers;

[Authorize(Roles = "Gerente")]
[ApiController]
[Route("api/[controller]")]
public class RelatorioController : ControllerBase
{
    private readonly IRelatorioService _relatorioVendasService;

    public RelatorioController(IRelatorioService relatorioVendasService)
    {
        _relatorioVendasService = relatorioVendasService;
    }

    [HttpGet("vendas")]
    public async Task<ActionResult<IEnumerable<RelatorioVendasDto>>> ObterRelatorioVendas([FromQuery] int mes, [FromQuery] int ano)
    {
        if (mes < 1 || mes > 12 || ano < 2000) 
            return BadRequest("Mês ou ano inválidos.");

        var relatorio = await _relatorioVendasService.GerarRelatorioMensalAsync(mes, ano);

        if (!relatorio.Any())
            return NotFound("Nenhum dado encontrado para o período especificado.");

        return Ok(relatorio);
    }
}
