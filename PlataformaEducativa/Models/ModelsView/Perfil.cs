namespace PlataformaEducativa.Models.ModelsView
{
    public class Perfil
    {
        public int Id { get; set; }

        public string Nombre { get; set; }  

        public string Apellido { get; set; }    

        public string Correo { get; set; }  

        public IFormFile Imagen { get; set; }
        public string Password { get; set; }
    }
}
