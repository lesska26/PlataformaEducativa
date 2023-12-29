using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PlataformaEducativa.Models;
using PlataformaEducativa.Models.ModelsView;
using System.Security.Cryptography.X509Certificates;
using System.Security.Permissions;

namespace PlataformaEducativa.Controllers
{
    public class EstudiantesController : Controller
    {
        private PlataformaEducativaDbContext _db;

        public EstudiantesController(PlataformaEducativaDbContext dbContext)
        {

            _db = dbContext;
        }
        // GET: EstudiantesController
        public ActionResult Index()
        {
            try

            {

                if (!User.IsInRole("Admin"))
                {
                    int InstitutoId = int.Parse(User.FindFirst("InstitutoId").Value);

                }
                //var Estudiante = from c in _db.estudiantes
                //                 select new EstudiantesDetalles
                //                 {
                //                     Nombre = c.Nombre,
                //                     Fecha = c.FechaDeNacimiento,
                //                     cursos = (from x in _db.Cursos join
                //                            i in _db.iniciarCurso on x.CursosId
                //                            equals i.CursosId join v in _db.CursoParticipante
                //                              on c.EstudiantesId equals v.EstudiantesId
                //                               select x).ToList()

                //                 };
                var Estudiante = (from c in _db.estudiantes select c).ToList();
                return View(Estudiante);
            }
            catch (Exception ex)
            {
                return View();

            }

        }

        public IActionResult BucardorEstudiantes()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> BucardorEstudiantes([FromBody] datos datos)
         {
            try
            {
                var id=(from c in _db.estudiantes where c.Matricula==datos.NCM select c.EstudiantesId).FirstOrDefault();
                var Estudiantes = (from c in _db.estudiantes
                                   where c.Matricula == datos.NCM
                                   select new
                                   {
                                       c.Nombre,
                                       c.Genero,
                                       c.EstudiantesId,
                                       listaCurso = (from x in _db.CursoParticipante join v in _db.CursoNota
                                                  on x.IniciarCursoId equals v.IniciarCursoId join i in
                                                  _db.iniciarCurso on x.IniciarCursoId equals i.IniciarCursoId
                                                     join t in _db.Cursos on i.CursosId equals t.CursosId
                                                     where  v.Status !='R' && x.EstudiantesId==id select new 
                                                     {

                                                            Curso = t.CursosName,
                                                             t.Duracion,
                                                             x.IniciarCursoId,
                                                             x.EstudiantesId,
                                                             


                                                     }).Distinct(),
                                      

                                   }).FirstOrDefault();

                //var status = (from c in _db.CursoNota where c.EstudiantesId == 5 select c).FirstOrDefault();
                if (Estudiantes == null)
                {
                    return Ok(null);
                }
                return Json(Estudiantes);
            }
            catch (Exception ex)
            {
                return Ok(ex.Message);
            }

            
           
        }
        // GET: EstudiantesController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }



        // POST: EstudiantesController/Create
        [HttpPost]
        ////[ValidateAntiForgeryToken]
        public ActionResult Crear([FromBody] EstudianteCursoView EstudiantesCurso)
        {
            try
            {
                 var existeCedula = _db.estudiantes.FirstOrDefault(m=>m.Cedula==EstudiantesCurso.Cedulas);
                if (existeCedula != null)
                {
                    return Ok("existe");
                }
                Estudiantes estudiantes = new Estudiantes();
                estudiantes.Apellido = EstudiantesCurso.Apellido;
                estudiantes.Nombre = EstudiantesCurso.Nombre;
                estudiantes.Correo = EstudiantesCurso.Correo;
                estudiantes.Telefono = EstudiantesCurso.Telefono;
                estudiantes.Cedula = EstudiantesCurso.Cedulas == string.Empty ?"No tiene Cedula": EstudiantesCurso.Cedulas; ;
                estudiantes.FechaCreacion = DateTime.Now;
                estudiantes.InstitucionesId = int.Parse(User.FindFirst("InstitucionId").Value);
                estudiantes.FechaDeNacimiento = EstudiantesCurso.Fecha;
                estudiantes.Status = "activo";
                estudiantes.Verificar = true;
                estudiantes.Genero = (char)EstudiantesCurso.Genero;
                estudiantes.Municipio = User.FindFirst("Municipio").Value;
                estudiantes.Matricula = Logica.Matricula.createMatricula(EstudiantesCurso.Nombre, EstudiantesCurso.Apellido);
                estudiantes.UsuarioId = int.Parse(User.FindFirst("ID").Value);
                _db.estudiantes.Add(estudiantes);

                _db.SaveChanges();

                CursoParticipante cursoParticipante = new CursoParticipante();

                cursoParticipante.IniciarCursoId = EstudiantesCurso.iniciarCursoId;
                cursoParticipante.EstudiantesId = estudiantes.EstudiantesId;

                _db.CursoParticipante.Add(cursoParticipante);
                _db.SaveChanges();
                return Ok(true);
            }
            catch(Exception ex)
            {
                return Ok(ex.Message);
            }
        }

        // GET: EstudiantesController/Edit/5
        public ActionResult Edit(int? id)
        {
            try
            {
                if (id == null)
                {
                    return NotFound();
                }
                var Estudinate = _db.estudiantes.Find(id);
                if (Estudinate == null)
                {
                    return NotFound();
                }

                return View(Estudinate);
            }
            catch (Exception e)
            {
                return Ok(e.Message);
            }
            
        }

        // POST: EstudiantesController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Estudiantes estudiantes)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(estudiantes);
                }
                _db.estudiantes.Update(estudiantes);
                _db.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: EstudiantesController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: EstudiantesController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
        [HttpGet]
        public IActionResult Existe(string idcurso, string institutoId, string curso, string instituto, int iniciarCursoId)
        {
            var Datos = new Datos();
            Datos.InstitutoId = int.Parse(idcurso);
            Datos.CursoId = int.Parse(idcurso);
            Datos.Curso = curso;
            Datos.Instituto = instituto;
            Datos.iniciarCursoId = iniciarCursoId;


            return View("VerificarExiste", Datos);
        }
        [HttpPost]
        public IActionResult Existe([FromBody] Datos datos)
        {
            var existe = _db.estudiantes.FirstOrDefault(m => m.Cedula == datos.Cedula.Trim() || m.Matricula == datos.Cedula);
            if (existe == null)
            {
                return PartialView("_RegistrarEstudiante");
            }
            var yaRegistrador = _db.CursoParticipante.FirstOrDefault(
                 c => c.IniciarCursoId == datos.iniciarCursoId && c.EstudiantesId == existe.EstudiantesId

                );
            if (yaRegistrador != null)
            {
                return Ok("Ya esta Registrado en el curso");
            }

            CursoParticipante cursoParticipante = new CursoParticipante();

            cursoParticipante.IniciarCursoId = datos.iniciarCursoId;
            cursoParticipante.EstudiantesId = existe.EstudiantesId;
            _db.CursoParticipante.Add(cursoParticipante);
            _db.SaveChanges();
            return Ok(true);
        }
        [HttpGet]
        public async Task<IActionResult>List_Estudt_Curso(int id)
        {
            try
            {


                var lista_Est_Curso = (from c in _db.CursoParticipante where 
                                       c.IniciarCursoId==id
                                       select new CursoParticipante
                                       {
                                           Nombre=c.Estudiantes.Nombre,
                                           EstudiantesId=c.EstudiantesId
                                       }).ToList();
                return View(lista_Est_Curso);
               
            }catch (Exception ex)
            {
                return Ok(ex.Message);
            }
        }
        //[HttpPost]
        //public async Task<IActionResult> BuscarEstudiantes(string MatriculaCedula)
        //{
        //    try
        //    {
        //        var Estudiates = await (from c in _db.estudiantes
        //                                where c.Matricula == MatriculaCedula || c.Cedula == MatriculaCedula
        //                                select c).FirstAsync();
        //        return Ok(Estudiates);
        //    }
        //    catch (Exception e)
        //    {
        //        return Ok(e.Message);
                
        //    }
            
        //}
        
        
    }
   
    public class datos
    {
        public string NCM { get; set; }
    }
}
