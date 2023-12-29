using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PlataformaEducativa.Models;
using PlataformaEducativa.Models.ModelsView;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using System.Diagnostics.Eventing.Reader;
using Microsoft.AspNetCore.Authorization;
using Nager.Date.Model;
using PlataformaEducativa.Seguridad;
using PlataformaEducativa.Correo;
using System.Linq;

namespace PlataformaEducativa.Controllers
{


    public class UsuarioController : Controller
    {
        private readonly PlataformaEducativaDbContext _dbContext;
        private readonly Correo.Smtp smtp;

        public UsuarioController(PlataformaEducativaDbContext dbContext, Correo.Smtp smtp1)
        {

            _dbContext = dbContext;
            smtp = smtp1;
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> R_S_Admin()
        {
            RegisterView usuario = new RegisterView();
            if (User.IsInRole("Admin"))
            {
                usuario.Roles = await _dbContext.roles.Where(m => m.RoleName == "Admin" || m.RoleName == "Supervisor").ToListAsync();
            }
            else
            {
                usuario.Roles = await _dbContext.roles.Where(x => x.RoleName != "Admin" && x.RoleName != "Supervisor").ToListAsync();
            }

            usuario.Municipios = await _dbContext.municipio.ToListAsync();
            return View(usuario);


        }

        [HttpPost]
        public async Task<IActionResult> R_S_Admin(RegisterView usuario, string Roles, string Municipios)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var numero = new Random(DateTime.Now.Millisecond);
                    Usuario user = new Usuario();
                    user.Nombre = usuario.Nombre;
                    user.UserName = usuario.cedula;
                    user.Apellido = usuario.Apellido;
                    user.Correo = usuario.Correo;
                    user.FechaCreacion = DateTime.Now;
                    user.FechaNacimiento = usuario.FechaNacimiento;
                    user.Genero = usuario.Genero;
                    user.Direccion = usuario.Direccion;
                    user.Telefono = usuario.Telefono;
                    user.Password = $"{numero.Next()}";
                    user.Confirmar = 1;
                    user.Cedula = usuario.cedula;
                    user.InstitucionesId = null;
                    var rol = await _dbContext.roles.FirstOrDefaultAsync(m => m.RoleName == Roles);
                    user.RolesId = rol.RolesId;
                    var municipio = await _dbContext.municipio.FirstOrDefaultAsync(m => m.MunicipioName == Municipios);
                    user.Municipio = municipio.MunicipioName;
                    user.Sector = null;
                    await _dbContext.AddAsync(user);
                    await _dbContext.SaveChangesAsync();
                    smtp.EnviarCorreo("", "", usuario.Correo, usuario.Password);
                    return RedirectToAction("Index", "Home");
                }


                RegisterView registerView = new RegisterView();

                registerView.Municipios = await _dbContext.municipio.ToListAsync();
                registerView.Roles = usuario.Roles = await _dbContext.roles.Where(x => x.RoleName == "Admin" && x.RoleName == "Supervisor").ToListAsync(); ;
                return View(registerView);
            }
            catch (Exception ex)
            {
                return View(ex.Message);
            }
        }

        [HttpGet]
        public IActionResult VerProfesor(int pg = 0)
        {
            try
            {
                ViewData["centros"] = null;
                var pag = pg <= 1 ? 0 : (10 * pg) - 10;
                if (User.IsInRole("Admin"))
                {
                    var Usuarios = from c in _dbContext.usuarios
                                   join f in _dbContext.roles on c.RolesId equals f.RolesId
                                   where f.RoleName == "Facilitador"

                                   select new ListUsuarioView
                                   {
                                       ProfesorName = c.Nombre,
                                       Materia = (from i in _dbContext.Cursos
                                                  join x in _dbContext.usuario_Materias
                                                  on i.CursosId equals x.CursosId
                                                  where x.UsuarioId == c.UsuarioId
                                                  select i).ToList(),
                                       usuario_Materias = (from b in _dbContext.Cursos
                                                           join x in _dbContext.usuario_Materias on
                                                         b.CursosId equals x.CursosId
                                                           where x.UsuarioId == c.UsuarioId
                                                           select b).ToList(),
                                       ProfesorId = c.UsuarioId,
                                       Foto = c.Foto,
                                       Instituciones = c.InstitucionesId,
                                       Roles = f.RoleName


                                   };
                    ViewBag.cantidad = Usuarios.Count();


                    return View(Usuarios.ToList().Skip(pg).Take(10).ToList());

                }

                var Usuario = from c in _dbContext.usuarios
                              join f in _dbContext.roles on c.RolesId equals f.RolesId
                              where f.RoleName == "Facilitador"

                              select new ListUsuarioView
                              {
                                  ProfesorName = c.Nombre,
                                  Materia = (from i in _dbContext.Cursos
                                             join x in _dbContext.usuario_Materias
                                             on i.CursosId equals x.CursosId
                                             where x.UsuarioId == c.UsuarioId
                                             select i).ToList(),
                                  usuario_Materias = (from b in _dbContext.Cursos
                                                      join x in _dbContext.usuario_Materias on
                                                    b.CursosId equals x.CursosId
                                                      where x.UsuarioId == c.UsuarioId
                                                      select b).ToList(),
                                  ProfesorId = c.UsuarioId,
                                  Foto = c.Foto,
                                  Instituciones = c.InstitucionesId,
                                  Roles = f.RoleName,
                                  Municipio = c.Municipio,
                                  Centros = (from i in _dbContext.instituciones
                                             where i.InstitucionesId == c.InstitucionesId
                                             select i.Nombre).FirstOrDefault()


                              };

                if (User.IsInRole("Gestor"))
                {
                    int institutoUser = int.Parse(User.FindFirst("InstitucionId").Value);
                    var filtros = Usuario.Where(c => c.Instituciones == institutoUser);
                    ViewBag.cantidad = filtros.Count();
                    return View(filtros.ToList().Skip(pg).Take(10).ToList());
                }
                if (User.IsInRole("SAC"))
                {
                    int institutoUser = int.Parse(User.FindFirst("InstitucionId").Value);
                    var filtros = Usuario.Where(c => c.Instituciones == institutoUser && c.Roles == "Facilitador");
                    ViewBag.cantidad = filtros.Count();
                    return View(filtros.ToList().Skip(pg).Take(10).ToList());
                }

                var filtros1 = Usuario.Where(c => c.Municipio == User.FindFirst("Municipio").Value && c.Roles == "Facilitador");
                ViewBag.cantidad = filtros1.Count();
                return View(filtros1.ToList().Skip(pag).Take(10).ToList());
            }
            catch (Exception ex)
            {
                return View(ex.Message);
            }


        }
        [Authorize(Roles = "Supervisor")]
        public async Task<IActionResult> BuscarProfCentros(int id, int pg = 0)
        {
            try
            {
                var pagina = pg <= 1 ? 0 : (pg * 10) - 10;
                var Usuario = (from c in _dbContext.usuarios
                               join f in _dbContext.roles on c.RolesId equals f.RolesId
                               where f.RoleName == "Facilitador" && c.InstitucionesId == id

                               select new ListUsuarioView
                               {
                                   ProfesorName = c.Nombre,
                                   Materia = (from i in _dbContext.Cursos
                                              join x in _dbContext.usuario_Materias
                                              on i.CursosId equals x.CursosId
                                              where x.UsuarioId == c.UsuarioId
                                              select i).ToList(),
                                   usuario_Materias = (from b in _dbContext.Cursos
                                                       join x in _dbContext.usuario_Materias on
                                                     b.CursosId equals x.CursosId
                                                       where x.UsuarioId == c.UsuarioId
                                                       select b).ToList(),
                                   ProfesorId = c.UsuarioId,
                                   Foto = c.Foto,
                                   Instituciones = c.InstitucionesId,
                                   Roles = f.RoleName,
                                   Municipio = c.Municipio,
                                   Centros = (from i in _dbContext.instituciones
                                              where i.InstitucionesId == c.InstitucionesId
                                              select i.Nombre).FirstOrDefault()


                               }).Skip(pg).Take(10);

                ViewBag.cantidad = Usuario.Count();
                return View(Usuario.ToList());
            }
            catch (Exception ex)
            {
                return View(ex.Message);
            }

        }
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Login()
        {

            return View();
        }
        [AllowAnonymous]
        public async Task<IActionResult> Login(Models.ModelsView.LoginView login)
        {
            if (string.IsNullOrEmpty(login.Password) || string.IsNullOrEmpty(login.Users))
            {
                ViewBag.Error = "Error en el Users o Password";
                return View(login);
            }
            var pass = login.Password;//Seguridad.Encriptar.Encriptars(login.Password);
            var logins = await _dbContext.usuarios.FirstOrDefaultAsync(c => c.UserName == login.Users && c.Password == pass);

            if (logins != null)
            {

                var foto = logins.Foto == null ? "no" : logins.Foto;
                var Rol = _dbContext.roles.Find(logins.RolesId);
                List<Claim> Datos = new List<Claim>()
                {
                    new Claim(ClaimTypes.Name,logins.Nombre),
                    new Claim(ClaimTypes.Role,Rol.RoleName),
                    new Claim("ID",logins.UsuarioId.ToString()),
                    new Claim("Municipio",logins.Municipio),
                    new Claim("InstitucionId",logins.InstitucionesId.ToString()),
                    new Claim("Role",Rol.RoleName),
                    new Claim("Foto",foto)

                };

                ClaimsIdentity claimsIdentity = new ClaimsIdentity(Datos, CookieAuthenticationDefaults.AuthenticationScheme);
                HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));
                return RedirectToAction("Index", "Home");
            }
            ViewBag.Error = "Error en el Users o Password";
            return View();
        }

        public IActionResult AdminVerUsuario()
        {
            var Usuario = from c in _dbContext.usuarios
                          join f in _dbContext.roles on c.RolesId equals f.RolesId

                          select new ListUsuarioView
                          {
                              ProfesorName = c.Nombre,
                              Materia = (from i in _dbContext.Cursos
                                         join x in _dbContext.usuario_Materias
                                         on i.CursosId equals x.CursosId
                                         where x.UsuarioId == c.UsuarioId
                                         select i).ToList(),
                              usuario_Materias = (from b in _dbContext.Cursos
                                                  join x in _dbContext.usuario_Materias on
                                                b.CursosId equals x.CursosId
                                                  where x.UsuarioId == c.UsuarioId
                                                  select b).ToList(),
                              ProfesorId = c.UsuarioId,
                              Foto = c.Foto,
                              Instituciones = c.InstitucionesId,
                              Roles = f.RoleName


                          };

            return View("VerUsuario", Usuario.ToList());
        }
        [HttpGet]
        public IActionResult VerUsuario()
        {

            var Usuario = from c in _dbContext.usuarios
                          join f in _dbContext.roles on c.RolesId equals f.RolesId

                          select new ListUsuarioView
                          {
                              ProfesorName = c.Nombre,
                              Materia = (from i in _dbContext.Cursos
                                         join x in _dbContext.usuario_Materias
                                         on i.CursosId equals x.CursosId
                                         where x.UsuarioId == c.UsuarioId
                                         select i).ToList(),
                              usuario_Materias = (from b in _dbContext.Cursos
                                                  join x in _dbContext.usuario_Materias on
                                                b.CursosId equals x.CursosId
                                                  where x.UsuarioId == c.UsuarioId
                                                  select b).ToList(),
                              ProfesorId = c.UsuarioId,
                              Foto = c.Foto,
                              Instituciones = c.InstitucionesId,
                              Roles = f.RoleName


                          };

            int institutoUser = int.Parse(User.FindFirst("InstitucionId").Value);
            if (User.IsInRole("Gestor"))
            {
                var filtros = Usuario.Where(c => c.Instituciones == institutoUser);
                return View(filtros.ToList());
            }
            if (User.IsInRole("SAC"))
            {
                var filtros = Usuario.Where(c => c.Instituciones == institutoUser && c.Roles == "Facilitador");
                return View(filtros.ToList());
            }
            var filtros1 = Usuario.Where(c => c.Instituciones == institutoUser);

            return View(filtros1.ToList());

        }
        [HttpGet]
        public async Task<IActionResult> Registrar()
        {
            RegisterView usuario = new RegisterView();

            usuario.Roles = await _dbContext.roles.Where(x => x.RoleName != "Admin" && x.RoleName != "Supervisor").ToListAsync();
            string UserMunicipio = User.FindFirst("Municipio").Value;

            usuario.Municipios = await _dbContext.municipio.ToListAsync();
            if (User.IsInRole("Supervisor") || User.IsInRole("SAC") || User.IsInRole("Gestor"))
            {
                usuario.Municipios = _dbContext.municipio.Where(c => c.MunicipioName == UserMunicipio.ToString()).ToList();
            }

            return View(usuario);
        }

        [HttpPost]
        public async Task<IActionResult> Registrar(RegisterView usuario, string Municipios, string Roles)
        {
            if (ModelState.IsValid && usuario.InstitucionesId != 0)
            {
                var existeGestor = (from c in _dbContext.usuarios join f in _dbContext.instituciones on c.InstitucionesId equals f.InstitucionesId
                                    join x in _dbContext.roles on c.RolesId equals x.RolesId where x.RoleName == Roles && f.InstitucionesId == usuario.InstitucionesId && x.RoleName == "Gestor"
                                    select c).FirstOrDefault();

                if (existeGestor != null)
                {

                    //ViewBag.Error = "Existe Ese Gestor en esa Institucion";
                    return Content("EXISTE ESE GESTOR");
                }
                Random numero = new Random(DateTime.Now.Millisecond);
                Usuario user = new Usuario();
                user.Nombre = usuario.Nombre;
                user.UserName = usuario.cedula;
                user.Apellido = usuario.Apellido;
                user.Correo = usuario.Correo;
                user.FechaCreacion = DateTime.Now;
                user.FechaNacimiento = usuario.FechaNacimiento;
                user.Genero = usuario.Genero;
                user.Direccion = usuario.Direccion;
                user.Telefono = usuario.Telefono;
                user.Password = $"{numero.Next()}";//Seguridad.Encriptar.Encriptars(usuario.Password);
                user.Confirmar = 1;
                user.Cedula = usuario.cedula;
                int InstitucionesId = _dbContext.instituciones.Find(usuario.InstitucionesId).InstitucionesId;
                user.InstitucionesId = InstitucionesId;
                var rol = await _dbContext.roles.FirstOrDefaultAsync(m => m.RoleName == Roles);
                user.RolesId = rol.RolesId;
                var municipio = await _dbContext.municipio.FirstOrDefaultAsync(m => m.MunicipioName == Municipios);
                user.Municipio = municipio.MunicipioName;
                user.Sector = null;
                await _dbContext.AddAsync(user);
                await _dbContext.SaveChangesAsync();
                var gestor = _dbContext.usuarios.Find(user.UsuarioId);
                if (gestor != null && gestor.RolesId != null)
                {
                    var roles = await _dbContext.roles.FirstOrDefaultAsync(r => r.RolesId == gestor.RolesId);
                    if (roles.RoleName == "Gestor")
                    {
                        var institucion = _dbContext.instituciones.Find(gestor.InstitucionesId);

                        institucion.Gestor = user.Nombre;
                        _dbContext.SaveChanges();

                    }
                }
                smtp.EnviarCorreo("", "", user.Correo, user.Password);
                return RedirectToAction("VerProfesor", "Usuario");
            }
            RegisterView registerView = new RegisterView();

            registerView.Municipios = await _dbContext.municipio.ToListAsync();
            registerView.Roles = usuario.Roles = await _dbContext.roles.Where(x => x.RoleName != "Admin" && x.RoleName != "Supervisor").ToListAsync(); ;
            return View(registerView);

        }
        [HttpGet]
        public async Task<IActionResult> AsignarMateria(ListUsuarioView profesores)
        {
            var Profesores = (from c in _dbContext.usuarios
                              join f in _dbContext.roles on c.RolesId equals f.RolesId
                              where f.RoleName == "Facilitador" && c.UsuarioId == profesores.ProfesorId
                              select new ListUsuarioView
                              {
                                  ProfesorName = c.Nombre,
                                  Materia = _dbContext.Cursos.ToList(),
                                  usuario_Materias = (from b in _dbContext.Cursos join x in _dbContext.usuario_Materias on
                                                      b.CursosId equals x.CursosId
                                                      where x.UsuarioId == c.UsuarioId
                                                      select b).ToList(),
                                  ProfesorId = c.UsuarioId

                              }).FirstOrDefault();
            List<Cursos> Materie = Profesores.Materia.Except(Profesores.usuario_Materias).ToList();
            Profesores.Materia = Materie;
            return View(Profesores);
        }
        [HttpPost]
        public IActionResult AsignarMateria([FromBody] Datoss datos = null)
        {
            if (datos == null)
            {
                return NotFound();
            }
            else
            {
                for (int contador = 0; datos.materia.Count() > contador; contador++)
                {
                    if (_dbContext.Cursos.Find(datos.materia[contador]) != null)
                    {
                        Usuario_Materia usuario_Materia = new Usuario_Materia();
                        usuario_Materia.CursosId = datos.materia[contador];
                        usuario_Materia.UsuarioId = datos.UsuarioId;
                        _dbContext.usuario_Materias.Add(usuario_Materia);
                        _dbContext.SaveChanges();
                    }
                }
            }
            return Ok();
        }
        [HttpGet]

        public async Task<IActionResult> EditAsignarMataria(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var Facilitador = _dbContext.usuarios.FirstOrDefaultAsync(m => m.UsuarioId == id);
            if (Facilitador == null)
            {
                return NotFound();
            }
            var Profesores = await (from c in _dbContext.usuarios
                                    join f in _dbContext.roles on c.RolesId equals f.RolesId
                                    where c.UsuarioId == id
                                    select new ListUsuarioView
                                    {
                                        ProfesorName = c.Nombre,
                                        Materia = _dbContext.Cursos.ToList(),
                                        usuario_Materias = (from b in _dbContext.Cursos
                                                            join x in _dbContext.usuario_Materias on
                                                            b.CursosId equals x.CursosId
                                                            where x.UsuarioId == c.UsuarioId
                                                            select b).ToList(),
                                        ProfesorId = c.UsuarioId

                                    }).FirstOrDefaultAsync();
            List<Cursos> Materie = Profesores.Materia.Except(Profesores.usuario_Materias).ToList();
            Profesores.Materia = Materie;

            return View(Profesores);
        }
        public async Task<IActionResult> Cerrar()
        {

            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Usuario");
        }
        [HttpGet]
        public IActionResult listInstituto(string Municipios)
        {
            if (!string.IsNullOrEmpty(Municipios))
            {

                if (User.IsInRole("SAC") || User.IsInRole("Gestor"))
                {
                    int InstitutoId = int.Parse(User.FindFirst("InstitucionId").Value.ToString());
                    var SGlistas = _dbContext.instituciones.Where(c => c.InstitucionesId == InstitutoId);
                    return Json(SGlistas);
                }
                var lista = _dbContext.instituciones.Where(m => m.Municipio == Municipios).ToList();

                return Json(lista);
            }
            return Ok(false);

        }
        [HttpGet]
        public IActionResult ExisteGestor(int? Instituciones, string Roles)
        {

            if (Instituciones == null || string.IsNullOrEmpty(Roles))
            {
                return Ok(false);
            }
            var ExisteGestor = (from c in _dbContext.instituciones
                                join x in _dbContext.usuarios
                             on c.InstitucionesId equals x.InstitucionesId
                                join r in _dbContext.roles on
                             x.RolesId equals r.RolesId
                                where c.InstitucionesId == Instituciones &&
                                r.RoleName == Roles.Trim() && r.RoleName == "Gestor"
                                select x).ToList();
            if (ExisteGestor.Count > 0)
            {
                return Ok(true);
            }
            return Ok(false);
        }
        [HttpGet]
        public async Task<IActionResult> Perfil(int UsuarioId)
        {
            try
            {
                if (UsuarioId != null)
                {
                    return NotFound();
                }

                var Usuario = await _dbContext.usuarios.FindAsync(UsuarioId);
                Usuario.Password = Encriptar.Encriptars(Usuario.Password);
                if (Usuario == null)
                {
                    return NotFound();
                }
                return View(UsuarioId);
            }
            catch (Exception ex)
            {
                return View(ex);
            }

        }
        //Editar Perfil del Usuario
        [HttpPost]
        public async Task<IActionResult> Edit(Usuario usuario)
        {

            try
            {
                Usuario usuario1 = new Usuario();
                usuario1.Municipio = usuario.Municipio;
                usuario1.FechaNacimiento = usuario.FechaNacimiento;
                usuario1.Apellido = usuario.Apellido;
                usuario1.Nombre = usuario.Nombre;
                usuario1.Password = usuario.Password;
                usuario1.Telefono = usuario.Telefono;
                usuario1.Foto = usuario.Foto;
                usuario1.UserName = usuario.UserName;
                return RedirectToAction("Index", "Home");
            }
            catch (Exception ex)
            {
                return RedirectToAction("");
            }

        }
        [HttpPost]
        public IActionResult buscarFacilitador([FromBody] int? UsuarioId)
        {
            if (UsuarioId == null)
            {
                return NotFound();
            }
            var usuario = _dbContext.usuarios.Find(UsuarioId);
            if (usuario == null)
            {
                return NotFound();
            }
            return Ok(usuario);
        }

        [HttpGet]
        public async Task<IActionResult> List_Usuario_Sup(int? p)
        {
            try
            {
            var centros = _dbContext.instituciones.Where(c => c.Municipio == User.FindFirst("Municipio").Value);
                return View(centros.ToList()) ;
            }
            catch(Exception r)
            {
                return View(r.Message);
            }
           
        }
        
        public async Task<IActionResult> BuscarUsuario_Instituto(int centro,string ?cedula,string? nombre,int? pg)
        {
            try
            {
                var UsuarioView = new VerUsuarioView();
                var pagina = pg == null || pg < 1 ? 0 : pg;
                if (cedula != "null")
                {
                    
                    UsuarioView.Usuario = _dbContext.usuarios.FirstOrDefault(c => c.InstitucionesId == centro &&
                    c.Cedula == cedula);
                    return Ok(UsuarioView);
                }
                if(nombre != "null")
                {
                    UsuarioView.Usuarios = (from c in _dbContext.usuarios
                                            where
                                         c.Nombre.StartsWith(nombre)
                                        && c.InstitucionesId == centro
                                            select c).ToList();
                    return Ok(UsuarioView);

                }
                UsuarioView.Usuarios = (from c in _dbContext.usuarios
                                       where c.InstitucionesId == centro
                                       select c).ToList();
                UsuarioView.Cantidad = UsuarioView.Usuarios.Count;
                return Ok(UsuarioView);
            }
            catch(Exception ex)
            {
                return Ok(ex.Message);
            }
            
        }
        public async Task<IActionResult> DatosTbUsuario(int pg)
        {
            try
            {
            
                if (pg != null)
                {
                    var pagina = pg > 1 ? (pg - 1) ^ 10 : 0;
                    var Usuario = new VerUsuarioView();
                    Usuario.Usuarios = _dbContext.usuarios.Where(m => m.Municipio == User.FindFirst("Municipio").Value).ToList();
                    Usuario.Cantidad = Usuario.Usuarios.Count;
                    Usuario.Usuarios = _dbContext.usuarios
                        .Where(m => m.Municipio == User.FindFirst("Municipio").Value)
                        .Skip((int)pagina).Take(10).ToList();

                    return Ok(Usuario);
                }

                return Ok("mmm.... algo paso :/");
            }
            catch(Exception ex)
            {
                return Ok(ex.Message);
            }
        }
    }
}

    public class Datoss
    {
        public int UsuarioId { get; set; }
        public int[] materia { get; set; }
    }

   
   
