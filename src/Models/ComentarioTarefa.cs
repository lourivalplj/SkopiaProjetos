using System;

public class ComentarioTarefa
{
    public int Id { get; set; }
    public int TarefaId { get; set; }
    public Tarefa Tarefa { get; set; }

    public int UsuarioId { get; set; }
    public Usuario Usuario { get; set; }

    public string Conteudo { get; set; }
    public DateTime CriadoEm { get; set; }
}