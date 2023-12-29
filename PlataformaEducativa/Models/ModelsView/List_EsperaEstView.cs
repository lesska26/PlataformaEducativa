namespace PlataformaEducativa.Models.ModelsView
{
    public class List_EsperaEstView
    {
        public int EstudiantesId { get; set; }

        public List<Cursos> Cursos { get; set; }

        public List<Instituciones> Instituciones { get; set; }

        public DateTime Fecha { get; set; } 
    }
}
