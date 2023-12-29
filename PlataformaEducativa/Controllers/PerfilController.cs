using DocumentFormat.OpenXml.Drawing;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using PlataformaEducativa.Models;
using PlataformaEducativa.Models.ModelsView;
using System.IO;
using System.Security.Claims;
using System.Security.Principal;

namespace PlataformaEducativa.Controllers
{
    public class PerfilController : Controller
    {
        private readonly PlataformaEducativaDbContext _context;
        private readonly IHostEnvironment _environment;
        public PerfilController(PlataformaEducativaDbContext db,IHostEnvironment environment)
        {
            _context = db;
            _environment= environment;
        }
        [HttpGet]
        public IActionResult Perfil(int? id)
        {
            if(id == null)
            {
                return NotFound();
            }
            
            var Usuario = _context.usuarios.Find(id);
            if (Usuario == null)
            {
                return NotFound();
            }
            return View(Usuario);
        }

        [HttpPost]
        public async Task<IActionResult> GuardarPerfil([FromForm]Models.ModelsView.Perfil Perfil)
        {
            try
            {
                var user = _context.usuarios.FindAsync(Perfil.Id);
                if (user.Result != null)
                {
                    user.Result.Apellido = Perfil.Apellido;
                    user.Result.Nombre = Perfil.Nombre;
                    user.Result.Password = Perfil.Password;
                    var ClaimPrincipal = User;
                    var claimAntiguoNombre = ClaimPrincipal.Identity.Name;
                    var Identityss= (ClaimsIdentity)ClaimPrincipal.Identity;
                    var remover = Identityss.FindFirst(c => c.Type == ClaimTypes.Name && c.Value == claimAntiguoNombre); ;
                    Identityss.RemoveClaim(remover);
                    var NuevoNombreClaim = new Claim(ClaimTypes.Name, user.Result.Nombre);
                    
                    Identityss.AddClaim(NuevoNombreClaim);
                    HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(Identityss));

                    if (Perfil.Imagen != null)
                    {
                        var path = System.IO.Path.GetExtension(Perfil.Imagen.FileName);
                        var nombreArchivo= Guid.NewGuid().ToString()+path;
                        string ruta = System.IO.Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "img",nombreArchivo);
                       
                       
                        


                        using (var stream =new FileStream(ruta, FileMode.Create))
                        {
                            if (user.Result.Foto == null)
                            {
                                Perfil.Imagen.CopyTo(stream);
                                user.Result.Foto = nombreArchivo;
                                var claimAntiguo = ClaimPrincipal.FindFirst("Foto");
                                var Identity = (ClaimsIdentity)ClaimPrincipal.Identity;
                                Identity.RemoveClaim(claimAntiguo);
                                var NuevoClaim = new Claim("Foto",nombreArchivo);
                                ///var Identitys = (ClaimsIdentity)ClaimPrincipal.Identity;
                                Identity.AddClaim(NuevoClaim);
                                HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(Identity));
                            }
                            else
                            {
                                string rutaDelete = System.IO.Path.Combine(Directory.GetCurrentDirectory(),"wwwroot","img",user.Result.Foto);
                                System.IO.File.Delete(rutaDelete);
                                Perfil.Imagen.CopyTo(stream);
                                user.Result.Foto = nombreArchivo;
                                var claimAntiguo = ClaimPrincipal.FindFirst("Foto");

                                var Identity = (ClaimsIdentity)ClaimPrincipal.Identity;
                                Identity.RemoveClaim(claimAntiguo);
                                var NuevoClaim = new Claim("Foto", nombreArchivo);
                                var Identitys = (ClaimsIdentity)ClaimPrincipal.Identity;

                                Identity.AddClaim(NuevoClaim);
                                HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(Identity));
                            }
                        }
                      
                    }
                    await _context.SaveChangesAsync();

                }
                return Ok();
            }
            catch(Exception ex)
            {
                return View(ex.Message);
            }
            
        }

        public async Task<IActionResult>VerPerfilUsuario(int id)
        {
            return View();
        }
    }
}
