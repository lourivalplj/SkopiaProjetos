using Microsoft.EntityFrameworkCore;

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

        // Relacionamentos e restrições podem ser configurados aqui, se necessário.
    }
}