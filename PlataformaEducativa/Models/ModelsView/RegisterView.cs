using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PlataformaEducativa.Models.ModelsView
{
    public class RegisterView
    {
        [Required]
        [Display(Name = "Nombre Completo")]
        [StringLength(35, ErrorMessage = "Maximo '{0}' y un Minimo '{1}'", ErrorMessageResourceName = null, ErrorMessageResourceType = null, MinimumLength = 5)]
        public string Nombre { get; set; }
        [Required]
        [Display(Name = "Apellido Completo")]
        [StringLength(35, ErrorMessage = "Maximo'{0}' y un Minimo '{1}'", ErrorMessageResourceName = null, ErrorMessageResourceType = null, MinimumLength = 5)]
        public string Apellido { get; set; }
        
        public string? UserName { get; set; }
        
        public string? Password { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime FechaNacimiento { get; set; }


        [Required]
        [RegularExpression(@"^[a-zA-Z0-9._-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,4}$",ErrorMessage ="Correo Invalido")]
        [EmailAddress]
        [DataType(DataType.EmailAddress)]
        public string Correo { get; set; }

        [Required(ErrorMessage = "Numero de Telefono Requerido")]
        public string Telefono { get; set; }
        [Required]
        [RegularExpression("^[MF]$", ErrorMessage = "El campo Genero debe ser 'M' o 'F'.")]
        public char Genero { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "Maximo de Caracteres '{0}' y Minimo {1}", ErrorMessageResourceName = null, ErrorMessageResourceType = null, MinimumLength = 10)]
        public string Direccion { get; set; }
        /*
        [StringLength(40)]
        public string? Sector { get; set; }
        */
        public byte Confirmar { get; set; }

        [RegularExpression(@"^\d{3}-?\d{7}-?\d{1}$",ErrorMessage ="Combinacion Invalida")]
        [Required]
        public string cedula { get; set; } 
        
        public virtual IEnumerable<Roles> Roles { get; set; }
        public int InstitucionesId { get; set; }
        public virtual IEnumerable<Municipio> Municipios { get; set; }
    }
}
