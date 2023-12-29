using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PlataformaEducativa.Models
{
    public class Usuario
    {
        [Key]
        
        public int UsuarioId { get; set; }

        [Required]
        [Display(Name = "Nombre Completo del Usuario")]
        [StringLength(35,ErrorMessage ="Maximo '{0}' y un Minimo '{1}'",ErrorMessageResourceName =null,ErrorMessageResourceType =null,MinimumLength =5)]
        public string Nombre { get; set; }
        [Required]
        [Display(Name ="Apellido Completo")]
        [StringLength(35,ErrorMessage ="Maximo'{0}' y un Minimo '{1}'",ErrorMessageResourceName =null,ErrorMessageResourceType =null,MinimumLength =5)]
        public string Apellido { get; set; }
        [Required]
        [Display(Name ="Nombre de Usuario")]
        [StringLength(20,ErrorMessage ="Maximo '{0}' y un Minimo '{1}'",ErrorMessageResourceName = null,ErrorMessageResourceType =null,MinimumLength =5)]
        public string UserName { get; set; }
        [Required]  
        [DataType(DataType.Password)]
        [StringLength(100,ErrorMessage ="Maximo '{0}' y un minimo '{1}'",ErrorMessageResourceName =null,ErrorMessageResourceType =null,MinimumLength =5)]
        [Display(Name ="Contraseña")]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime FechaNacimiento { get; set; }

        [Required]
        [EmailAddress]
        [DataType (DataType.EmailAddress)]
        public string Correo { get; set; }

        [Required(ErrorMessage ="Numero de Telefono Requerido")]
        public string Telefono { get; set; }

        public char? Genero { get; set; }
        [Required]
        public DateTime FechaCreacion { get; set; }

        [Required]
        [StringLength(50,ErrorMessage ="Maximo de Caracteres '{0}' y Minimo {1}",ErrorMessageResourceName =null,ErrorMessageResourceType =null,MinimumLength =10)]
        public string Direccion { get; set; }

        [Required]
        [StringLength(25)]
        public string Municipio {  get; set; }
        [StringLength(40)]
        public string? Sector { get; set; }

        [ForeignKey("RolesId")]
        public Roles Roles { get; set; }

        [Required]
        
        public int RolesId { get; set; }

        public byte Confirmar { get; set; }

        [ForeignKey("InstitucionesId")]
        public Instituciones? Instituciones { get; set; }
        public int? InstitucionesId { get; set; }
        public string? Cedula{ get; set;}
        public string ? Foto { get; set; }
        
    }
}
