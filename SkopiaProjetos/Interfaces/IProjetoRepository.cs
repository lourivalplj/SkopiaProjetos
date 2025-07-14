using System.Collections.Generic;
using System.Threading.Tasks;
using SkopiaProjetos.Models;

namespace SkopiaProjetos.Interfaces
{
    public interface IProjetoRepository
    {
        Task<IEnumerable<Projeto>> ListarProjetosPorUsuarioAsync(int usuarioId);
        Task<Projeto> CriarProjetoAsync(Projeto projeto);
        Task<Projeto> ObterProjetoPorIdAsync(int id);
        Task<bool> RemoverProjetoAsync(Projeto projeto);
    }
}