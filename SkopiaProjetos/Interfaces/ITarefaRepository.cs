using System.Collections.Generic;
using System.Threading.Tasks;
using SkopiaProjetos.Models;

namespace SkopiaProjetos.Interfaces
{
    public interface ITarefaRepository
    {
        Task<IEnumerable<Tarefa>> ListarTarefasPorProjetoAsync(int projetoId);
        Task<Tarefa> CriarTarefaAsync(Tarefa tarefa);
        Task<Tarefa> ObterTarefaPorIdAsync(int id);
        Task<bool> AtualizarTarefaAsync(Tarefa tarefa);
        Task<bool> RemoverTarefaAsync(Tarefa tarefa);
    }
}   