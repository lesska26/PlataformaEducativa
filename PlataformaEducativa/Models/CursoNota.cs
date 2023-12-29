using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PlataformaEducativa.Models
{
    public class CursoNota
    {
        [Key]
        public int CursoNotaId { get; set; }

        [ForeignKey("IniciarCursoId")]
        public IniciarCurso IniciarCurso { get; set; }

        [Required]
        public int IniciarCursoId { get; set; }
        [ForeignKey("EstudiantesId")]
        public Estudiantes Estudiantes { get; set;}
        [Required]
        public int EstudiantesId { get;set; }

        [Required]
        public float Nota { get; set; }

        public DateTime Fecha { get; set; }

        public char? Status { get; set; } 
       







    }
}
