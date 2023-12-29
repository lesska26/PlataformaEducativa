using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlataformaEducativa.Migrations
{
    public partial class v5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_usuario_Materias_materia_MateriaId",
                table: "usuario_Materias");

            migrationBuilder.DropTable(
                name: "materia");

            migrationBuilder.RenameColumn(
                name: "MateriaId",
                table: "usuario_Materias",
                newName: "CursosId");

            migrationBuilder.RenameIndex(
                name: "IX_usuario_Materias_MateriaId",
                table: "usuario_Materias",
                newName: "IX_usuario_Materias_CursosId");

            migrationBuilder.AddForeignKey(
                name: "FK_usuario_Materias_Cursos_CursosId",
                table: "usuario_Materias",
                column: "CursosId",
                principalTable: "Cursos",
                principalColumn: "CursosId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_usuario_Materias_Cursos_CursosId",
                table: "usuario_Materias");

            migrationBuilder.RenameColumn(
                name: "CursosId",
                table: "usuario_Materias",
                newName: "MateriaId");

            migrationBuilder.RenameIndex(
                name: "IX_usuario_Materias_CursosId",
                table: "usuario_Materias",
                newName: "IX_usuario_Materias_MateriaId");

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

            migrationBuilder.AddForeignKey(
                name: "FK_usuario_Materias_materia_MateriaId",
                table: "usuario_Materias",
                column: "MateriaId",
                principalTable: "materia",
                principalColumn: "MateriaId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
