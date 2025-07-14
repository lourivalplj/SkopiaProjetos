using System.Threading.Tasks;
using SkopiaProjetos.Data;
using SkopiaProjetos.Models;
using SkopiaProjetos.Interfaces;

namespace SkopiaProjetos.Repository
{
    public class ComentarioTarefaRepository : IComentarioTarefaRepository
    {
        private readonly AppDbContext _context;

        public ComentarioTarefaRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<ComentarioTarefa> AdicionarComentarioAsync(ComentarioTarefa comentario)
        {
            _context.ComentariosTarefa.Add(comentario);
            await _context.SaveChangesAsync();
            return comentario;
        }
    }
}