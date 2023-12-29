using System.ComponentModel.DataAnnotations;

namespace PlataformaEducativa.Models.ModelsView
{
    public class LoginView
    {
        [Required]
        [Display(Name ="Nombre de Usuario")]
        public string Users { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
