using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SkopiaProjetos.Data;
using SkopiaProjetos.Models;
using SkopiaProjetos.Interfaces;

namespace SkopiaProjetos.Repository
{
    public class ProjetoRepository : IProjetoRepository
    {
        private readonly AppDbContext _context;

        public ProjetoRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Projeto>> ListarProjetosPorUsuarioAsync(int usuarioId)
        {
            return await _context.Projetos
                .Include(p => p.Tarefas)
                .Where(p => p.UsuarioId == usuarioId)
                .ToListAsync();
        }

        public async Task<Projeto> CriarProjetoAsync(Projeto projeto)
        {
            _context.Projetos.Add(projeto);
            await _context.SaveChangesAsync();
            return projeto;
        }

        public async Task<Projeto> ObterProjetoPorIdAsync(int id)
        {
            return await _context.Projetos
                .Include(p => p.Tarefas)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<bool> RemoverProjetoAsync(Projeto projeto)
        {
            _context.Projetos.Remove(projeto);
            return await _context.SaveChangesAsync() > 0;
        }
    }
}