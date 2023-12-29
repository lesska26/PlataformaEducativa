using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PlataformaEducativa.Logica;
using PlataformaEducativa.Models;
using PlataformaEducativa.Models.ModelsView;

namespace PlataformaEducativa.Controllers
{
    public class List_EsperaEstController : Controller
    {
        private readonly PlataformaEducativaDbContext _context;
        public List_EsperaEstController(PlataformaEducativaDbContext Context) 
        {
             _context = Context;
        }

        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> List_EsperaEst()
        {

            try
            {
                var List_EsperaEstView = new List_EsperaEstView();
                if (!User.IsInRole("Admin"))
                {
                    List_EsperaEstView.Instituciones = _context.instituciones
                        .Where(c=>c.InstitucionesId==int.Parse(User.FindFirst("InstitucionId").Value)).ToList();
                }
                else
                {
                    List_EsperaEstView.Instituciones = _context.instituciones.ToList();
                    List_EsperaEstView.Cursos = _context.Cursos.ToList();
                }
                
                return View(List_EsperaEstView);

            }
            catch (Exception ex)
            {
                return View(ex.Message);
            }
           
        }
        [HttpPost]
        public async Task<IActionResult> List_EsperaEst([FromBody]ListaDeEspera listaDeEspera,int CursoId,int InstitucionId)
        {
            try
            {
                var existe =await  _context.estudiantes.FirstOrDefaultAsync(m => m.Cedula == listaDeEspera.Cedula);
                if(existe != null)
                {
                    return Ok();
                }
                Estudiantes estudiantes = new Estudiantes();
                estudiantes.Apellido = listaDeEspera.Apellido;
                estudiantes.Nombre = listaDeEspera.Nombre;
                estudiantes.FechaDeNacimiento = listaDeEspera.Edad;
                estudiantes.Status = "Activo";
                estudiantes.Matricula = Matricula.createMatricula(listaDeEspera.Nombre, listaDeEspera.Apellido);
                estudiantes.Cedula = listaDeEspera.Cedula.Trim()==""?"no tiene":listaDeEspera.Cedula;
                estudiantes.Correo = "correo@hotmail.com";
                estudiantes.FechaCreacion = DateTime.Now;
                estudiantes.Genero = listaDeEspera.Sexo;
                _context.estudiantes.Add(estudiantes);
                await _context.SaveChangesAsync();
                List_Est_Curso list_Est_Curso = new List_Est_Curso();
                list_Est_Curso.EstudiantesId = estudiantes.EstudiantesId;
                list_Est_Curso.Fecha = DateTime.Now;
                list_Est_Curso.InstitucioneId = InstitucionId;
                list_Est_Curso.CursoId = CursoId;
                _context.List_Est_Curso.Add(list_Est_Curso);
                await _context.SaveChangesAsync();  
                return Ok();

            }
            catch(Exception ex)
            {
                return Ok(ex.Message);
            }
            


            
        }
    }
}
