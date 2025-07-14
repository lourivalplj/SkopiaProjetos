using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace SkopiaProjetos.Models
{
    public class ComentarioTarefa
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
        public string Conteudo { get; set; }
        [Required]
        public DateTime CriadoEm { get; set; }
    }
}