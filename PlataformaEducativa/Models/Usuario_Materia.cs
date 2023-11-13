using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PlataformaEducativa.Models
{
    public class Usuario_Materia
    {
        [Key]
        public int Usuario_MateriaId { get; set; }
        [ForeignKey("MateriaId")]
        public Materia Materia { get; set; }

        [Required]

        public int MateriaId { get; set; }
        [ForeignKey("UsuarioId")] 
        public Usuario Usuario { get; set; }

        [Required]
        
        public int UsuarioId { get; set; }
    }
}
