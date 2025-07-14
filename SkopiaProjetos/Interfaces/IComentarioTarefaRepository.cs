using System.Threading.Tasks;
using SkopiaProjetos.Models;

namespace SkopiaProjetos.Interfaces
{
    public interface IComentarioTarefaRepository
    {
        Task<ComentarioTarefa> AdicionarComentarioAsync(ComentarioTarefa comentario);
    }
}