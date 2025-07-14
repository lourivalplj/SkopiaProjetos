using System.Collections.Generic;
using System.Threading.Tasks;
using SkopiaProjetos.Models;

namespace SkopiaProjetos.Interfaces
{
    public interface IProjetoService
    {
        Task<IEnumerable<Projeto>> ListarProjetosAsync(int usuarioId);
        Task<Projeto> CriarProjetoAsync(Projeto projeto);
        Task<bool> RemoverProjetoAsync(int id);
    }
}