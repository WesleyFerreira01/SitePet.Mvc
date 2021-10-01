using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SitePet.Mvc.Models
{
    public class Pet
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Raca { get; set; }
        public Tipo Tipo { get; set; }
        public Genero Genero { get; set; }
        public string Imagem { get; set; }
    }

    public enum Tipo
    {
        Gato = 1,
        Cachorro
    }

    public enum Genero
    {
        Femea = 1,
        Macho
    }
}
