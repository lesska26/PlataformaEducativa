using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PlataformaEducativa.Models;
using PlataformaEducativa.Models.ModelsView;

namespace PlataformaEducativa.Controllers
{
    public class InstitucionesController : Controller
    {
        private readonly PlataformaEducativaDbContext _context;

        public InstitucionesController(PlataformaEducativaDbContext dbContext) 
        { 
            _context = dbContext;
        } 
        
        public async Task<IActionResult> Index()
        {
            if (User.IsInRole("Admin"))
            {
                var Institucioness = await _context.instituciones.ToListAsync();
                foreach(var i in Institucioness)
                {
                    
                }
                return View(Institucioness);
            }
           var Instituciones = await _context.instituciones.Where(c => c.Municipio == User.FindFirst("Municipio").Value.Trim()).ToListAsync();

          return View(Instituciones);

        }
        [HttpGet]
        public async Task< IActionResult> Crear()
        {
            if (User.IsInRole("Supervisor"))
            {
                Models.ModelsView.InstitucionesModelView institucionesModelView1 = new Models.ModelsView.InstitucionesModelView();
                institucionesModelView1.Municipios = await _context.municipio.Where(c => c.MunicipioName == User.FindFirst("Municipio").Value).ToListAsync();
                return View(institucionesModelView1);
            }

            if (User.IsInRole("Admin"))
            {
                Models.ModelsView.InstitucionesModelView institucionesModelView1 = new InstitucionesModelView();
                institucionesModelView1.Municipios = _context.municipio.ToList();
                return View(institucionesModelView1);
            }
            Models.ModelsView.InstitucionesModelView institucionesModelView = new Models.ModelsView.InstitucionesModelView();

            Console.WriteLine(User.FindFirst("Municipio").Value);
            institucionesModelView.Municipios = await _context.municipio.Where(c => c.MunicipioName == User.FindFirst("Municipio").Value).ToListAsync();

            return View(institucionesModelView);
        }
        [HttpPost]
        public IActionResult Crear(Models.ModelsView.InstitucionesModelView institucionesModelView)
        {
            InstitucionesModelView model = new InstitucionesModelView();
            model.Municipios = _context.municipio.ToList();
            if(!ModelState.IsValid)
                return View(model);
            Instituciones instis = new Instituciones();
            instis.Direccion = institucionesModelView.Direccion;
            
            instis.Nombre = institucionesModelView.Nombre;
            instis.Municipio = institucionesModelView.MunicipioSelect;
            instis.FechaRegistro = DateTime.Now;
            instis.Telefono = institucionesModelView.Telefono;
            
            
            _context.instituciones.Add(instis);
            _context.SaveChanges();
            return RedirectToAction("Index","Home");
        }
      
        public async Task<IActionResult> editar(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var instito = await _context.instituciones.FindAsync(id);
            if (instito == null)
            {
                return NotFound();
            }
            var view = new Models.ModelsView.InstitucionesModelView();
            view.MunicipioSelect = instito.Municipio;
            view.InstitucionesId = instito.InstitucionesId;
            view.Direccion=instito.Direccion;
            view.Nombre=instito.Nombre;
            view.Municipios = _context.municipio.ToList();
            
            view.Telefono = instito.Telefono;
            return View(view);
        }

      
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> editar(int id,InstitucionesModelView instituciones)
        {
            if (instituciones.InstitucionesId ==null)
            {
                return NotFound();
            }
            
            if (ModelState.IsValid)
            {
                try
                {
                    
                    var insti =_context.instituciones.Find(instituciones.InstitucionesId);
                    if (insti==null)
                    {
                        return NotFound();
                    }
                    insti.Municipio = instituciones.MunicipioSelect.ToString();
                    insti.Direccion = instituciones.Direccion.ToString();
                    insti.Nombre = instituciones.Nombre.ToString();
          
                    insti.Telefono = instituciones.Telefono;
                    
                    _context.instituciones.Update(insti);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    return View("Error");
                }
                return RedirectToAction(nameof(Index),"Home");
            }
            return View(instituciones);
        }

    }
}
