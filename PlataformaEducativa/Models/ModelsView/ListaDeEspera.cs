namespace PlataformaEducativa.Models.ModelsView
{
    public class ListaDeEspera
    {
        public int? EstudiantesId { get; set; }

        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public DateTime Edad { get; set; }
        public string Cedula { get; set; }
        public string Telefono { get; set; }
        public char Sexo { get; set; }
    }
}
