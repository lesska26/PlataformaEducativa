namespace PlataformaEducativa.Models.ModelsView
{
    public class ModelsAsignar
    {
        public int IniciarCursoId { get; set; }

        public int CursoId { get; set; }

        public string Instituto { get; set; }

        public string InstitutoNombre { get; set; }

        public List<Usuario>? Usuarios { get; set; }
        public string CursoName { get; set; }
    }
}
