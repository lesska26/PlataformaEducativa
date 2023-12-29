using Microsoft.EntityFrameworkCore;

namespace PlataformaEducativa.Models
{
    public class PlataformaEducativaDbContext:DbContext
    {
        public PlataformaEducativaDbContext(DbContextOptions<PlataformaEducativaDbContext> options) : base(options) 
        { 
        
        }
        public DbSet<CursoNota> CursoNota { get; set;}

        public DbSet<CursoParticipante> CursoParticipante { get; set;}

        public DbSet<CursoProfesor> cursoProfesors { get; set;}

        public DbSet<Cursos> Cursos { get; set;}    

        
        public DbSet<Estudiantes> estudiantes { get; set;}


        public DbSet<IniciarCurso> iniciarCurso { get; set;}

        public DbSet<Instituciones> instituciones { get; set;}

        public DbSet<Municipio> municipio { get; set;}  

        public DbSet<Roles> roles { get; set;}
        public DbSet<Usuario> usuarios { get; set;} 

        public DbSet<Usuario_Materia> usuario_Materias { get; set;}
        public DbSet<Hora>Horas { get; set;}
        public DbSet<DiaSemana> dia_Semana { get; set;}
        public DbSet<List_Est_Curso> List_Est_Curso { get; set;}
        public DbSet<DiaSemanaCurso> diaSemanaCursos { get; set;}

        public DbSet<CursoFinalizar> cursoFinalizar { get; set;}

    }
}
