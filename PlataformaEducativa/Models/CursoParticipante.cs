using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PlataformaEducativa.Models
{
    public class CursoParticipante
    {
        [Key]
       public int CursoParticipanteId { get; set; }
       public Estudiantes Estudiantes {  get; set; }
          
        public int EstudiantesId { get; set; }

        [ForeignKey("IniciarCursoId")]
        public IniciarCurso IniciarCurso { get; set; }

        public int IniciarCursoId { get; set; }

        public char?Status { get; set; }
        [NotMapped]
        public virtual string Nombre { get; set; }

       

    }
}
