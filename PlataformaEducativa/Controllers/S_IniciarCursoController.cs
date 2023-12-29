using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PlataformaEducativa.Models;

namespace PlataformaEducativa.Controllers
{
    public class S_IniciarCursoController : Controller
    {
        private readonly PlataformaEducativaDbContext _dbContext;
       
        public S_IniciarCursoController(PlataformaEducativaDbContext db) 
        { 
        
            _dbContext = db;
        }
        [HttpGet]
        public async Task<IActionResult> List_IniciarCurso()
        {
            try
            {
                var Centros = _dbContext.instituciones.Where(c=>
                c.Municipio==User.FindFirst("Municipio").Value);

                return View(Centros.ToList());

            }catch (Exception ex)
            {
                return Ok(ex.Message);
            }
        }

        public async Task<IActionResult>L_Curso_I(int id)
        {

            try
            {

                var CursoEnIncricion = from c in _dbContext.iniciarCurso
                                       where c.InstitucionesId == id && c.Activo == 0
                                       && c.Termino == 0
                                       select new
                                       {
                                           profesor = (from f in _dbContext.cursoProfesors
                                                       join p in _dbContext.usuarios on
                                                       f.UsuarioId equals p.UsuarioId where
                                                       f.IniciarCursoId == c.IniciarCursoId
                                                       select p.Nombre).FirstOrDefault(),

                                           FechaIniciar = c.FechaIniciar == null ? "sin fecha" : c.FechaIniciar.ToString(),
                                           curso = _dbContext.Cursos.FirstOrDefault(m=>m.CursosId==c.CursosId).CursosName,
                                           centro = _dbContext.instituciones.FirstOrDefault(m=>m.InstitucionesId==c.InstitucionesId).Nombre,
                                           Estudiantes = _dbContext.CursoParticipante.Where(r=>r.IniciarCursoId==c.IniciarCursoId).Count()
                         

                                       };
                return Ok(CursoEnIncricion.ToList());

            }catch(Exception ex)
            {
                return Ok(ex.Message);
            }
        }
       
    }
   
   
}
