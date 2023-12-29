namespace PlataformaEducativa.Models.ModelsView
{
    public class EstudiantesDetalles
    {
        public int EstudiantesId { get; set; }

        public string Nombre { get; set; }

        public Instituciones Instituciones { get; set; }

        public List<CursoParticipante> Participantees { get; set; }

        public List<CursoNota> CursoNotas { get; set; }

        public List<Usuario> Usuarios { get; set; }

        public DateTime Fecha { get; set; }

        public List<Cursos> cursos { get; set; }
    }
}
