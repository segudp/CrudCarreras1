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
    [Table("Carreras")]
    public class Carrera
    {
        public int ID { get; set; }
        public string Nombre { get; set; } = string.Empty;

        [Range(1, 20, ErrorMessage = "Debe estar entre 1 y 20 cuatrimestres")]
        public int Cuatrimestres { get; set; }
        public int InstitutoId { get; set; }
        public Instituto? Instituto { get; set; }
    }
}