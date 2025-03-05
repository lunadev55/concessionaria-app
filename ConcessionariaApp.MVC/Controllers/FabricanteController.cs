using AutoMapper;
using ConcessionariaApp.Application.Dtos;
using ConcessionariaApp.Application.Interfaces.Services;
using ConcessionariaApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace ConcessionariaApp.MVC.Controllers;

[ApiController]
[Route("[controller]")]
public class FabricanteController : Controller
{
    private readonly IFabricanteService _fabricanteService;
    private readonly IMapper _mapper;

    public FabricanteController(IFabricanteService fabricanteService, IMapper mapper)
    {
        _fabricanteService = fabricanteService;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var fabricantes = await _fabricanteService.GetAllAsync();
        return View(_mapper.Map<IEnumerable<FabricanteDto>>(fabricantes));
    }

    [HttpGet("details/{id}")]
    public async Task<IActionResult> Details(int id)
    {
        var fabricante = await _fabricanteService.GetByIdAsync(id);
        return View("Details", _mapper.Map<FabricanteDto>(fabricante));
    }

    [HttpGet("create")]
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost("create")]    
    public async Task<IActionResult> Create(FabricanteDto fabricanteDto)
    {
        if (!ModelState.IsValid)
            return BadRequest("Dados inválidos!");

        var created = await _fabricanteService.AddAsync(_mapper.Map<Fabricante>(fabricanteDto));        
        return Ok(new { message = "Fabricante criado com sucesso!" });
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Edit(int id)
    {
        var fabricante = await _fabricanteService.GetByIdAsync(id);
        return View(_mapper.Map<FabricanteDto>(fabricante));
    }

    [HttpPut("{id}")]    
    public async Task<IActionResult> Edit(int id, FabricanteDto fabricanteDto)
    {
        var updated = await _fabricanteService.UpdateAsync(_mapper.Map<Fabricante>(fabricanteDto));
        if (updated == null) return NotFound();
        return Ok(updated);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await _fabricanteService.DeleteAsync(id);
        if (!result) return NotFound();
        return NoContent();
    }
}