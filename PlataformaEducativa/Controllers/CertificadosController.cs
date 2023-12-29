using DinkToPdf;
using DocumentFormat.OpenXml.Drawing.Diagrams;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Presentation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGeneration.EntityFrameworkCore;
using PlataformaEducativa.Models;
using PlataformaEducativa.Models.ModelsView;
using Spire.Pdf;
using Spire.Presentation;
using System.Globalization;
using System.Security.Policy;

namespace PlataformaEducativa.Controllers
{
    public class CertificadosController : Controller
    {
        private readonly PlataformaEducativaDbContext _db;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public CertificadosController(PlataformaEducativaDbContext db,IWebHostEnvironment host) 
        {
            _db = db;
            _webHostEnvironment = host;
        }
        // GET: CertificadosController
        public ActionResult EstudiantesCertificado()
        {
            int InstitucinesId =int.Parse( User.FindFirst("InstitucionId").Value);
            var Certificados = (from c in _db.CursoParticipante
                                join x in _db.iniciarCurso on
                               c.IniciarCursoId equals x.IniciarCursoId
                                join e in _db.Cursos
                              on x.CursosId equals e.CursosId
                                join v in _db.CursoNota on
                              c.EstudiantesId equals v.EstudiantesId
                                join s in _db.estudiantes
                              on c.EstudiantesId equals s.EstudiantesId
                                where x.InstitucionesId == InstitucinesId
                                && v.Nota > 70
                                select s).ToList();
            return View(Certificados);
        }
        [HttpPost]
        public IActionResult BuscarEstudianteCertificado(string NCM)
        {
            var BuscarListaEstuCertifi = (from c in _db.estudiantes
                                         join x in _db.CursoParticipante on
                                       c.EstudiantesId equals x.EstudiantesId
                                         join
                                       f in _db.iniciarCurso on x.IniciarCursoId equals
                                       f.IniciarCursoId
                                         select new
                                         {
                                            IniciarId=x.IniciarCurso,
                                            Estudiante=c.Nombre,
                                            EstudianteId=c.EstudiantesId,
                                            c.Matricula,
                                            c.Cedula,

                                         }).Where(m=>m.Cedula==NCM||m.Cedula==NCM || m.Matricula==NCM).ToList();

            return View(BuscarListaEstuCertifi);
        }

        [HttpGet]
        public async Task <IActionResult> Certificado(int IniciarCursoId ,int EstudiantesId)
        {
            try
            {
                var Certificado = await  (from c in _db.iniciarCurso
                                   join x in _db.CursoParticipante on
                                 c.IniciarCursoId equals x.IniciarCursoId
                                   join p in _db.cursoProfesors
                                 on
                                 x.IniciarCursoId equals p.IniciarCursoId
                                   join e in _db.Cursos
                                 on c.CursosId equals e.CursosId
                                   join t in _db.estudiantes on x.EstudiantesId
                                   equals t.EstudiantesId
                                   select new Certificado
                                   {
                                       Nombre = t.Nombre,
                                       profesor =p.UsuarioId.ToString(),
                                       CursosName = e.CursosName,
                                       Finalizo = c.Finalizo,
                                       IniciarCursoId = c.IniciarCursoId,
                                       EstudiantesId = t.EstudiantesId

                                   }).FirstOrDefaultAsync(m => m.EstudiantesId == EstudiantesId
                                   && m.IniciarCursoId == IniciarCursoId); ;
                if (Certificado == null)
                {
                    return NotFound();
                }
                string filePath = Path.Combine(_webHostEnvironment.ContentRootPath, "Plantilla/Plantilla Certificado.pptx");
                if (!System.IO.File.Exists(filePath))
                {
                    return NotFound();
                }
                //https://localhost:7295/Certificados/Certificado?IniciarCursoId=1&EstudiantesId=2
                using (MemoryStream pdfStream = new MemoryStream())
                using (var file = new FileStream(filePath, FileMode.Open, FileAccess.Read))
                {
                    MemoryStream memoryStream = new MemoryStream();
                    file.CopyTo(memoryStream);
                    memoryStream.Seek(0, SeekOrigin.Begin);
                    using (PresentationDocument presentation = PresentationDocument.Open(memoryStream, true))
                    {
                        PresentationPart presentationPart = presentation.PresentationPart;
                        SlidePart diapositivas = presentationPart.SlideParts.First();
                        
                        EditarText(diapositivas, Certificado);
                        diapositivas.Slide.Save();




                    }
                    memoryStream.Seek(0, SeekOrigin.Begin);

                    using (MemoryStream newMemory = new MemoryStream(memoryStream.ToArray()))
                    {


                        newMemory.Seek(0, SeekOrigin.Begin);
                        Spire.Presentation.Presentation presentation1 = new Spire.Presentation.Presentation();

                        presentation1.LoadFromStream(newMemory, Spire.Presentation.FileFormat.Pptx2016);

                        using (MemoryStream pdfMemoryStream = new MemoryStream())
                        {
                            presentation1.SaveToFile(pdfStream, Spire.Presentation.FileFormat.PDF);
                            return File(pdfStream.ToArray(), "application/pdf", "Estudiantes.pdf");
                        }

                    }



                }
                return View(Certificado);
            }
            catch(Exception ex)
            {
                return View(ex.Message);
            }
            
        }
       public void EditarText(SlidePart slidePart,Certificado certificado)
        {
            CultureInfo culture = new CultureInfo("es-ES");
            string mes = culture.DateTimeFormat.GetMonthName(certificado.Finalizo.Value.Month);
            var datos = slidePart.Slide.Descendants<DocumentFormat.OpenXml.Drawing.Text>();
            string profe = _db.usuarios.Find(int.Parse(certificado.profesor)).Nombre;
            foreach(var i in datos)
            {
                if(i.Text== "Curso")
                {
                   i.Text= i.Text.Replace("Curso", certificado.CursosName);
                }
                if(i.Text== "Participante")
                {
                    i.Text=i.Text.Replace("Participante", certificado.Nombre);
                }
                if(i.Text.Trim()== "08")
                {
                    i.Text=i.Text.Replace("08 "," "+ certificado.Finalizo.Value.Day.ToString());
                }
                if(i.Text.Trim()== "del 2023")
                {
                    i.Text = i.Text.Replace("del 2023", $"del {certificado.Finalizo.Value.Year}");
                }
                if( i.Text.Trim()== "noviembre")
                {
                    i.Text = i.Text.Replace("noviembre", mes);
                }if(i.Text=="Profesor")
                {
                    i.Text = i.Text.Replace("Profesor",profe);
                }
            }
            slidePart.Slide.Save();

        } 
       
    }
}
