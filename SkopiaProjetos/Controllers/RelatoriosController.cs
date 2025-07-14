using Microsoft.AspNetCore.Mvc;
using SkopiaProjetos.Interfaces;
using System.Threading.Tasks;

[ApiController]
[Route("api/[controller]")]
public class RelatoriosController : ControllerBase
{
    private readonly IRelatorioService _relatorioService;

    public RelatoriosController(IRelatorioService relatorioService)
    {
        _relatorioService = relatorioService;
    }

    
    [HttpGet("Desempenho")]
    public async Task<IActionResult> RelatorioDesempenho([FromQuery] int usuarioId)
    {
        var resultado = await _relatorioService.RelatorioDesempenhoAsync(usuarioId);
        if (resultado == null)
            return NotFound();
        return Ok(resultado);
    }
}