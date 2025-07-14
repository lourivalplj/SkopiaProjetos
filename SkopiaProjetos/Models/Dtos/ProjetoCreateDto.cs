using System.ComponentModel.DataAnnotations;

namespace SkopiaProjetos.Models.Dtos
{
    public class ProjetoCreateDto
    {
        [Required]
        public string Nome { get; set; }
        [Required]
        public int UsuarioId { get; set; }
    }
}