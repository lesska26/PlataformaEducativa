using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlataformaEducativa.Migrations
{
    public partial class Educativo1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "instituciones",
                columns: table => new
                {
                    InstitucionesId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Municipio = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Sector = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Direccion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FechaRegistro = table.Column<DateTime>(type: "datetime2", nullable: false)
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
                    MateriaName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Hora = table.Column<DateTime>(type: "datetime2", nullable: false)
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
                name: "sector",
                columns: table => new
                {
                    SectorId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SectorName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MunicipioId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_sector", x => x.SectorId);
                    table.ForeignKey(
                        name: "FK_sector_municipio_MunicipioId",
                        column: x => x.MunicipioId,
                        principalTable: "municipio",
                        principalColumn: "MunicipioId",
                        onDelete: ReferentialAction.NoAction);
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
                    Password = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    FechaNacimiento = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Correo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Telefono = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Direccion = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Municipio = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    Sector = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: true),
                    RolesId = table.Column<int>(type: "int", nullable: false),
                    Confirmar = table.Column<byte>(type: "tinyint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_usuarios", x => x.UsuarioId);
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
                    Duracion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UsuarioId = table.Column<int>(type: "int", nullable: false)
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
                    FechaDeNacimiento = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Correo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Municipio = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Sector = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UsuarioId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_estudiantes", x => x.EstudiantesId);
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
                    FechaIniciar = table.Column<DateTime>(type: "datetime2", nullable: false),
                    HoraIniciar = table.Column<DateTime>(type: "datetime2", nullable: false),
                    HoraTerminar = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Activo = table.Column<byte>(type: "tinyint", nullable: false)
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
                        name: "FK_iniciarCurso_instituciones_InstitucionesId",
                        column: x => x.InstitucionesId,
                        principalTable: "instituciones",
                        principalColumn: "InstitucionesId",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "CursoParticipante",
                columns: table => new
                {
                    CursoParticipanteId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EstudiantesId = table.Column<int>(type: "int", nullable: false)
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
                });

            migrationBuilder.CreateTable(
                name: "CursoNota",
                columns: table => new
                {
                    CursoNotaId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IniciarCursoId = table.Column<int>(type: "int", nullable: false),
                    UsuarioId = table.Column<int>(type: "int", nullable: false),
                    EstudianteId = table.Column<int>(type: "int", nullable: false),
                    EstudiantesId = table.Column<int>(type: "int", nullable: false),
                    MateriaId = table.Column<int>(type: "int", nullable: false),
                    Nota = table.Column<float>(type: "real", nullable: false),
                    Fecha = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CursoNota", x => x.CursoNotaId);
                    table.ForeignKey(
                        name: "FK_CursoNota_estudiantes_EstudianteId",
                        column: x => x.EstudianteId,
                        principalTable: "estudiantes",
                        principalColumn: "EstudiantesId",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_CursoNota_iniciarCurso_IniciarCursoId",
                        column: x => x.IniciarCursoId,
                        principalTable: "iniciarCurso",
                        principalColumn: "IniciarCursoId",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_CursoNota_materia_MateriaId",
                        column: x => x.MateriaId,
                        principalTable: "materia",
                        principalColumn: "MateriaId",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_CursoNota_usuarios_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "usuarios",
                        principalColumn: "UsuarioId",
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
                    MateriaId = table.Column<int>(type: "int", nullable: false)
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
                        name: "FK_cursoProfesors_materia_MateriaId",
                        column: x => x.MateriaId,
                        principalTable: "materia",
                        principalColumn: "MateriaId",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_cursoProfesors_usuarios_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "usuarios",
                        principalColumn: "UsuarioId",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CursoNota_EstudianteId",
                table: "CursoNota",
                column: "EstudianteId");

            migrationBuilder.CreateIndex(
                name: "IX_CursoNota_IniciarCursoId",
                table: "CursoNota",
                column: "IniciarCursoId");

            migrationBuilder.CreateIndex(
                name: "IX_CursoNota_MateriaId",
                table: "CursoNota",
                column: "MateriaId");

            migrationBuilder.CreateIndex(
                name: "IX_CursoNota_UsuarioId",
                table: "CursoNota",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_CursoParticipante_EstudiantesId",
                table: "CursoParticipante",
                column: "EstudiantesId");

            migrationBuilder.CreateIndex(
                name: "IX_cursoProfesors_IniciarCursoId",
                table: "cursoProfesors",
                column: "IniciarCursoId");

            migrationBuilder.CreateIndex(
                name: "IX_cursoProfesors_MateriaId",
                table: "cursoProfesors",
                column: "MateriaId");

            migrationBuilder.CreateIndex(
                name: "IX_cursoProfesors_UsuarioId",
                table: "cursoProfesors",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_Cursos_UsuarioId",
                table: "Cursos",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_estudiantes_UsuarioId",
                table: "estudiantes",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_iniciarCurso_CursosId",
                table: "iniciarCurso",
                column: "CursosId");

            migrationBuilder.CreateIndex(
                name: "IX_iniciarCurso_InstitucionesId",
                table: "iniciarCurso",
                column: "InstitucionesId");

            migrationBuilder.CreateIndex(
                name: "IX_sector_MunicipioId",
                table: "sector",
                column: "MunicipioId");

            migrationBuilder.CreateIndex(
                name: "IX_usuario_Materias_MateriaId",
                table: "usuario_Materias",
                column: "MateriaId");

            migrationBuilder.CreateIndex(
                name: "IX_usuario_Materias_UsuarioId",
                table: "usuario_Materias",
                column: "UsuarioId");

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
                name: "sector");

            migrationBuilder.DropTable(
                name: "usuario_Materias");

            migrationBuilder.DropTable(
                name: "estudiantes");

            migrationBuilder.DropTable(
                name: "iniciarCurso");

            migrationBuilder.DropTable(
                name: "municipio");

            migrationBuilder.DropTable(
                name: "materia");

            migrationBuilder.DropTable(
                name: "Cursos");

            migrationBuilder.DropTable(
                name: "instituciones");

            migrationBuilder.DropTable(
                name: "usuarios");

            migrationBuilder.DropTable(
                name: "roles");
        }
    }
}
