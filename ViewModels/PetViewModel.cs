using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SitePet.Mvc.ViewModels
{
    public class PetViewModel
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(20, ErrorMessage = "O {0} dever ter de {1} ate {2} caracteres", MinimumLength = 3)]
        public string Nome { get; set; }

        [Required]
        [StringLength(20, ErrorMessage = "O {0} dever ter de {1} ate {2} caracteres", MinimumLength = 3)]
        [DisplayName("Raça")]
        public string Raca { get; set; }

        [Required]
        public int Tipo { get; set; }

        [Required]
        public int Genero { get; set; }

        [NotMapped]
        [DisplayName("Imagem do Pet")]
        public IFormFile ImagemUpload { get; set; }
        public string Imagem { get; set; }
    }
}
