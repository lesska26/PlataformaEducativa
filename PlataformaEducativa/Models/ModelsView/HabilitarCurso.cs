namespace PlataformaEducativa.Models.ModelsView
{
    public class HabilitarCurso
    {
        public ICollection<Cursos> Cursos { get; set; }
        public List<Municipio> Municipio { get; set; }
    }
}
