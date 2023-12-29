namespace PlataformaEducativa.Models.ModelsView
{
    public class ListUsuarioView
    {
        public int ProfesorId { get; set; }
        public string ProfesorName { get; set; }

        public ICollection<Cursos>Materia{get;set;}

        public ICollection<Cursos> usuario_Materias { get;set;}

        public string? Foto { get; set; }

        public int? Instituciones { get; set; }

        public string Roles { get; set; }

        public string Municipio { get; set; }

        public int Cantidad { get; set; }

        public string Centros { get; set; }

    }
}
