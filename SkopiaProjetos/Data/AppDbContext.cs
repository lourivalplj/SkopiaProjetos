using Microsoft.EntityFrameworkCore;
using SkopiaProjetos.Models;

namespace SkopiaProjetos.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Projeto> Projetos { get; set; }        
        public DbSet<Tarefa> Tarefas { get; set; }
        public DbSet<ComentarioTarefa> ComentariosTarefa { get; set; }
        public DbSet<HistoricoTarefa> HistoricosTarefa { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
                        
            modelBuilder.Entity<ComentarioTarefa>()
                .HasOne(c => c.Usuario)
                .WithMany()
                .HasForeignKey(c => c.UsuarioId)
                .OnDelete(DeleteBehavior.Restrict);
                        
            modelBuilder.Entity<ComentarioTarefa>()
                .HasOne(c => c.Tarefa)
                .WithMany(t => t.Comentarios)
                .HasForeignKey(c => c.TarefaId)
                .OnDelete(DeleteBehavior.Cascade);
                        
            modelBuilder.Entity<HistoricoTarefa>()
                .HasOne(h => h.Usuario)
                .WithMany()
                .HasForeignKey(h => h.UsuarioId)
                .OnDelete(DeleteBehavior.Restrict);
                        
            modelBuilder.Entity<HistoricoTarefa>()
                .HasOne(h => h.Tarefa)
                .WithMany(t => t.Historico)
                .HasForeignKey(h => h.TarefaId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}