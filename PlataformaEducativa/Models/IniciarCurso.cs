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

        
        [Display(Name ="Fecha de Inicio")]
        [DataType(DataType.Date)]
        public DateTime? FechaIniciar { get; set; }

        
        [Display(Name ="Hora Iniciar de la clase")]
        [DataType(DataType.Duration)]
        public TimeSpan? HoraIniciar { get; set; }

        
        [Display(Name ="Hora Terminar de la clase")]
        [DataType(DataType.Duration)]
        public TimeSpan ?HoraTerminar { get; set; }

        public byte Activo { get; set; }
        public byte? Termino { get; set; }
        [ForeignKey(nameof(HoraId))]
        public Hora Hora { get; set; }
        
        public int HoraId { get; set; }

        public DateTime? Finaliza { get; set; }

        public DateTime? Finalizo { get; set; }
       

    }
}
