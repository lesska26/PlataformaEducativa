using System.ComponentModel.DataAnnotations;

namespace PlataformaEducativa.Models
{
    public class Instituciones
    {
        public int InstitucionesId {get;set;}

        [Required]
        public string Nombre { get;set;}
        [Required]
        public string Municipio { get; set;}
        [Required]
        public string Sector { get; set;}
        [Required]
        public string Direccion { get; set;}
        [Required]
        public DateTime FechaRegistro { get; set; }



    }
}
