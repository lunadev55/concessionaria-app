using AutoMapper;
using ConcessionariaApp.Application.Dtos;
using ConcessionariaApp.Application.Interfaces.Services;
using ConcessionariaApp.Models;
using ConcessionariaApp.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ConcessionariaApp.MVC.Controllers;

[Controller]
[Route("[controller]")]
public class VeiculoController : Controller
{
    private readonly IVeiculoService _veiculoService;
    private readonly IFabricanteService _fabricanteService;
    private readonly IMapper _mapper;

    public VeiculoController(
        IVeiculoService veiculoService, 
        IFabricanteService fabricanteService,
        IMapper mapper)
    {
        _veiculoService = veiculoService;
        _fabricanteService = fabricanteService;
        _mapper = mapper;
    }  

    [HttpGet]
    public async Task<IActionResult> Index()
    {     
        var veiculos = await _veiculoService.GetAllAsync();
        return View(_mapper.Map<IEnumerable<VeiculoDto>>(veiculos));
    }

    [HttpGet("details/{id}")]
    public async Task<IActionResult> Details(int id)
    {
        var veiculo = await _veiculoService.GetByIdAsync(id);
        return View("Details", _mapper.Map<VeiculoDto>(veiculo));
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        ViewBag.Fabricantes =
                new SelectList(_mapper.Map<IEnumerable<FabricanteDto>>(await _fabricanteService.GetAllAsync()), "Id", "Nome");

        var veiculo = await _veiculoService.GetByIdAsync(id);
        if (veiculo == null) 
            return NotFound();

        return View("Edit", _mapper.Map<VeiculoDto>(veiculo));
    }

    [HttpGet("create")]
    public async Task<IActionResult> Create()
    {        
        ViewBag.Fabricantes =
                new SelectList(_mapper.Map<IEnumerable<FabricanteDto>>(await _fabricanteService.GetAllAsync()), "Id", "Nome");

        return View();
    }

    [HttpPost("create")]
    public async Task<IActionResult> Create([FromBody] VeiculoDto veiculoDto)
    {
        if (!ModelState.IsValid)
            return BadRequest("Dados inválidos!");

        var success = await _veiculoService.AddAsync(_mapper.Map<Veiculo>(veiculoDto));
        if (!success) 
            return Conflict("Modelo de veículo já existe.");

        return Ok(new { message = "Veículo criado com sucesso!" });
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Edit(int id, [FromBody] VeiculoDto veiculoDto)
    {
        if (id != veiculoDto.Id) 
            return BadRequest("ID inconsistente.");

        var success = await _veiculoService.UpdateAsync(_mapper.Map<Veiculo>(veiculoDto));
        if (!success) 
            return NotFound();
        
        return Ok(new { message = "Veículo criado com sucesso!" });
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var success = await _veiculoService.DeleteAsync(id);
        if (!success) 
            return NotFound();

        return NoContent();
    }
}
