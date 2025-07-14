using System;
using System.ComponentModel.DataAnnotations;

namespace SkopiaProjetos.Models.Dtos
{
    public class ComentarioTarefaCreateDto
    {
        [Required]
        public int TarefaId { get; set; }
        [Required]
        public string Conteudo { get; set; }
        
    }
}