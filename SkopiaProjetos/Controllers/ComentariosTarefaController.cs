using Microsoft.AspNetCore.Mvc;
using SkopiaProjetos.Models;
using SkopiaProjetos.Models.Dtos;
using SkopiaProjetos.Interfaces;
using System.Threading.Tasks;

[ApiController]
[Route("api/[controller]")]
public class ComentariosTarefaController : ControllerBase
{
    private readonly IComentarioTarefaService _comentarioTarefaService;

    public ComentariosTarefaController(IComentarioTarefaService comentarioTarefaService)
    {
        _comentarioTarefaService = comentarioTarefaService;
    }

    [HttpPost]
    public async Task<IActionResult> AdicionarComentario([FromBody] ComentarioTarefaCreateDto dto, [FromQuery] int usuarioId)
    {
        var comentario = new ComentarioTarefa
        {
            TarefaId = dto.TarefaId,
            Conteudo = dto.Conteudo,            
            UsuarioId = usuarioId
        };

        var result = await _comentarioTarefaService.AdicionarComentarioAsync(dto.TarefaId, comentario);
        if (result == null)
            return NotFound();
        return Ok(result);
    }
}