using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SkopiaProjetos.Models;
using SkopiaProjetos.Interfaces;

namespace SkopiaProjetos.Services
{
    public class RelatorioService : IRelatorioService
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IProjetoRepository _projetoRepository;

        public RelatorioService(
            IUsuarioRepository usuarioRepository,
            IProjetoRepository projetoRepository)
        {
            _usuarioRepository = usuarioRepository;
            _projetoRepository = projetoRepository;
        }

        public async Task<IEnumerable<object>> RelatorioDesempenhoAsync(int usuarioId)
        {
            var usuario = await _usuarioRepository.ObterUsuarioPorIdAsync(usuarioId);
            if (usuario == null || !string.Equals(usuario.Funcao, "gerente", StringComparison.OrdinalIgnoreCase))
                return null;

            var trintaDiasAtras = DateTime.UtcNow.AddDays(-30);
                                   
            var usuarios = await _usuarioRepository.ListarTodosUsuariosAsync();

            var resultado = new List<object>();

            foreach (var user in usuarios)
            {
                var projetos = await _projetoRepository.ListarProjetosPorUsuarioAsync(user.Id);
                int totalTarefasConcluidas = 0;

                foreach (var projeto in projetos)
                {
                    if (projeto.Tarefas == null)
                        continue;

                    totalTarefasConcluidas += projeto.Tarefas
                        .Count(t => t.Status == StatusTarefa.Concluida &&
                                    t.Historico != null &&
                                    t.Historico.Any(h => h.DataAlteracao >= trintaDiasAtras && h.TipoAlteracao == "Status" && h.ValorNovo == StatusTarefa.Concluida.ToString()));
                }

                resultado.Add(new
                {
                    UsuarioId = user.Id,
                    UsuarioNome = user.Nome,
                    MediaTarefasConcluidas = totalTarefasConcluidas / 30.0
                });
            }

            return resultado;
        }
    }
}