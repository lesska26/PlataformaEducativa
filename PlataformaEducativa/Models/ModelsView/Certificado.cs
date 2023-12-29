namespace PlataformaEducativa.Models.ModelsView
{
    public class Certificado
    {
        public string Nombre { get; set; }
        public string profesor { get; set; }
        public string CursosName { get; set; }
         public DateTime? Finalizo { get; set; }
        public int IniciarCursoId { get; set; }
        public int EstudiantesId { get; set; }
    }
}
