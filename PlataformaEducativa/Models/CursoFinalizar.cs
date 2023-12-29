using System.ComponentModel.DataAnnotations.Schema;

namespace PlataformaEducativa.Models
{
    public class CursoFinalizar
    {
       public int CursoFinalizarId { get; set; }

       [ForeignKey("IniciarCursoId")]
       public IniciarCurso IniciarCurso { get; set; }

      public  int IniciarCursoId { get; set; }
      public DateTime FinalizoCurso { get; set; }
    }
}
