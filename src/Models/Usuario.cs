using System.Collections.Generic;

public class Usuario
{
    public int Id { get; set; }
    public string Nome { get; set; }
    public string Funcao { get; set; } // Ex: "usuario", "gerente"

    public ICollection<Projeto> Projetos { get; set; }
}