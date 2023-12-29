using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PlataformaEducativa.Models
{
    public class Hora
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        public int HoraId { get; set; }
        [Required]
        public string Horas_Iniciar { get; set; }
        [Required]
        
        public string Horas_Final { get; set; }
        public int CatidadHora { get; set; }

       
        public string AMPM { get; set; }

        

        
    }
}
