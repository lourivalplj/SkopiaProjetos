using System.Collections.Generic;

public class Projeto
{
    public int Id { get; set; }
    public string Nome { get; set; }
    public int UsuarioId { get; set; }
    public Usuario Usuario { get; set; }

    public ICollection<Tarefa> Tarefas { get; set; }
}