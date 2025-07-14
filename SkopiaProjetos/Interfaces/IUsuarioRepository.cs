using System.Collections.Generic;
using System.Threading.Tasks;
using SkopiaProjetos.Models;

namespace SkopiaProjetos.Interfaces
{
    public interface IUsuarioRepository
    {
        Task<Usuario> ObterUsuarioPorIdAsync(int id);
        Task<IEnumerable<Usuario>> ListarTodosUsuariosAsync();
    }
}