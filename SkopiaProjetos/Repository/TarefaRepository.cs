using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SkopiaProjetos.Data;
using SkopiaProjetos.Interfaces;
using SkopiaProjetos.Models;


namespace SkopiaProjetos.Repository
{
    public class TarefaRepository : ITarefaRepository
    {
        private readonly AppDbContext _context;

        public TarefaRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Tarefa>> ListarTarefasPorProjetoAsync(int projetoId)
        {
            return await _context.Tarefas
                .Include(t => t.Comentarios)
                .Include(t => t.Historico)
                .Where(t => t.ProjetoId == projetoId)
                .ToListAsync();
        }

        public async Task<Tarefa> CriarTarefaAsync(Tarefa tarefa)
        {
            _context.Tarefas.Add(tarefa);
            await _context.SaveChangesAsync();
            return tarefa;
        }

        public async Task<Tarefa> ObterTarefaPorIdAsync(int id)
        {
            return await _context.Tarefas
                .Include(t => t.Comentarios)
                .Include(t => t.Historico)
                .FirstOrDefaultAsync(t => t.Id == id);
        }

        public async Task<bool> AtualizarTarefaAsync(Tarefa tarefa)
        {
            _context.Tarefas.Update(tarefa);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> RemoverTarefaAsync(Tarefa tarefa)
        {
            _context.Tarefas.Remove(tarefa);
            return await _context.SaveChangesAsync() > 0;
        }
    }
}