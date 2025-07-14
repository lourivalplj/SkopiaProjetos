using System.Threading.Tasks;
using SkopiaProjetos.Models;

namespace SkopiaProjetos.Interfaces
{
    public interface IHistoricoTarefaRepository
    {
        Task<HistoricoTarefa> AdicionarHistoricoAsync(HistoricoTarefa historico);
    }
}