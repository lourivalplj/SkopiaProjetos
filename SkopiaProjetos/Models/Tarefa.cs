using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace SkopiaProjetos.Models
{
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
        [Required]
        public int ProjetoId { get; set; }

        [JsonIgnore] 
        public Projeto Projeto { get; set; }
        [Required]
        public string Titulo { get; set; }
        [Required]
        public string Descricao { get; set; }
        [Required]
        public DateTime DataVencimento { get; set; }
        [Required]
        public StatusTarefa Status { get; set; }
        [Required]
        public PrioridadeTarefa Prioridade { get; set; }
        public ICollection<ComentarioTarefa> Comentarios { get; set; }
        public ICollection<HistoricoTarefa> Historico { get; set; }
    }
}