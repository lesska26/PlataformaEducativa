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

        [ForeignKey("UsuarioId")]
        public Usuario Usuario { get; set; }

        [Required]
        public int UsuarioId { get; set; }
        [ForeignKey("EstudianteId")]
        public Estudiantes Estudiantes { get; set;}
        [Required]
        public int EstudiantesId { get;set; }
        [ForeignKey("MateriaId")]
        public Materia Materia { get; set; }

        [Required]

        public int MateriaId { get; set; }
        [Required]
        public float Nota { get; set; }

        public DateTime Fecha { get; set; }







    }
}
