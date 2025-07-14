using System;

public class HistoricoTarefa
{
    public int Id { get; set; }
    public int TarefaId { get; set; }
    public Tarefa Tarefa { get; set; }

    public int UsuarioId { get; set; }
    public Usuario Usuario { get; set; }

    public string TipoAlteracao { get; set; } // Ex: "Status", "Descricao", "Comentario"
    public string ValorAntigo { get; set; }
    public string ValorNovo { get; set; }
    public DateTime DataAlteracao { get; set; }
}