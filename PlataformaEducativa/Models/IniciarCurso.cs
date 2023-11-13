using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PlataformaEducativa.Models
{
    public class IniciarCurso
    {
        [Key]
        public int IniciarCursoId { get; set; }

        [ForeignKey("CursosId")]
        public Cursos Cursos { get; set; }

        [Required]
        public int CursosId { get;set; }

        [ForeignKey("InstitucionesId")]
        public Instituciones Instituciones { get; set;}
        [Required]
        public int  InstitucionesId { get; set; }

        [Required]
        [Display(Name ="Fecha de Inicio")]
        [DataType(DataType.Date)]
        public DateTime FechaIniciar { get; set; }

        [Required]
        [Display(Name ="Hora Iniciar de la clase")]
        [DataType(DataType.Duration)]
        public DateTime HoraIniciar { get; set; }

        [Required]
        [Display(Name ="Hora Terminar de la clase")]
        [DataType(DataType.Duration)]
        public DateTime HoraTerminar { get; set; }

        public byte Activo { get; set; }


    }
}
