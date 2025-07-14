using Microsoft.AspNetCore.Mvc;
using SkopiaProjetos.Models;
using SkopiaProjetos.Models.Dtos;
using SkopiaProjetos.Interfaces;
using System.Threading.Tasks;

[ApiController]
[Route("api/[controller]")]
public class TarefasController : ControllerBase
{
    private readonly ITarefaService _tarefaService;

    public TarefasController(ITarefaService tarefaService)
    {
        _tarefaService = tarefaService;
    }

    
    [HttpGet("Projeto/{projetoId}")]
    public async Task<IActionResult> ListarTarefas(int projetoId)
    {
        var tarefas = await _tarefaService.ListarTarefasAsync(projetoId);
        return Ok(tarefas);
    }

    
    [HttpPost]
    public async Task<IActionResult> CriarTarefa([FromBody] TarefaCreateDto dto)
    {
        var tarefa = new Tarefa
        {
            ProjetoId = dto.ProjetoId,
            Titulo = dto.Titulo,
            Descricao = dto.Descricao,
            DataVencimento = dto.DataVencimento,
            Status = dto.Status,
            Prioridade = dto.Prioridade
        };

        var novaTarefa = await _tarefaService.CriarTarefaAsync(dto.ProjetoId, tarefa);
        if (novaTarefa == null)
            return BadRequest("O projeto não existe ou atingiu o limite máximo de 20 tarefas.");
        return CreatedAtAction(nameof(ListarTarefas), new { projetoId = dto.ProjetoId }, novaTarefa);
    }

    
    [HttpPut("{id}")]
    public async Task<IActionResult> AtualizarTarefa(int id, [FromBody] TarefaUpdateDto dto, [FromQuery] int usuarioId)
    {
        if (id != dto.TarefaId)
            return BadRequest("O id da URL não corresponde ao id da tarefa.");

        var tarefaAtual = await _tarefaService.ObterTarefaPorIdAsync(id);
        if (tarefaAtual == null)
            return NotFound("Tarefa não encontrada.");
                
        if (!string.IsNullOrWhiteSpace(dto.Titulo))
            tarefaAtual.Titulo = dto.Titulo;

        if (!string.IsNullOrWhiteSpace(dto.Descricao))
            tarefaAtual.Descricao = dto.Descricao;

        if (dto.DataVencimento != default && dto.DataVencimento > System.DateTime.Now)
            tarefaAtual.DataVencimento = dto.DataVencimento;

        tarefaAtual.Status = dto.Status;

        var atualizado = await _tarefaService.AtualizarTarefaAsync(id, tarefaAtual, usuarioId);
        if (!atualizado)
            return BadRequest("Não é permitido alterar a prioridade da tarefa após a criação ou tarefa não encontrada.");
        return NoContent();
    }

    
    [HttpDelete("{id}")]
    public async Task<IActionResult> RemoverTarefa(int id)
    {
        var removido = await _tarefaService.RemoverTarefaAsync(id);
        if (!removido)
            return NotFound();
        return NoContent();
    }
}