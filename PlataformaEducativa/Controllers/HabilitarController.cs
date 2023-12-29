using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PlataformaEducativa.Models;
using PlataformaEducativa.Models.ModelsView;
namespace PlataformaEducativa.Controllers
{
    public class HabilitarController : Controller
    {
        private readonly PlataformaEducativaDbContext _context;
        public HabilitarController(PlataformaEducativaDbContext context) { 
           _context= context;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> HabilitarCurso()
        {
            var HabilitarCurso=new HabilitarCurso();
            HabilitarCurso.Cursos = _context.Cursos.ToList();
            HabilitarCurso.Municipio = _context.municipio.ToList();
            return View(HabilitarCurso);
        }
        [HttpGet]
        public async Task<IActionResult> L_C(int ?id)
        {
            if (id == null)
            {
                return Ok(false);
            }
            var municipio = await _context.municipio.FindAsync(id);
            if (municipio != null)
            {
                var Centros = from c in _context.instituciones where c.Municipio==municipio.MunicipioName
                              select new { 
                                  c.InstitucionesId,
                                  c.Nombre
                              };
                return Json(Centros);

            }
            return Ok(false);


        }

    }
    
}
