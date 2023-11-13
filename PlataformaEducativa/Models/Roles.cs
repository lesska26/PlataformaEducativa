using System.ComponentModel.DataAnnotations;

namespace PlataformaEducativa.Models
{
    public class Roles
    {
        public int RolesId{get;set;}

        [Required]
        [StringLength(25)]
        [Display(Name ="Nombre del Rol")]
        public string RoleName { get;set;}
    }
}
