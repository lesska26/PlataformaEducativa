using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol;
using PlataformaEducativa.Models;
using PlataformaEducativa.Models.ModelsView;

namespace PlataformaEducativa.Controllers
{
    public class CursosController : Controller
    {
        private readonly PlataformaEducativaDbContext _db;
        public  CursosController(PlataformaEducativaDbContext context) 
        { 
            _db = context;
        }
        public IActionResult Index()
         {
            var Cursos= (from c in _db.Cursos select c).ToList();
            return View(Cursos.ToList());
        }
      
        [HttpGet]
        public IActionResult Crear()
        {

            return View();
        }

        [HttpPost]
      
        public IActionResult Crear(Models.ModelsView.CursoView cursos)
        {
            if(!ModelState.IsValid)
            {
                return View();
            }
            var curso = new Cursos();
            curso.Duracion = cursos.Duracion;
            curso.CursosName = cursos.CursosName;
            curso.UsuarioId = int.Parse(User.FindFirst("ID").Value);
            curso.Descripcion = cursos.Descripcion;
            _db.Cursos.Add(curso);
            _db.SaveChanges();
           
            return RedirectToAction("Index","Home");
        }
    }
}
