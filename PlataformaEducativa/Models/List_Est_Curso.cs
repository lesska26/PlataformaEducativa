using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PlataformaEducativa.Models
{
    public class List_Est_Curso
    {
        [Key]
        public int List_Est_CursoId { get; set; }

        [ForeignKey("CursoId")]
        public Cursos Cursos { get; set; }
        public int CursoId { get; set; }
        [ForeignKey("InstitucioneId")]
        public Instituciones Instituciones { get; set; }
        public int InstitucioneId { get; set; }

        public DateTime Fecha { get; set; }

        [ForeignKey("EstudiantesId")]
        public Estudiantes Estudiantes { get; set; }
        public int EstudiantesId { get; set; }
    }
}
