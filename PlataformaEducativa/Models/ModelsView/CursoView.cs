using System.ComponentModel.DataAnnotations;

namespace PlataformaEducativa.Models.ModelsView
{
    public class CursoView
    {
        public int CursosId { get; set; }
        [Required(ErrorMessage = "No puede dejar el Campo Vacio")]
        [Display(Name = "Nombre del Curso")]
        [StringLength(100)]
        public string CursosName { get; set; }

        [Required]
        [DataType(DataType.Duration)]
        public int Duracion { get; set; }

        [Required]
        [DataType(DataType.MultilineText)]
        public string Descripcion { get; set; }
    }
}
