using System.Collections.Generic;


namespace SkopiaProjetos.Models
{ 
    public class Usuario
    {    
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Funcao { get; set; } 
        public ICollection<Projeto> Projetos { get; set; }
    }
}