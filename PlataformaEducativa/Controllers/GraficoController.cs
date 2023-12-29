using Microsoft.AspNetCore.Mvc;
using PlataformaEducativa.Models;
using PlataformaEducativa.Models.ModelsView;

namespace PlataformaEducativa.Controllers
{
    public class GraficoController : Controller
    {
        private readonly PlataformaEducativaDbContext _dbcontext;
        public GraficoController(PlataformaEducativaDbContext db) 
        {
            
            _dbcontext = db;
        }

        public IActionResult CentroGrafico()
        {
            var centros = from c in _dbcontext.instituciones
                          where c.Municipio == User.FindFirst("Municipio").Value
                          select new ReporteView
                          {
                              centro=c.Nombre,
                              cant=_dbcontext.usuarios.Select(f=>f.InstitucionesId==c.InstitucionesId).ToList().Count
                          };
            return Ok(centros.ToList());
        }
    }
}
