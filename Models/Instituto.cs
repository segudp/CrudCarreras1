using CrudCarreras1.Controllers;
using CrudCarreras1.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrudCarreras1.Models
{
    [Table("Institutos")]
    public class Instituto
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Nombre { get; set; } = string.Empty;
        [Required]
        [Display(Name = "Tipo de Instituto")]
        public string TipoInstituto { get; set; } = string.Empty;

        public ICollection<Carrera>? Carreras { get; set; } = new List<Carrera>();

    }
}