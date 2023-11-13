using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace PlataformaEducativa.Models
{
    public class Materia
    {
        [Key]
        public int MateriaId{get;set;}

        [Required(ErrorMessage="No puedes dejar el Campo Mataria Vacio")]
        [StringLength(50)]
        [Display(Name="Nombre de la Materia")]
        public string MateriaName { get; set;}

        [Required]
        [Display(Name ="Horas de la Materia")]
        [DataType(DataType.Duration)]
        public DateTime Hora { get; set; }
    }
}
