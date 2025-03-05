using AutoMapper;
using ConcessionariaApp.Application.Dtos;
using ConcessionariaApp.Application.Interfaces.Services;
using ConcessionariaApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace ConcessionariaApp.MVC.Controllers;

[Route("[controller]")]
[ApiController]
public class ConcessionariaController : Controller
{    
    private readonly IConcessionariaService _concessionariaService;
    private readonly IMapper _mapper;

    public ConcessionariaController(IConcessionariaService concessionariaService, IMapper mapper)
    {
        _concessionariaService = concessionariaService;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {        
        var concessionarias = await _concessionariaService.GetAllAsync();        
        return View(_mapper.Map<IEnumerable<ConcessionariaDto>>(concessionarias));
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var concessionaria = await _concessionariaService.GetByIdAsync(id);

        if (concessionaria == null) 
            return NotFound();
        
        return Ok(_mapper.Map<ConcessionariaDto>(concessionaria));
    }

    [HttpGet("details/{id}")]
    public async Task<IActionResult> Details(int id)
    {
        var concessionaria = await _concessionariaService.GetByIdAsync(id);      
        return View("Details", _mapper.Map<ConcessionariaDto>(concessionaria));
    }

    [HttpGet("create")]
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]    
    public async Task<IActionResult> Create(ConcessionariaDto concessionaria)
    {
        if (!ModelState.IsValid) 
            return BadRequest(ModelState);
       
        var success = await _concessionariaService.AddAsync(_mapper.Map<Concessionaria>(concessionaria));
        if (!success) 
            return Conflict("Nome da concessionária já existe.");
        
        return Ok(new { message = "Concessionaria cadastrada com sucesso!" });
    } 

    [HttpGet("edit/{id}")]
    public async Task<IActionResult> Edit(int id)
    {        
        var concessionaria = await _concessionariaService.GetByIdAsync(id);
        return View(_mapper.Map<ConcessionariaDto>(concessionaria));
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, ConcessionariaDto concessionaria)
    {
        if (id != concessionaria.Id) 
            return BadRequest("ID inconsistente.");

        var success = await _concessionariaService.UpdateAsync(_mapper.Map<Concessionaria>(concessionaria));
        if (!success) 
            return NotFound();

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var success = await _concessionariaService.DeleteAsync(id);
        if (!success) 
            return NotFound();

        return NoContent();
    }
}
