using Microsoft.Extensions.DependencyInjection;
using SkopiaProjetos.Services;
using SkopiaProjetos.Interfaces;
using SkopiaProjetos.Repository;

namespace SkopiaProjetos.Services
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddSkopiaServices(this IServiceCollection services)
        {
            
            services.AddScoped<IProjetoService, ProjetoService>();
            services.AddScoped<ITarefaService, TarefaService>();
            services.AddScoped<IComentarioTarefaService, ComentarioTarefaService>();
            services.AddScoped<IRelatorioService, RelatorioService>();

            
            services.AddScoped<IProjetoRepository, ProjetoRepository>();
            services.AddScoped<ITarefaRepository, TarefaRepository>();
            services.AddScoped<IComentarioTarefaRepository, ComentarioTarefaRepository>();
            services.AddScoped<IHistoricoTarefaRepository, HistoricoTarefaRepository>();
            services.AddScoped<IUsuarioRepository, UsuarioRepository>();

            return services;
        }
    }
}