using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PlataformaEducativa.Models
{
    public class Cursos
    {
        public int CursosId { get; set; }
        [Required(ErrorMessage ="No puede dejar el Campo Vacio")]
        [Display(Name ="Nombre del Curso")]
        [StringLength(100)]
        public string CursosName { get; set;}

        [Required]
        [DataType(DataType.Duration)]
        public DateTime Duracion { get; set; }

        [ForeignKey("UsuarioId")]
        public Usuario Usuario { get; set; }

        [Required]

        public int UsuarioId { get; set; }
    }
}
