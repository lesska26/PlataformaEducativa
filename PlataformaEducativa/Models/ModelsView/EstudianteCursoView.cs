namespace PlataformaEducativa.Models.ModelsView
{
    public class EstudianteCursoView
    {
        public string Nombre { get; set; }
        public string Apellido {  get; set; }
        public string Telefono { get; set; }
        public string Correo { get; set; }
        public DateTime Fecha { get; set; }
        public string Cedulas { get; set; }
        public int IdCurso { get; set; }
        public int IdInstituto { get; set; }

        public int iniciarCursoId { get; set; }

        public char? Genero { get; set; }
    }
}
