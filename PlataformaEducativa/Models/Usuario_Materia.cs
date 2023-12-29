using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PlataformaEducativa.Models
{
    public class Usuario_Materia
    {
        [Key]
        public int Usuario_MateriaId { get; set; }
        [ForeignKey("CursosId")]
        public Cursos Cursos { get; set; }

        [Required]

        public int CursosId { get; set; }
        [ForeignKey("UsuarioId")] 
        public Usuario Usuario { get; set; }

        [Required]
        
        public int UsuarioId { get; set; }
    }
}
