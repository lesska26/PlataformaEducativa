using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlataformaEducativa.Migrations
{
    public partial class EdctvV1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "dia_Semana",
                columns: table => new
                {
                    DiaSemanaId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Dias = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_dia_Semana", x => x.DiaSemanaId);
                });

            migrationBuilder.CreateTable(
                name: "Horas",
                columns: table => new
                {
                    HoraId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Horas_Iniciar = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Horas_Final = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CatidadHora = table.Column<int>(type: "int", nullable: false),
                    AMPM = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Horas", x => x.HoraId);
                });

            migrationBuilder.CreateTable(
                name: "instituciones",
                columns: table => new
                {
                    InstitucionesId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Municipio = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Direccion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FechaRegistro = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Gestor = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Telefono = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_instituciones", x => x.InstitucionesId);
                });

            migrationBuilder.CreateTable(
                name: "materia",
                columns: table => new
                {
                    MateriaId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MateriaName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_materia", x => x.MateriaId);
                });

            migrationBuilder.CreateTable(
                name: "municipio",
                columns: table => new
                {
                    MunicipioId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MunicipioName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_municipio", x => x.MunicipioId);
                });

            migrationBuilder.CreateTable(
                name: "roles",
                columns: table => new
                {
                    RolesId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleName = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_roles", x => x.RolesId);
                });

            migrationBuilder.CreateTable(
                name: "usuarios",
                columns: table => new
                {
                    UsuarioId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(35)", maxLength: 35, nullable: false),
                    Apellido = table.Column<string>(type: "nvarchar(35)", maxLength: 35, nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    FechaNacimiento = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Correo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Telefono = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Genero = table.Column<string>(type: "nvarchar(1)", nullable: true),
                    FechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Direccion = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Municipio = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    Sector = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: true),
                    RolesId = table.Column<int>(type: "int", nullable: false),
                    Confirmar = table.Column<byte>(type: "tinyint", nullable: false),
                    InstitucionesId = table.Column<int>(type: "int", nullable: true),
                    Cedula = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Foto = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_usuarios", x => x.UsuarioId);
                    table.ForeignKey(
                        name: "FK_usuarios_instituciones_InstitucionesId",
                        column: x => x.InstitucionesId,
                        principalTable: "instituciones",
                        principalColumn: "InstitucionesId");
                    table.ForeignKey(
                        name: "FK_usuarios_roles_RolesId",
                        column: x => x.RolesId,
                        principalTable: "roles",
                        principalColumn: "RolesId",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "Cursos",
                columns: table => new
                {
                    CursosId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CursosName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Duracion = table.Column<int>(type: "int", nullable: false),
                    UsuarioId = table.Column<int>(type: "int", nullable: false),
                    FechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cursos", x => x.CursosId);
                    table.ForeignKey(
                        name: "FK_Cursos_usuarios_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "usuarios",
                        principalColumn: "UsuarioId",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "estudiantes",
                columns: table => new
                {
                    EstudiantesId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    Apellido = table.Column<string>(type: "nvarchar(35)", maxLength: 35, nullable: false),
                    Cedula = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Matricula = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FechaDeNacimiento = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Correo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Genero = table.Column<string>(type: "nvarchar(1)", nullable: false),
                    Municipio = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UsuarioId = table.Column<int>(type: "int", nullable: false),
                    Telefono = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Verificar = table.Column<bool>(type: "bit", nullable: false),
                    InstitucionesId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_estudiantes", x => x.EstudiantesId);
                    table.ForeignKey(
                        name: "FK_estudiantes_instituciones_InstitucionesId",
                        column: x => x.InstitucionesId,
                        principalTable: "instituciones",
                        principalColumn: "InstitucionesId");
                    table.ForeignKey(
                        name: "FK_estudiantes_usuarios_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "usuarios",
                        principalColumn: "UsuarioId",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "usuario_Materias",
                columns: table => new
                {
                    Usuario_MateriaId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MateriaId = table.Column<int>(type: "int", nullable: false),
                    UsuarioId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_usuario_Materias", x => x.Usuario_MateriaId);
                    table.ForeignKey(
                        name: "FK_usuario_Materias_materia_MateriaId",
                        column: x => x.MateriaId,
                        principalTable: "materia",
                        principalColumn: "MateriaId",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_usuario_Materias_usuarios_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "usuarios",
                        principalColumn: "UsuarioId",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "iniciarCurso",
                columns: table => new
                {
                    IniciarCursoId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CursosId = table.Column<int>(type: "int", nullable: false),
                    InstitucionesId = table.Column<int>(type: "int", nullable: false),
                    FechaIniciar = table.Column<DateTime>(type: "datetime2", nullable: true),
                    HoraIniciar = table.Column<TimeSpan>(type: "time", nullable: true),
                    HoraTerminar = table.Column<TimeSpan>(type: "time", nullable: true),
                    Activo = table.Column<byte>(type: "tinyint", nullable: false),
                    Termino = table.Column<byte>(type: "tinyint", nullable: true),
                    HoraId = table.Column<int>(type: "int", nullable: false),
                    Finaliza = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Finalizo = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_iniciarCurso", x => x.IniciarCursoId);
                    table.ForeignKey(
                        name: "FK_iniciarCurso_Cursos_CursosId",
                        column: x => x.CursosId,
                        principalTable: "Cursos",
                        principalColumn: "CursosId",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_iniciarCurso_Horas_HoraId",
                        column: x => x.HoraId,
                        principalTable: "Horas",
                        principalColumn: "HoraId",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_iniciarCurso_instituciones_InstitucionesId",
                        column: x => x.InstitucionesId,
                        principalTable: "instituciones",
                        principalColumn: "InstitucionesId",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "List_Est_Curso",
                columns: table => new
                {
                    List_Est_CursoId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CursoId = table.Column<int>(type: "int", nullable: false),
                    InstitucioneId = table.Column<int>(type: "int", nullable: false),
                    Fecha = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EstudiantesId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_List_Est_Curso", x => x.List_Est_CursoId);
                    table.ForeignKey(
                        name: "FK_List_Est_Curso_Cursos_CursoId",
                        column: x => x.CursoId,
                        principalTable: "Cursos",
                        principalColumn: "CursosId",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_List_Est_Curso_estudiantes_EstudiantesId",
                        column: x => x.EstudiantesId,
                        principalTable: "estudiantes",
                        principalColumn: "EstudiantesId",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_List_Est_Curso_instituciones_InstitucioneId",
                        column: x => x.InstitucioneId,
                        principalTable: "instituciones",
                        principalColumn: "InstitucionesId",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "CursoNota",
                columns: table => new
                {
                    CursoNotaId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IniciarCursoId = table.Column<int>(type: "int", nullable: false),
                    EstudiantesId = table.Column<int>(type: "int", nullable: false),
                    Nota = table.Column<float>(type: "real", nullable: false),
                    Fecha = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(1)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CursoNota", x => x.CursoNotaId);
                    table.ForeignKey(
                        name: "FK_CursoNota_estudiantes_EstudiantesId",
                        column: x => x.EstudiantesId,
                        principalTable: "estudiantes",
                        principalColumn: "EstudiantesId",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_CursoNota_iniciarCurso_IniciarCursoId",
                        column: x => x.IniciarCursoId,
                        principalTable: "iniciarCurso",
                        principalColumn: "IniciarCursoId",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "CursoParticipante",
                columns: table => new
                {
                    CursoParticipanteId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EstudiantesId = table.Column<int>(type: "int", nullable: false),
                    IniciarCursoId = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(1)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CursoParticipante", x => x.CursoParticipanteId);
                    table.ForeignKey(
                        name: "FK_CursoParticipante_estudiantes_EstudiantesId",
                        column: x => x.EstudiantesId,
                        principalTable: "estudiantes",
                        principalColumn: "EstudiantesId",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_CursoParticipante_iniciarCurso_IniciarCursoId",
                        column: x => x.IniciarCursoId,
                        principalTable: "iniciarCurso",
                        principalColumn: "IniciarCursoId",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "cursoProfesors",
                columns: table => new
                {
                    CursoProfesorId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IniciarCursoId = table.Column<int>(type: "int", nullable: false),
                    UsuarioId = table.Column<int>(type: "int", nullable: false),
                    Termino = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_cursoProfesors", x => x.CursoProfesorId);
                    table.ForeignKey(
                        name: "FK_cursoProfesors_iniciarCurso_IniciarCursoId",
                        column: x => x.IniciarCursoId,
                        principalTable: "iniciarCurso",
                        principalColumn: "IniciarCursoId",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_cursoProfesors_usuarios_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "usuarios",
                        principalColumn: "UsuarioId",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "diaSemanaCursos",
                columns: table => new
                {
                    DiaSemanaCursoId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DiaSemanaId = table.Column<int>(type: "int", nullable: false),
                    IniciarCursoId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_diaSemanaCursos", x => x.DiaSemanaCursoId);
                    table.ForeignKey(
                        name: "FK_diaSemanaCursos_dia_Semana_DiaSemanaId",
                        column: x => x.DiaSemanaId,
                        principalTable: "dia_Semana",
                        principalColumn: "DiaSemanaId",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_diaSemanaCursos_iniciarCurso_IniciarCursoId",
                        column: x => x.IniciarCursoId,
                        principalTable: "iniciarCurso",
                        principalColumn: "IniciarCursoId",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CursoNota_EstudiantesId",
                table: "CursoNota",
                column: "EstudiantesId");

            migrationBuilder.CreateIndex(
                name: "IX_CursoNota_IniciarCursoId",
                table: "CursoNota",
                column: "IniciarCursoId");

            migrationBuilder.CreateIndex(
                name: "IX_CursoParticipante_EstudiantesId",
                table: "CursoParticipante",
                column: "EstudiantesId");

            migrationBuilder.CreateIndex(
                name: "IX_CursoParticipante_IniciarCursoId",
                table: "CursoParticipante",
                column: "IniciarCursoId");

            migrationBuilder.CreateIndex(
                name: "IX_cursoProfesors_IniciarCursoId",
                table: "cursoProfesors",
                column: "IniciarCursoId");

            migrationBuilder.CreateIndex(
                name: "IX_cursoProfesors_UsuarioId",
                table: "cursoProfesors",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_Cursos_UsuarioId",
                table: "Cursos",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_diaSemanaCursos_DiaSemanaId",
                table: "diaSemanaCursos",
                column: "DiaSemanaId");

            migrationBuilder.CreateIndex(
                name: "IX_diaSemanaCursos_IniciarCursoId",
                table: "diaSemanaCursos",
                column: "IniciarCursoId");

            migrationBuilder.CreateIndex(
                name: "IX_estudiantes_InstitucionesId",
                table: "estudiantes",
                column: "InstitucionesId");

            migrationBuilder.CreateIndex(
                name: "IX_estudiantes_UsuarioId",
                table: "estudiantes",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_iniciarCurso_CursosId",
                table: "iniciarCurso",
                column: "CursosId");

            migrationBuilder.CreateIndex(
                name: "IX_iniciarCurso_HoraId",
                table: "iniciarCurso",
                column: "HoraId");

            migrationBuilder.CreateIndex(
                name: "IX_iniciarCurso_InstitucionesId",
                table: "iniciarCurso",
                column: "InstitucionesId");

            migrationBuilder.CreateIndex(
                name: "IX_List_Est_Curso_CursoId",
                table: "List_Est_Curso",
                column: "CursoId");

            migrationBuilder.CreateIndex(
                name: "IX_List_Est_Curso_EstudiantesId",
                table: "List_Est_Curso",
                column: "EstudiantesId");

            migrationBuilder.CreateIndex(
                name: "IX_List_Est_Curso_InstitucioneId",
                table: "List_Est_Curso",
                column: "InstitucioneId");

            migrationBuilder.CreateIndex(
                name: "IX_usuario_Materias_MateriaId",
                table: "usuario_Materias",
                column: "MateriaId");

            migrationBuilder.CreateIndex(
                name: "IX_usuario_Materias_UsuarioId",
                table: "usuario_Materias",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_usuarios_InstitucionesId",
                table: "usuarios",
                column: "InstitucionesId");

            migrationBuilder.CreateIndex(
                name: "IX_usuarios_RolesId",
                table: "usuarios",
                column: "RolesId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CursoNota");

            migrationBuilder.DropTable(
                name: "CursoParticipante");

            migrationBuilder.DropTable(
                name: "cursoProfesors");

            migrationBuilder.DropTable(
                name: "diaSemanaCursos");

            migrationBuilder.DropTable(
                name: "List_Est_Curso");

            migrationBuilder.DropTable(
                name: "municipio");

            migrationBuilder.DropTable(
                name: "usuario_Materias");

            migrationBuilder.DropTable(
                name: "dia_Semana");

            migrationBuilder.DropTable(
                name: "iniciarCurso");

            migrationBuilder.DropTable(
                name: "estudiantes");

            migrationBuilder.DropTable(
                name: "materia");

            migrationBuilder.DropTable(
                name: "Cursos");

            migrationBuilder.DropTable(
                name: "Horas");

            migrationBuilder.DropTable(
                name: "usuarios");

            migrationBuilder.DropTable(
                name: "instituciones");

            migrationBuilder.DropTable(
                name: "roles");
        }
    }
}
