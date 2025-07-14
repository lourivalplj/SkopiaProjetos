using System.Threading.Tasks;
using SkopiaProjetos.Data;
using SkopiaProjetos.Models;
using SkopiaProjetos.Interfaces;

namespace SkopiaProjetos.Repository
{
    public class HistoricoTarefaRepository : IHistoricoTarefaRepository
    {
        private readonly AppDbContext _context;

        public HistoricoTarefaRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<HistoricoTarefa> AdicionarHistoricoAsync(HistoricoTarefa historico)
        {
            _context.HistoricosTarefa.Add(historico);
            await _context.SaveChangesAsync();
            return historico;
        }
    }
}