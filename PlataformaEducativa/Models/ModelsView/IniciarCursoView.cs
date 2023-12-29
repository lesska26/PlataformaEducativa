namespace PlataformaEducativa.Models.ModelsView
{
    public class IniciarCursoView
    {
        public int IniciarCursoId { get; set; }
        public string? NombreCurso { get; set; }

        public string? Municipio { get; set; }

        public Cursos Cursoss { get; set; }
        public bool? Status { get; set; }
        public TimeSpan? HoraIniciar { get; set; }

        public TimeSpan? HoraFinal { get; set; }

        public DateTime? Fecha { get; set; }

        public int? NumeroParticipante { get; set; }

        public string Instituto { get; set; }

        public List<Instituciones>? Instituciones { get; set; }

        public List<Cursos>? Cursos { get; set; }

        public Hora? Hora { get; set; }
        public List<Hora>? HorlaLista { get; set; }
        public string Horas { get; set; }

        public List<DiaSemana>? diaSemanas { get; set; }

        public string[] diaSemanass { get; set; }

        public int? CursoId { get; set; }


        public int? InstitutoId { get; set; }

        public string? Profesor { get; set; }

        public int? Estudiantes { get; set; }

        public string? Finalizo { get; set; }

        public string? FechaInicio { get; set; }

        public DateTime? FechaIniciar { get; set; }
        public List<DiaSemana>? dias { get; set; }

        public int ? ProfesorId { get; set; }

        public List<Usuario>? Profesores { get; set; }

        public List<DiaSemana>? DiasNoLaboral { get; set; }

        public int CantidadHora { get; set; }
    }
}
