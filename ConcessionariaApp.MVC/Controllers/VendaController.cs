using AutoMapper;
using ConcessionariaApp.Application.Dtos;
using ConcessionariaApp.Application.Interfaces.Services;
using ConcessionariaApp.Models;
using ConcessionariaApp.Services;
using Microsoft.AspNetCore.Mvc;

[Route("[controller]")]
[Controller]
public class VendaController : Controller
{
    private readonly IVendaService _vendaService;
    private readonly IVeiculoService _veiculoService;
    private readonly IConcessionariaService _concessionariaService;
    private readonly IMapper _mapper;

    public VendaController(IVendaService vendaService,
                            IVeiculoService veiculoService,
                            IConcessionariaService concessionariaService,
                            IClienteService clienteService,
                            IMapper mapper)
    {
        _vendaService = vendaService;
        _veiculoService = veiculoService;
        _concessionariaService = concessionariaService;   
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var vendas = await _vendaService.GetAllAsync();
        return View(_mapper.Map<IEnumerable<VendaDto>>(vendas));
    }

    [HttpGet("details/{id}")]
    public async Task<IActionResult> Details(int id)
    {
        var venda = await _vendaService.GetByIdAsync(id);

        return View("Details", _mapper.Map<VendaDto>(venda));
    }
  
    [HttpGet("{id}")]
    public async Task<IActionResult> Edit(int id)
    {
        var venda = await _vendaService.GetByIdAsync(id);
        if (venda == null) 
            return NotFound();
       
        return View(_mapper.Map<VendaDto>(venda));
    }

    [HttpGet("create")]
    public async Task<IActionResult> Create()
    {      
        ViewBag.Veiculos = _mapper.Map<IEnumerable<VeiculoDto>>(await _veiculoService.GetAllAsync());   
        ViewBag.Concessionarias = _mapper.Map<IEnumerable<ConcessionariaDto>>(await _concessionariaService.GetAllAsync());
        return View();
    }

    [HttpPost("create")]
    public async Task<IActionResult> Create([FromBody] VendaDto vendaDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(new { message = "Dados inválidos", errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage) });
        }       
        await _vendaService.AddAsync(_mapper.Map<Venda>(vendaDto));   
        return Ok(new { message = "Venda cadastrada com sucesso!" });
    }    

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var success = await _vendaService.DeleteAsync(id);
        if (!success) 
            return NotFound();

        return View("Index");        
    }
}
