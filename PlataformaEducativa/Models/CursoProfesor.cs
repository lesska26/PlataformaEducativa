using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PlataformaEducativa.Models
{
    public class CursoProfesor
    {
        [Key]
        public int CursoProfesorId { get; set; }
        [ForeignKey("IniciarCursoId")]
        public IniciarCurso IniciarCurso { get; set; }

        public int IniciarCursoId { get; set; }
        [ForeignKey("UsuarioId")]
        public Usuario Usuario { get; set;}

        public int UsuarioId { get; set;}

        public bool Termino { get; set; }

    }
}
