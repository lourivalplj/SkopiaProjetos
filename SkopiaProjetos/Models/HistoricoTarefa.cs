using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace SkopiaProjetos.Models
{
    public class HistoricoTarefa
    {
        public int Id { get; set; }
        [Required]
        public int TarefaId { get; set; }

        [JsonIgnore] 
        public Tarefa Tarefa { get; set; }

        [Required]
        public int UsuarioId { get; set; }
        public Usuario Usuario { get; set; }
        [Required]
        public string TipoAlteracao { get; set; }
        [Required]
        public string ValorAntigo { get; set; }
        [Required]
        public string ValorNovo { get; set; }
        [Required]
        public DateTime DataAlteracao { get; set; }
    }
}