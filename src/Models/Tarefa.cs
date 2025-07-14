using System;
using System.Collections.Generic;

public enum StatusTarefa
{
    Pendente,
    EmAndamento,
    Concluida
}

public enum PrioridadeTarefa
{
    Baixa,
    Media,
    Alta
}

public class Tarefa
{
    public int Id { get; set; }
    public int ProjetoId { get; set; }
    public Projeto Projeto { get; set; }

    public string Titulo { get; set; }
    public string Descricao { get; set; }
    public DateTime DataVencimento { get; set; }
    public StatusTarefa Status { get; set; }
    public PrioridadeTarefa Prioridade { get; set; }

    public ICollection<ComentarioTarefa> Comentarios { get; set; }
    public ICollection<HistoricoTarefa> Historico { get; set; }
}