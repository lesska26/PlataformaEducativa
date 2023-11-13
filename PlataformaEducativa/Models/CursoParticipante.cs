using System.ComponentModel.DataAnnotations;

namespace PlataformaEducativa.Models
{
    public class CursoParticipante
    {
        [Key]
       public int CursoParticipanteId { get; set; }
       public Estudiantes Estudiantes {  get; set; }
          
        public int EstudiantesId { get; set; }



    }
}
