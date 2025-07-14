using System;
using System.ComponentModel.DataAnnotations;
using SkopiaProjetos.Models;

namespace SkopiaProjetos.Models.Dtos
{
    public class TarefaCreateDto
    {
        [Required]
        public int ProjetoId { get; set; }
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
    }
}