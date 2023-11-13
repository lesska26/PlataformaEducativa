using System.ComponentModel.DataAnnotations;

namespace PlataformaEducativa.Models
{
    public class Municipio
    {
        [Key]
        public int MunicipioId { get; set; }

        [Required]
        [Display(Name ="Municipio")]
        public string MunicipioName { get; set;}
    }
}
