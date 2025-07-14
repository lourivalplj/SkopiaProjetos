using Microsoft.AspNetCore.Mvc;
using SkopiaProjetos.Models;
using SkopiaProjetos.Interfaces;
using System.Threading.Tasks;
using SkopiaProjetos.Models.Dtos;

[ApiController]
[Route("api/[controller]")]
public class ProjetosController : ControllerBase
{
    private readonly IProjetoService _projetoService;

    public ProjetosController(IProjetoService projetoService)
    {
        _projetoService = projetoService;
    }

    
    [HttpGet]
    public async Task<IActionResult> ListarProjetos([FromQuery] int usuarioId)
    {
        var projetos = await _projetoService.ListarProjetosAsync(usuarioId);
        return Ok(projetos);
    }

    
    [HttpPost]
    public async Task<IActionResult> CriarProjeto([FromBody] ProjetoCreateDto dto)
    {
        var projeto = new Projeto
        {
            Nome = dto.Nome,
            UsuarioId = dto.UsuarioId
        };

        var novoProjeto = await _projetoService.CriarProjetoAsync(projeto);
        return CreatedAtAction(nameof(ListarProjetos), new { usuarioId = novoProjeto.UsuarioId }, novoProjeto);
    }

    /
    [HttpDelete("{id}")]
    public async Task<IActionResult> RemoverProjeto(int id)
    {
        var removido = await _projetoService.RemoverProjetoAsync(id);
        if (!removido)
            return BadRequest("Não é possível remover o projeto enquanto houver tarefas pendentes ou projeto não encontrado.");
        return NoContent();
    }
}