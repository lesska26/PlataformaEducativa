using System.ComponentModel.DataAnnotations.Schema;

namespace PlataformaEducativa.Models
{
    public class DiaSemanaCurso
    {
        public int DiaSemanaCursoId { get; set; }

        public int DiaSemanaId { get; set;}
        [ForeignKey(nameof(DiaSemanaId))]
        public DiaSemana DiaSemana { get; set; }
        public int IniciarCursoId { get; set; }

        [ForeignKey(nameof(IniciarCursoId))]
        public IniciarCurso IniciarCurso { get; set;}
    }
}
