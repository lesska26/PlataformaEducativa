namespace PlataformaEducativa.Models.ModelsView
{
    public class InstitucionesModelView
    {
        public string Nombre { get; set; }
        public List<Municipio> ?Municipios { get;  set; }
        public string Direccion { get; set; }
        public string MunicipioSelect { get;  set; }
        public int ?InstitucionesId { get; set; }
        public string Telefono { get; set; }
       
    }
}