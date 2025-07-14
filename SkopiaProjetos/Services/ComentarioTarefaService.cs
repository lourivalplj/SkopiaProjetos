using System.Threading.Tasks;
using SkopiaProjetos.Models;
using SkopiaProjetos.Interfaces;

namespace SkopiaProjetos.Services
{
    public class ComentarioTarefaService : IComentarioTarefaService
    {
        private readonly IComentarioTarefaRepository _comentarioTarefaRepository;
        private readonly IHistoricoTarefaRepository _historicoTarefaRepository;

        public ComentarioTarefaService(
            IComentarioTarefaRepository comentarioTarefaRepository,
            IHistoricoTarefaRepository historicoTarefaRepository)
        {
            _comentarioTarefaRepository = comentarioTarefaRepository;
            _historicoTarefaRepository = historicoTarefaRepository;
        }

        public async Task<ComentarioTarefa> AdicionarComentarioAsync(int tarefaId, ComentarioTarefa comentario)
        {
            comentario.TarefaId = tarefaId;
            comentario.CriadoEm = System.DateTime.UtcNow;
            var result = await _comentarioTarefaRepository.AdicionarComentarioAsync(comentario);
                        
            await _historicoTarefaRepository.AdicionarHistoricoAsync(new HistoricoTarefa
            {
                TarefaId = tarefaId,
                UsuarioId = comentario.UsuarioId,
                TipoAlteracao = "Comentario",
                ValorAntigo = "",
                ValorNovo = comentario.Conteudo,
                DataAlteracao = System.DateTime.UtcNow
            });

            return result;
        }
    }
}