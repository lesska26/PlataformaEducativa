using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PlataformaEducativa.Models
{
    public class Sector
    {
        [Key]
        public int SectorId { get; set; }

        [Required]
        [Display(Name ="Sector")]
        public string SectorName { get; set;}
        [ForeignKey("MunicipioId")]
        public Municipio Municipio { get; set; }

        [Required]
       

        public int MunicipioId { get; set; }
    }
}
