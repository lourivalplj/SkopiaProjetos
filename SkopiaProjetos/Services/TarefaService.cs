using System.Collections.Generic;
using System.Threading.Tasks;
using SkopiaProjetos.Models;
using SkopiaProjetos.Interfaces;
namespace SkopiaProjetos.Services
{
    public class TarefaService : ITarefaService
    {
        private readonly ITarefaRepository _tarefaRepository;
        private readonly IProjetoRepository _projetoRepository;
        private readonly IHistoricoTarefaRepository _historicoTarefaRepository;

        public TarefaService(
            ITarefaRepository tarefaRepository,
            IProjetoRepository projetoRepository,
            IHistoricoTarefaRepository historicoTarefaRepository)
        {
            _tarefaRepository = tarefaRepository;
            _projetoRepository = projetoRepository;
            _historicoTarefaRepository = historicoTarefaRepository;
        }

        public async Task<IEnumerable<Tarefa>> ListarTarefasAsync(int projetoId)
        {
            return await _tarefaRepository.ListarTarefasPorProjetoAsync(projetoId);
        }

        public async Task<Tarefa> CriarTarefaAsync(int projetoId, Tarefa tarefa)
        {
            var projeto = await _projetoRepository.ObterProjetoPorIdAsync(projetoId);
            if (projeto == null)
                return null;

            if (projeto.Tarefas != null && projeto.Tarefas.Count >= 20)
                return null;

            tarefa.ProjetoId = projetoId;
            return await _tarefaRepository.CriarTarefaAsync(tarefa);
        }

        public async Task<Tarefa> ObterTarefaPorIdAsync(int id)
        {
            return await _tarefaRepository.ObterTarefaPorIdAsync(id);
        }

        public async Task<bool> AtualizarTarefaAsync(int id, Tarefa tarefaAtualizada, int usuarioId)
        {
            var tarefa = await _tarefaRepository.ObterTarefaPorIdAsync(id);
            if (tarefa == null)
                return false;
                        
            if (tarefaAtualizada.Prioridade != tarefa.Prioridade && tarefaAtualizada.Prioridade != 0)
                return false;
            
            if (!string.IsNullOrWhiteSpace(tarefaAtualizada.Titulo))
                tarefa.Titulo = tarefaAtualizada.Titulo;

            if (!string.IsNullOrWhiteSpace(tarefaAtualizada.Descricao))
                tarefa.Descricao = tarefaAtualizada.Descricao;

            if (tarefaAtualizada.DataVencimento != default && tarefaAtualizada.DataVencimento > System.DateTime.Now)
                tarefa.DataVencimento = tarefaAtualizada.DataVencimento;

            tarefa.Status = tarefaAtualizada.Status;
                        
            if (tarefa.Status != tarefaAtualizada.Status)
            {
                await _historicoTarefaRepository.AdicionarHistoricoAsync(new HistoricoTarefa
                {
                    TarefaId = tarefa.Id,
                    UsuarioId = usuarioId,
                    TipoAlteracao = "Status",
                    ValorAntigo = tarefa.Status.ToString(),
                    ValorNovo = tarefaAtualizada.Status.ToString(),
                    DataAlteracao = System.DateTime.UtcNow
                });
                tarefa.Status = tarefaAtualizada.Status;
            }
            if (tarefa.Titulo != tarefaAtualizada.Titulo && !string.IsNullOrWhiteSpace(tarefaAtualizada.Titulo))
            {
                await _historicoTarefaRepository.AdicionarHistoricoAsync(new HistoricoTarefa
                {
                    TarefaId = tarefa.Id,
                    UsuarioId = usuarioId,
                    TipoAlteracao = "Titulo",
                    ValorAntigo = tarefa.Titulo,
                    ValorNovo = tarefaAtualizada.Titulo,
                    DataAlteracao = System.DateTime.UtcNow
                });
                tarefa.Titulo = tarefaAtualizada.Titulo;
            }
            if (tarefa.Descricao != tarefaAtualizada.Descricao && !string.IsNullOrWhiteSpace(tarefaAtualizada.Descricao))
            {
                await _historicoTarefaRepository.AdicionarHistoricoAsync(new HistoricoTarefa
                {
                    TarefaId = tarefa.Id,
                    UsuarioId = usuarioId,
                    TipoAlteracao = "Descricao",
                    ValorAntigo = tarefa.Descricao,
                    ValorNovo = tarefaAtualizada.Descricao,
                    DataAlteracao = System.DateTime.UtcNow
                });
                tarefa.Descricao = tarefaAtualizada.Descricao;
            }
            if (tarefa.DataVencimento != tarefaAtualizada.DataVencimento && tarefaAtualizada.DataVencimento != default && tarefaAtualizada.DataVencimento > System.DateTime.Now)
            {
                await _historicoTarefaRepository.AdicionarHistoricoAsync(new HistoricoTarefa
                {
                    TarefaId = tarefa.Id,
                    UsuarioId = usuarioId,
                    TipoAlteracao = "DataVencimento",
                    ValorAntigo = tarefa.DataVencimento.ToString("o"),
                    ValorNovo = tarefaAtualizada.DataVencimento.ToString("o"),
                    DataAlteracao = System.DateTime.UtcNow
                });
                tarefa.DataVencimento = tarefaAtualizada.DataVencimento;
            }

            return await _tarefaRepository.AtualizarTarefaAsync(tarefa);
        }

        public async Task<bool> RemoverTarefaAsync(int id)
        {
            var tarefa = await _tarefaRepository.ObterTarefaPorIdAsync(id);
            if (tarefa == null)
                return false;

            return await _tarefaRepository.RemoverTarefaAsync(tarefa);
        }
    }
}