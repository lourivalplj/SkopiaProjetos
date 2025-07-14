using System;
using System.ComponentModel.DataAnnotations;
using SkopiaProjetos.Models;

namespace SkopiaProjetos.Models.Dtos
{
    public class TarefaUpdateDto
    {
        [Required]
        public int TarefaId { get; set; }
        public string Titulo { get; set; }
        public string Descricao { get; set; }
        public DateTime DataVencimento { get; set; }
        [Required]
        public StatusTarefa Status { get; set; }
    }
}