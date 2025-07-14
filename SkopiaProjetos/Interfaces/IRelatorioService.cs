using System.Threading.Tasks;
using System.Collections.Generic;


namespace SkopiaProjetos.Interfaces
{ 
    public interface IRelatorioService
    {
        Task<IEnumerable<object>> RelatorioDesempenhoAsync(int usuarioId);
    }
}