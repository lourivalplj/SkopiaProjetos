using System.Collections.Generic;
using System.Threading.Tasks;
using SkopiaProjetos.Models;

namespace SkopiaProjetos.Interfaces
{
    public interface ITarefaService
    {
        Task<IEnumerable<Tarefa>> ListarTarefasAsync(int projetoId);
        Task<Tarefa> CriarTarefaAsync(int projetoId, Tarefa tarefa);
        Task<bool> AtualizarTarefaAsync(int id, Tarefa tarefaAtualizada, int usuarioId);
        Task<bool> RemoverTarefaAsync(int id);
        Task<Tarefa> ObterTarefaPorIdAsync(int id);
    }
}