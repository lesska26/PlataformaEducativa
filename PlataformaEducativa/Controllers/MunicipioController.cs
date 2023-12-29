using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PlataformaEducativa.Models;

namespace PlataformaEducativa.Controllers
{
    public class MunicipioController : Controller
    {
        private readonly PlataformaEducativaDbContext _db;
        public MunicipioController(PlataformaEducativaDbContext context) 
        { 
          _db=context;
        }
        // GET: MunicipioControllers
        public ActionResult Index()
        {
            return View();
        }

        // GET: MunicipioControllers/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: MunicipioControllers/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: MunicipioControllers/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Municipio municipio)
        {
            try
            {
                var Municipio= new Municipio();

                Municipio.MunicipioName=municipio.MunicipioName;
                _db.municipio.Add(municipio);
                _db.SaveChanges();  
                return RedirectToAction(nameof(Index),"Home");
            }
            catch
            {
                return View();
            }
        }

        // GET: MunicipioControllers/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: MunicipioControllers/Edit/5
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

        // GET: MunicipioControllers/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: MunicipioControllers/Delete/5
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
    }
}
