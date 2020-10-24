using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace TiendaServicios.Api.Autor.Migrations
{
    public partial class MigrationPostgresInitial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AutorLibros",
                columns: table => new
                {
                    AutorLibroId = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Nombre = table.Column<string>(nullable: true),
                    Apellido = table.Column<string>(nullable: true),
                    FechaNacimiento = table.Column<DateTime>(nullable: true),
                    AutorLibroGuid = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AutorLibros", x => x.AutorLibroId);
                });

            migrationBuilder.CreateTable(
                name: "GradosAcademicos",
                columns: table => new
                {
                    GradoAcademicoId = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Nombre = table.Column<string>(nullable: true),
                    CentroAcademico = table.Column<string>(nullable: true),
                    FechaGrado = table.Column<DateTime>(nullable: true),
                    AutorLibroId = table.Column<int>(nullable: false),
                    GradoAcademicoGuid = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GradosAcademicos", x => x.GradoAcademicoId);
                    table.ForeignKey(
                        name: "FK_GradosAcademicos_AutorLibros_AutorLibroId",
                        column: x => x.AutorLibroId,
                        principalTable: "AutorLibros",
                        principalColumn: "AutorLibroId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GradosAcademicos_AutorLibroId",
                table: "GradosAcademicos",
                column: "AutorLibroId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GradosAcademicos");

            migrationBuilder.DropTable(
                name: "AutorLibros");
        }
    }
}
