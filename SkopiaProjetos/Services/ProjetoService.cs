using System.Collections.Generic;
using System.Threading.Tasks;
using SkopiaProjetos.Models;
using SkopiaProjetos.Interfaces;

namespace SkopiaProjetos.Services
{
    public class ProjetoService : IProjetoService
    {
        private readonly IProjetoRepository _projetoRepository;

        public ProjetoService(IProjetoRepository projetoRepository)
        {
            _projetoRepository = projetoRepository;
        }

        public async Task<IEnumerable<Projeto>> ListarProjetosAsync(int usuarioId)
        {
            return await _projetoRepository.ListarProjetosPorUsuarioAsync(usuarioId);
        }

        public async Task<Projeto> CriarProjetoAsync(Projeto projeto)
        {
            return await _projetoRepository.CriarProjetoAsync(projeto);
        }

        public async Task<bool> RemoverProjetoAsync(int id)
        {
            var projeto = await _projetoRepository.ObterProjetoPorIdAsync(id);
            if (projeto == null)
                return false;
                        
            if (projeto.Tarefas != null && projeto.Tarefas.Any(t => t.Status == StatusTarefa.Pendente))
                return false;

            return await _projetoRepository.RemoverProjetoAsync(projeto);
        }
    }
}