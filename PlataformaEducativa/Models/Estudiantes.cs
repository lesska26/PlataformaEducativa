using Microsoft.Extensions.Diagnostics.HealthChecks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PlataformaEducativa.Models
{
    public class Estudiantes
    {
        [Key]
        public int  EstudiantesId{get;set;}
        [Required]
        [StringLength(25,ErrorMessage="Caracteres Maximo '{0}' y Caracteres Minimos '{1}'",ErrorMessageResourceName =null,ErrorMessageResourceType =null,MinimumLength =5)]
        [Display(Name ="Nombre Completo")]
        public string Nombre { get; set; }

        [Required]
        [StringLength(35,ErrorMessage = "Caracteres Maximo '{0}' y Caracteres Minimos '{1}'",ErrorMessageResourceName =null,ErrorMessageResourceType =null,MinimumLength =5)]
        [Display(Name ="Apellido Completo ")]
        public string Apellido { get; set; }
        [Required]
        //qui va una expresion regular para validar la cedula
        public string? Cedula { get; set; }

        [Required]
        public string? Matricula { get; set; }
        [Required]
        [DataType(DataType.Date)]
        [Display(Name ="Fecha de Nacimiento")]
        public DateTime FechaDeNacimiento { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        [EmailAddress]
        public string Correo { get; set; }
        [Required]
        
        public char Genero { get; set; }
        [Required]
        public string Municipio { get; set; }
       
        public DateTime FechaCreacion { get; set; }
        [Required]
        [Display(Name ="Estado del Estudiante")]
        public string Status { get; set; }
        [ForeignKey("UsuarioId")]
        public Usuario? Usuario { get; set;}

        [Required]
        public int UsuarioId { get; set; }

        public string Telefono { get; set; }

        public bool Verificar { get; set; }

        [ForeignKey("InstitucionesId")]
        public Instituciones? Instituciones { get; set; }
        public int? InstitucionesId { get; set; }
    }
}
