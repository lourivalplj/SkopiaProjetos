using System.Threading.Tasks;
using SkopiaProjetos.Models;

namespace SkopiaProjetos.Interfaces
{
    public interface IComentarioTarefaService
    {
        Task<ComentarioTarefa> AdicionarComentarioAsync(int tarefaId, ComentarioTarefa comentario);
    }
}