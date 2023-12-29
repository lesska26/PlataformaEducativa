using PlataformaEducativa.Models;

namespace PlataformaEducativa.Logica
{
    public class ConfiguracionService
    {
        private readonly PlataformaEducativaDbContext _context;
        public ConfiguracionService(PlataformaEducativaDbContext _db) 
        { 
            _context= _db;
        }

        public void Initializer()
        {
            if( !_context.dia_Semana.Any()) 
            {
                List<string> Semena = new List<string>()
                {
                    "Lunes",
                    "Martes",
                    "Miercoles",
                    "Jueves",
                    "Viernes"
                };
                foreach(string s in Semena)
                {
                    var semana = new DiaSemana();
                    semana.Dias = s;
                    _context.dia_Semana.Add(semana);
                    _context.SaveChanges();
                }

            }
            if (!_context.municipio.Any())
            {
                
                List<string> municipio = new List<string>()
                {
                    "Norte",
                    "Sur",
                    "Este",
                    "Metropolitana"
                };
                
                foreach(var i in municipio)
                {
                    var MUNI = new Municipio();
                    MUNI.MunicipioName= i;
                    _context.municipio.Add(MUNI);
                    _context.SaveChanges();
                }
            }

            if(!_context.roles.Any())
            {
                
                List<string> roles = new List<string>()
               {
                   "Admin",
                   "Facilitador",
                   "Gestor",
                   "SAC",
                   "Supervisor"
               };
                foreach(var role in roles)
                {
                    Roles roles1 = new Roles();
                    roles1.RoleName= role;
                    _context.roles.Add(roles1);
                    _context.SaveChanges();
                }
            }
            if (!_context.usuarios.Any())
            {
               
                Usuario usurio = new Usuario();
                usurio.Nombre = "Admin";
                usurio.Municipio = "es admin";
                usurio.Password = "1234";
                usurio.Telefono = "000000000";
                usurio.Confirmar = 1;
                usurio.FechaCreacion = DateTime.Now;
                usurio.FechaNacimiento = DateTime.Now;
                usurio.Genero = 'M';
                usurio.UserName = "Admin";
                usurio.Apellido = "Admin";
                usurio.Cedula = usurio.Telefono;
                usurio.Correo = "correo@hotmail.com";
                usurio.Direccion = "Sin direccion";
                _context.usuarios.Add(usurio);
                usurio.RolesId = 1;
                _context.SaveChanges();

            }
        }
    }
}
