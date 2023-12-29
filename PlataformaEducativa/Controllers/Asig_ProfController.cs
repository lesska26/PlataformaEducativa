using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.EntityFrameworkCore;
using PlataformaEducativa.Models;
using PlataformaEducativa.Models.ModelsView;
using System.Linq;

namespace PlataformaEducativa.Controllers
{
    //[Authorize(Roles = "Admin")]
    public class Asig_ProfController : Controller
    {
        private readonly PlataformaEducativaDbContext _context;
        public Asig_ProfController(PlataformaEducativaDbContext contenx)
        {
            _context = contenx;
        }
        // GET: Asig_ProfController
        public async Task<ActionResult> M_I()
        {
            try
            {
                var Municipio = new M_C();
                Municipio.MunicipioList = await _context.municipio.ToListAsync();
                return View(Municipio.MunicipioList);
            }
            catch (Exception ex)
            {
                return View(ex.Message);
            }

        }
        [HttpPost]
        public async Task<IActionResult> M_I([FromBody] string Municipio)
        {
            try
            {
                var Centros = new M_C();

                Centros.Instituciones = await _context.instituciones
                        .Where(m => m.Municipio == Municipio).ToListAsync();
                return Ok(Centros.Instituciones);

            } catch (Exception ex)
            {
                return View(ex.Message);
            }
        }
        [HttpGet]
        public async Task<IActionResult> B_P(int? Institucion,int ?pg)
        {
            try
            {
                if (Institucion == 0)
                {
                    return Ok(false);
                }
                if (Institucion == null)
                {
                    return Ok(false);
                }
                var cantidad = (from c in _context.usuarios
                                join s in _context.roles
                                    on c.RolesId equals s.RolesId
                                where c.InstitucionesId == Institucion &&
                                    s.RoleName == "Facilitador" select c).Count();
                var profesores = (from c in _context.usuarios join s in _context.roles
                                  on c.RolesId equals s.RolesId where c.InstitucionesId == Institucion && 
                                  s.RoleName=="Facilitador"
                                  select new
                                  {
                                      id = c.UsuarioId,
                                      nombre = c.Nombre,
                                      foto = c.Foto,
                                      materia = (from x in _context.usuario_Materias join m in _context.Cursos
                                               on x.CursosId equals m.CursosId where x.UsuarioId == c.UsuarioId select m).ToList(),
                                      cantidad
                                  }).ToListAsync();
               
                if (profesores.Result.Count == 0)
                {
                    return Ok(true);
                }         
             
                if (pg > 1)
                {
                    int pagina = (int)((int)pg > 1 ? ((pg-1) * 10) : pg-1);
                    return Json(profesores.Result.Skip(pagina).Take(10));
                }
                return Json(profesores.Result.Skip(0).Take(10));
            }
            catch (Exception Ex)
            {
                return View(Ex.Message);
            }
        }


        [HttpGet]
        public async Task<IActionResult> A_S_M(int? id)
        {
            try
            {
                if (id == null)
                {
                    return Ok(false);
                }
                var profesor = await _context.usuarios.FindAsync(id);
                if (profesor == null)
                {
                    return Ok(false);
                }
                var materia = (from c in _context.usuarios
                               join x in _context.usuario_Materias
                    on c.UsuarioId equals x.UsuarioId
                               join m in _context.Cursos on
                               x.CursosId equals m.CursosId
                               where c.UsuarioId == profesor.UsuarioId
                               select m);

                var materiaFaltante = (from c in _context.Cursos select c).Except(materia);


                var objet = new Agregar_M
                {
                    Usuario=profesor,
                    TusMaterias=materia.ToList(),
                    AgragarMaterias=materiaFaltante.ToList(),

                };


                return View(objet);

            }
            catch (Exception Ex)
            {
                return View(Ex.Message);
            }
        }
        [HttpPost]
        public async Task<IActionResult> DeleteUpgradeMateria([FromBody] MateriasDatos materiasDatos)
        {
            try
            {
                var contador = materiasDatos.agregar.Count();
                if (materiasDatos.agregar.Count() > 0)
                {
                    for (int a = 0; materiasDatos.agregar.Count() >a; a++)
                    {
                        var materiaUsurio = new Usuario_Materia();
                        materiaUsurio.CursosId = materiasDatos.agregar[a];
                        materiaUsurio.UsuarioId = materiasDatos.profesorId;
                        _context.usuario_Materias.Add(materiaUsurio);
                        await _context.SaveChangesAsync();
                    }
                }
                if (materiasDatos.delet.Count() > 0)
                {
                    for(int a = 0; materiasDatos.delet.Count() > a; a++)
                    {
                        var materiaDelete=(from c in _context.usuario_Materias where
                                          materiasDatos.delet[a]==c.CursosId && 
                                          materiasDatos.profesorId==c.UsuarioId
                                          select c).FirstOrDefault();
                        var MateriaUsuario = new Usuario_Materia();
                        var delete = _context.usuario_Materias.Find(materiaDelete.Usuario_MateriaId);
                        _context.usuario_Materias.Remove(delete);
                        await _context.SaveChangesAsync();
                    }
                }
                return Ok(true);
            }
            catch(Exception Ex)
            {
                return Ok(Ex.Message);
            }
        }
        [HttpGet]
        public async Task<IActionResult>BuscarProfesor(int? id)
        {
            if (id == null)
            {
                return Ok(false);

            }

            var profe = _context.usuarios.FindAsync(id).Result;
            if (profe == null)
            {
                return Ok(false);
            }
            return Ok(profe);
        }
        
    }
}
