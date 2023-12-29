using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PlataformaEducativa.Logica;
using PlataformaEducativa.Models;
using PlataformaEducativa.Models.ModelsView;
using System.Globalization;

namespace PlataformaEducativa.Controllers
{
    public class IniciarCursoController : Controller
    {
        private readonly PlataformaEducativaDbContext _dbContext;
        public IniciarCursoController(PlataformaEducativaDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        // GET: IniciarCursoController
        public ActionResult Index()
        {
            using (var db = _dbContext)
            {

                var IniciarCursos = from c in db.iniciarCurso
                                    where c.Activo == 0 && c.Termino == null
                                    select new IniciarCursoView
                                    {
                                        NombreCurso = (from i in db.Cursos select i).FirstOrDefault(f => f.CursosId == c.CursosId).CursosName,
                                        Instituto = (from i in db.instituciones select i).FirstOrDefault(f => f.InstitucionesId == c.InstitucionesId).Nombre,

                                        Hora = (from f in db.Horas where f.HoraId == c.HoraId select f).First(),
                                        CursoId = c.CursosId,
                                        InstitutoId = c.InstitucionesId,
                                        IniciarCursoId = c.IniciarCursoId,
                                        diaSemanas = (from i in _dbContext.dia_Semana
                                                      join f in db.diaSemanaCursos
                                                      on i.DiaSemanaId equals f.DiaSemanaId
                                                      where f.IniciarCursoId == c.IniciarCursoId select i).ToList(),
                                        Profesor = (from x in _dbContext.cursoProfesors join b in _dbContext.usuarios
                                                 on x.UsuarioId equals b.UsuarioId
                                                    where c.IniciarCursoId == x.IniciarCursoId select b.Nombre).FirstOrDefault(),

                                        Estudiantes = (from x in db.CursoParticipante
                                                       where x.IniciarCursoId == c.IniciarCursoId select x).ToList().Count,
                                        Finalizo = c.Finalizo.ToString()


                                    };
                return View(IniciarCursos.ToList());
            }

        }

        public async Task<IActionResult> CursosFinalizados()
        {
            using (var db = _dbContext)
            {

                var IniciarCursos = from c in db.iniciarCurso
                                    where c.Activo == 0 && c.Termino == 1
                                    select new IniciarCursoView
                                    {
                                        NombreCurso = (from i in db.Cursos select i).FirstOrDefault(f => f.CursosId == c.CursosId).CursosName,
                                        Instituto = (from i in db.instituciones select i).FirstOrDefault(f => f.InstitucionesId == c.InstitucionesId).Nombre,

                                        Horas = (from f in db.Horas where f.HoraId == c.HoraId select f).First().Horas_Iniciar,
                                        CursoId = c.CursosId,
                                        InstitutoId = c.InstitucionesId,
                                        IniciarCursoId = c.IniciarCursoId,
                                        diaSemanas = (from i in _dbContext.dia_Semana
                                                      join f in db.diaSemanaCursos
                                                      on i.DiaSemanaId equals f.DiaSemanaId
                                                      where f.IniciarCursoId == c.IniciarCursoId
                                                      select i).ToList(),
                                        Profesor = (from x in _dbContext.cursoProfesors
                                                    join b in _dbContext.usuarios
                                                 on x.UsuarioId equals b.UsuarioId
                                                    where c.IniciarCursoId == x.IniciarCursoId
                                                    select b.UserName).FirstOrDefault(),

                                        Estudiantes = (from x in db.CursoParticipante
                                                       where x.IniciarCursoId == c.IniciarCursoId
                                                       select x).ToList().Count,



                                    };
                return View(IniciarCursos.ToList());
            }

        }
        public async Task<IActionResult> CursosActivos()
        {
            using (var db = _dbContext)
            {


                int Id = int.Parse(User.FindFirst("ID").Value);
                var IniciarCursos = from c in db.iniciarCurso
                                    join p in db.cursoProfesors
                                    on c.IniciarCursoId equals p.IniciarCursoId

                                    where c.Activo == 1 && p.UsuarioId == Id
                                    select new IniciarCursoView
                                    {
                                        NombreCurso = (from i in db.Cursos select i).FirstOrDefault(f => f.CursosId == c.CursosId).CursosName,
                                        Instituto = (from i in db.instituciones select i).FirstOrDefault(f => f.InstitucionesId == c.InstitucionesId).Nombre,

                                        Hora = (from f in db.Horas where f.HoraId == c.HoraId select f).First(),
                                        CursoId = c.CursosId,
                                        InstitutoId = c.InstitucionesId,
                                        IniciarCursoId = c.IniciarCursoId,
                                        diaSemanas = (from i in _dbContext.dia_Semana
                                                      join f in db.diaSemanaCursos
                                                      on i.DiaSemanaId equals f.DiaSemanaId
                                                      where f.IniciarCursoId == c.IniciarCursoId
                                                      select i).ToList(),
                                        Profesor = (from x in _dbContext.cursoProfesors
                                                    join b in _dbContext.usuarios
                                                 on x.UsuarioId equals b.UsuarioId
                                                    where c.IniciarCursoId == x.IniciarCursoId
                                                    select b.UserName).FirstOrDefault(),

                                        Estudiantes = (from x in db.CursoParticipante
                                                       where x.IniciarCursoId == c.IniciarCursoId
                                                       select x).ToList().Count,

                                        Finalizo = c.Finalizo.ToString().Substring(0, 11),
                                        FechaInicio = c.FechaIniciar.ToString().Substring(0, 11)


                                    };
                return View(IniciarCursos.ToList());
            }

        }
        // GET: IniciarCursoController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: IniciarCursoController/Create
        [HttpGet]
        public async Task<ActionResult> Crear()
        {
            var IniciarView = new IniciarCursoView();

            IniciarView.Instituciones = await _dbContext.instituciones.Where(c => c.InstitucionesId == int.Parse(User.FindFirst("InstitucionId").Value)).ToListAsync();
            IniciarView.Cursos = await _dbContext.Cursos.ToListAsync();
            IniciarView.HorlaLista = await _dbContext.Horas.ToListAsync();
            IniciarView.diaSemanas = await _dbContext.dia_Semana.ToListAsync();
            return View(IniciarView);
        }

        // POST: IniciarCursoController/Create
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Crear([FromBody] IniciarCursoView iniciarCurso)
        {
            try
            {
                int institucionId = int.Parse(User.FindFirst("InstitucionId").Value);
                var IniciarView = new IniciarCursoView();
                IniciarView.Instituciones = _dbContext.instituciones.Where(c => c.InstitucionesId == institucionId).ToList();
                IniciarView.Cursos = _dbContext.Cursos.ToList();
                IniciarView.diaSemanas = _dbContext.dia_Semana.ToList();
                IniciarView.HorlaLista = _dbContext.Horas.ToList();
               

                using (var db = _dbContext)
                {
                    var idInstituto = db.instituciones.Where(m => m.Nombre.Trim() == iniciarCurso.Instituto
                    .ToString().Trim()).FirstOrDefault().InstitucionesId;
                    if (idInstituto == null)
                    {
                        return View(IniciarView);
                    }

                    var CursoId = db.Cursos.FirstOrDefault(m => m.CursosName == iniciarCurso.NombreCurso.Trim()).CursosId;
                    if (CursoId == null)
                    {
                        return View(IniciarView);
                    }
                    if (!int.TryParse(iniciarCurso.Horas, out int d))
                        return View(IniciarView);
                    var idHora = db.Horas.Find(int.Parse(iniciarCurso.Horas));
                    var IniciarCursos = new IniciarCurso();
                    IniciarCursos.CursosId = CursoId;
                    IniciarCursos.InstitucionesId = idInstituto;
                    IniciarCursos.HoraIniciar = iniciarCurso.HoraIniciar;
                    IniciarCursos.HoraTerminar = iniciarCurso.HoraFinal;
                    IniciarCursos.FechaIniciar = iniciarCurso.Fecha;
                    IniciarCursos.HoraId = idHora.HoraId;
                    IniciarCursos.Finaliza = null;

                    _dbContext.iniciarCurso.Add(IniciarCursos);
                    _dbContext.SaveChanges();

                    foreach (var i in iniciarCurso.diaSemanass)
                    {

                        if (int.TryParse(i, out int s))
                        {
                            var CursosSemanas = new DiaSemanaCurso();
                            CursosSemanas.DiaSemanaId = int.Parse(i);
                            CursosSemanas.IniciarCursoId = IniciarCursos.IniciarCursoId;
                            _dbContext.diaSemanaCursos.Add(CursosSemanas);
                            _dbContext.SaveChanges();
                        }
                    }

                }
                return Ok();
            }
            catch
            {
                return View();
            }
        }

        // GET: IniciarCursoController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: IniciarCursoController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
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

        // GET: IniciarCursoController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: IniciarCursoController/Delete/5
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
        public IActionResult AsignarProf(int IniciarCursoId, int Curso, int Instituto)
        {
            List<Usuario> usuarios = new List<Usuario>();
            ModelsAsignar modelsAsignar = new ModelsAsignar();
            modelsAsignar.IniciarCursoId = IniciarCursoId;
            var instituto = _dbContext.instituciones.Find(Instituto);
            modelsAsignar.CursoId = Curso;
            modelsAsignar.CursoName = _dbContext.Cursos.Find(Curso).CursosName;
            modelsAsignar.Instituto =instituto.Nombre;
            modelsAsignar.InstitutoNombre = instituto.Nombre;
          
            var lista = (from c in _dbContext.usuarios
                         join f in _dbContext.roles
                         on c.RolesId equals f.RolesId join
                         m in _dbContext.usuario_Materias on
                         c.UsuarioId equals m.UsuarioId join z in
                         _dbContext.Cursos on m.CursosId
                         equals z.CursosId
                         //join r in _dbContext.cursoProfesors
                         //on c.UsuarioId equals r.UsuarioId
                         where c.InstitucionesId == instituto.InstitucionesId
                        
                         select new Usuario
                         {
                             Nombre = c.Nombre,
                             UsuarioId = c.UsuarioId


                         }).ToList();
            lista.ForEach(e =>
            {
                var cantidad = (from c in _dbContext.iniciarCurso
                                join f in _dbContext.cursoProfesors
                                on c.IniciarCursoId equals f.IniciarCursoId
                                where c.Activo == 1 && f.UsuarioId == e.UsuarioId
                                select c).ToList().Count;
                if (cantidad <= 3)
                {
                    usuarios.Add(e);
                }
            });
            modelsAsignar.Usuarios = usuarios;
            return View(modelsAsignar);
        }
        [HttpPost]
        public IActionResult AsignarProf(ModelsAsignar modelsAsignar, int Usuario)
        {
            try
            {
                var validarProfesor = (from c in _dbContext.usuarios
                                       join f in _dbContext.usuario_Materias
                                       on c.UsuarioId equals f.UsuarioId
                                       join e in _dbContext.Cursos
                                       on f.CursosId equals e.CursosId
                                       where e.CursosId == modelsAsignar.CursoId && c.UsuarioId == Usuario
                                       select c.Nombre).FirstOrDefault();
                if (validarProfesor != null)
                {

                    var MateriId = (from c in _dbContext.Cursos
                                    where c.CursosId == modelsAsignar.CursoId
                                    select c.CursosId).FirstOrDefault();

                    CursoProfesor cursoProfesor = new CursoProfesor();
                    cursoProfesor.IniciarCursoId = modelsAsignar.IniciarCursoId;
                    cursoProfesor.UsuarioId = Usuario;

                    _dbContext.cursoProfesors.Add(cursoProfesor);
                    _dbContext.SaveChanges();
                    return RedirectToAction("Index", "IniciarCurso");
                }
                return NotFound();
            }
            catch (Exception e)
            {

                return Ok(e.Message);
            }



        }
        [HttpGet]
        public IActionResult Iniciar(Datos datos)
        {

            return View(datos);
        }
        [HttpPost]

        public async Task<IActionResult> Iniciar(Datos datos, DateTime Fecha)
        {
            using (var dbContext = _dbContext)
            {
                var Edit = dbContext.iniciarCurso.Find(datos.iniciarCursoId);
                if (Edit != null)
                {
                    DateTime dateTime = Fecha;
                    var listaDiaLaborabel = await (from c in _dbContext.dia_Semana join f in _dbContext.diaSemanaCursos
                                           on c.DiaSemanaId equals f.DiaSemanaId where f.IniciarCursoId == datos.iniciarCursoId
                                                   select c).ToListAsync();
                    int hora = await (from c in _dbContext.Horas join f in _dbContext.iniciarCurso on c.HoraId equals f.HoraId select c.CatidadHora).FirstAsync();

                    var cursoDuracion = await (from c in _dbContext.Cursos
                                               join f in _dbContext.iniciarCurso on c.CursosId equals f.CursosId
                                               select c.Duracion).FirstAsync();


                    int Duracion = cursoDuracion / hora;
                    while (Duracion > 0)
                    {
                        foreach (var i in listaDiaLaborabel)
                        {
                            if (dateTime.DayOfWeek.ToString() == TraduccionSemanasInglesh.Traduccion(i.Dias) && Duracion > 0)
                            {
                                dateTime = dateTime.AddDays(1);
                                Duracion--;
                            }
                        };
                        if (Duracion > 0)
                        {

                            dateTime = dateTime.AddDays(1);
                        }

                    }
                    Edit.FechaIniciar = Fecha;
                    Edit.Activo = 1;
                    Edit.Finalizo = dateTime;
                    dbContext.Update(Edit);
                    await dbContext.SaveChangesAsync();
                    return RedirectToAction("Index", "IniciarCurso");
                }
                return NotFound();
            }
        }

        public async Task<IActionResult> IniciarEdit(int Id)
        {
            try
            {

                var IniciarCurso = _dbContext.iniciarCurso.Find(Id);
                if (IniciarCurso == null)
                {
                    return NotFound();
                }
                var InicursoEdit = new IniciarCursoView();
                InicursoEdit.IniciarCursoId = IniciarCurso.IniciarCursoId;
                InicursoEdit.Profesor = (from c in _dbContext.usuarios
                                         join x in _dbContext.cursoProfesors on
                                      c.UsuarioId equals x.UsuarioId
                                         where x.IniciarCursoId == IniciarCurso.IniciarCursoId
                                         select c.Nombre).FirstOrDefault();

                InicursoEdit.ProfesorId = (from c in _dbContext.usuarios
                                           join x in _dbContext.cursoProfesors on
                                        c.UsuarioId equals x.UsuarioId
                                           where x.IniciarCursoId == IniciarCurso.IniciarCursoId
                                           select c.UsuarioId).FirstOrDefault();

                InicursoEdit.dias = (from c in _dbContext.dia_Semana
                                     join x in _dbContext.diaSemanaCursos
                                  on c.DiaSemanaId equals x.DiaSemanaId
                                     where x.IniciarCursoId == IniciarCurso.IniciarCursoId
                                     select c).ToList();

              

                var laboral = _dbContext.dia_Semana.ToList();
                InicursoEdit.DiasNoLaboral = laboral.Except(InicursoEdit.dias.ToList()).ToList();
                InicursoEdit.Hora = (from c in _dbContext.Horas
                                     where c.HoraId == IniciarCurso.HoraId
                                     select c).FirstOrDefault();

                InicursoEdit.HorlaLista = await _dbContext.Horas.ToListAsync();

                InicursoEdit.Cursoss = (from c in _dbContext.iniciarCurso
                                        join x in _dbContext.Cursos
                                   on c.CursosId equals x.CursosId
                                        where IniciarCurso.IniciarCursoId == c.IniciarCursoId
                                        select x).FirstOrDefault();
                int cursos = InicursoEdit.Cursoss.CursosId;
                InicursoEdit.Profesores = await (from c in _dbContext.usuarios
                                                 join x in _dbContext.roles on c.RolesId equals
                                                 x.RolesId join b in _dbContext.usuario_Materias on
                                                 c.UsuarioId equals  b.UsuarioId join m in _dbContext.Cursos
                                                 on b.CursosId equals m.CursosId
                                                 where
                                              c.InstitucionesId == int.Parse(User.FindFirst("InstitucionId").Value)
                                              && m.CursosId==cursos
                                                 select c).ToListAsync();
                return View(InicursoEdit);


            }
            catch (Exception ex)
            {
                return View(ex.Message);
            }

        }
        [HttpPost]
       public async Task<IActionResult>UpdateIniciar([FromBody]UpdateIniciarCurso IniciarCurso)
        {
            try
            {

                if (IniciarCurso.AgreagarDias.Length > 0)
                {
                    
                    for (int i = 0; IniciarCurso.AgreagarDias.Length > i; i++)
                    {
                        var DiasSemanaCurso = new DiaSemanaCurso();
                        DiasSemanaCurso.IniciarCursoId = IniciarCurso.IniciarCursoId;
                        DiasSemanaCurso.DiaSemanaId = IniciarCurso.AgreagarDias[i];
                        _dbContext.diaSemanaCursos.Add(DiasSemanaCurso);
                        _dbContext.SaveChanges();
                    }
                }
                if (IniciarCurso.EliminarDia.Length > 0)
                {
                    for(int a = 0; IniciarCurso.EliminarDia.Length > a; a++)
                    {
                        var DiasSemanaCurso = _dbContext.diaSemanaCursos.FirstOrDefault(c=>c.IniciarCursoId==IniciarCurso.IniciarCursoId &&
                        c.DiaSemanaId == IniciarCurso.EliminarDia[a]);
                        _dbContext.diaSemanaCursos.Remove(DiasSemanaCurso);
                        _dbContext.SaveChanges();

                    }
                }
                var cursoProfesor = _dbContext.cursoProfesors.Find(IniciarCurso.IniciarCursoId);
                if (cursoProfesor != null)
                {
                    cursoProfesor.UsuarioId = IniciarCurso.ProfesorId;
                    _dbContext.SaveChanges();
                }
                return Ok();
            }
            catch(Exception ex)
            {
                return Ok(ex.Message);
            }
        }
        public async Task<IActionResult> TusCursosActivos()
        {
            int Id = int.Parse(User.FindFirst("InstitucionId").Value);
            var IniciarCursos = from c in _dbContext.iniciarCurso
                                join p in _dbContext.cursoProfesors
                                on c.IniciarCursoId equals p.IniciarCursoId

                                where c.Activo == 1 && c.InstitucionesId== Id
                                select new IniciarCursoView
                                {
                                    NombreCurso = (from i in _dbContext.Cursos select i).FirstOrDefault(f => f.CursosId == c.CursosId).CursosName,
                                    Instituto = (from i in _dbContext.instituciones select i).FirstOrDefault(f => f.InstitucionesId == c.InstitucionesId).Nombre,

                                    Hora = (from f in _dbContext.Horas where f.HoraId == c.HoraId select f).First(),
                                    CursoId = c.CursosId,
                                    InstitutoId = c.InstitucionesId,
                                    IniciarCursoId = c.IniciarCursoId,
                                    diaSemanas = (from i in _dbContext.dia_Semana
                                                  join f in _dbContext.diaSemanaCursos
                                                  on i.DiaSemanaId equals f.DiaSemanaId
                                                  where f.IniciarCursoId == c.IniciarCursoId
                                                  select i).ToList(),
                                    Profesor = (from x in _dbContext.cursoProfesors
                                                join b in _dbContext.usuarios
                                             on x.UsuarioId equals b.UsuarioId
                                                where c.IniciarCursoId == x.IniciarCursoId
                                                select b.Nombre).FirstOrDefault(),

                                    Estudiantes = (from x in _dbContext.CursoParticipante
                                                   where x.IniciarCursoId == c.IniciarCursoId
                                                   select x).ToList().Count,

                                    Finalizo = c.Finalizo.ToString().Substring(0, 11),
                                    FechaInicio = c.FechaIniciar.ToString().Substring(0, 11)


                                };
            return View(IniciarCursos.ToList());
        }
    }
    public class UpdateIniciarCurso
    {
        public int[]? EliminarDia { get; set; }
        public int []?AgreagarDias{get;set;}
        public int IniciarCursoId { get; set; }
        public int ProfesorId { get; set; }
        public int Hora { get; set; }

    }
  
}
