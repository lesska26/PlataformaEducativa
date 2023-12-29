using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PlataformaEducativa.Models
{
    public class Cursos_en_Curso
    {
        [Key]
        public int Cursos_en_CursoId { get; set; }

        [ForeignKey("IniciarCursoId")]
        public IniciarCurso IniciarCurso { get; set; }


        public int IniciarCursoId { get; set; }

        public DateTime Fecha { get; set; }
    }
}
