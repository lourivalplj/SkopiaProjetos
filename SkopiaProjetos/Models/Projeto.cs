using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace SkopiaProjetos.Models
{
    public class Projeto
    {
        public int Id { get; set; }
        [Required]
        public string Nome { get; set; }
        [Required]
        public int UsuarioId { get; set; }

        [JsonIgnore] 
        public Usuario Usuario { get; set; }

        public ICollection<Tarefa> Tarefas { get; set; }
    }
}