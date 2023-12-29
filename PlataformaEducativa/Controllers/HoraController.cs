using Microsoft.AspNetCore.Mvc;

namespace PlataformaEducativa.Controllers
{
    public class HoraController : Controller
    {
        public readonly PlataformaEducativa.Models.PlataformaEducativaDbContext _context;
        public HoraController(PlataformaEducativa.Models.PlataformaEducativaDbContext _db) 
        {
        
          _context = _db;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody]Models.Hora hora )
        {

            try
            {
                hora.AMPM = "AMPM";
                _context.Horas.Add(hora);

                _context.SaveChanges();

                return RedirectToAction("Index", "Home");

            }
            catch (Exception ex)
            {
                return View(ex.Message);
            }
            
        }
    }
}
