using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlataformaEducativa.Migrations
{
    public partial class CentrosV5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "cursoFinalizar",
                columns: table => new
                {
                    CursoFinalizarId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IniciarCursoId = table.Column<int>(type: "int", nullable: false),
                    FinalizoCurso = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_cursoFinalizar", x => x.CursoFinalizarId);
                    table.ForeignKey(
                        name: "FK_cursoFinalizar_iniciarCurso_IniciarCursoId",
                        column: x => x.IniciarCursoId,
                        principalTable: "iniciarCurso",
                        principalColumn: "IniciarCursoId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_cursoFinalizar_IniciarCursoId",
                table: "cursoFinalizar",
                column: "IniciarCursoId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "cursoFinalizar");
        }
    }
}
