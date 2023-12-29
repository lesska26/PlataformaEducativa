using Microsoft.AspNetCore.Mvc;
using PlataformaEducativa.Models;
using PlataformaEducativa.Models.ModelsView;

namespace PlataformaEducativa.Controllers
{
    public class CalificarController : Controller
    {
        private readonly PlataformaEducativaDbContext _db;

        public CalificarController(PlataformaEducativaDbContext _dbContext)
        {
            _db = _dbContext;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> CalificarEstudiantes(int IniciarCursoId)
        {
            var listaEstudiantes = from c in _db.CursoParticipante
                                   where c.IniciarCursoId == IniciarCursoId
                                   select new CursoParticipante
                                   {
                                       EstudiantesId = c.EstudiantesId,
                                       IniciarCursoId = c.IniciarCursoId,
                                       Estudiantes = (from x in _db.estudiantes 
                                                     where x.EstudiantesId==c.EstudiantesId select x)
                                                     .FirstOrDefault()

                                   };

            return View(listaEstudiantes.ToList());
        }

        [HttpPost]
        public  IActionResult CalificarEstudiantes([FromBody]List<Calificar>Calificar,int IniciarCursoId)
        {
            try
            {
                int id = 0;
                foreach (var i in Calificar)
                {
                    var CursoNota = new CursoNota();
                    CursoNota.Nota = i.Nota;
                    CursoNota.IniciarCursoId = i.IniciarCursoId;
                    CursoNota.EstudiantesId = i.EstudianteId;
                    CursoNota.Fecha = DateTime.Now;
                    CursoNota.Status = CursoNota.Nota >= 70 ? 'A' : 'R';
                    id = i.IniciarCursoId;
                    _db.CursoNota.Add(CursoNota);
                    _db.SaveChanges();

                }
                var Edit = _db.iniciarCurso.Find(id);
                Edit.Termino = 1;
                Edit.Finaliza = DateTime.Now;
                Edit.Activo = 0;
                _db.SaveChanges();
                CursoFinalizar cursoFinalizar= new CursoFinalizar();
                cursoFinalizar.FinalizoCurso = DateTime.Now;
                cursoFinalizar.IniciarCursoId = Edit.IniciarCursoId;
                return Ok(true);
            }
            catch (Exception e)
            {

                return Ok(e.Message);
            }
            
        }
    }
}
