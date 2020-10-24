﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TiendaServicios.Api.Libro.Migrations
{
    public partial class MigrationSqlServerInitial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LibreriaMateriales",
                columns: table => new
                {
                    LibreriaMaterialId = table.Column<Guid>(nullable: false),
                    Titulo = table.Column<string>(nullable: true),
                    FechaPublicacion = table.Column<DateTime>(nullable: true),
                    AutorLibro = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LibreriaMateriales", x => x.LibreriaMaterialId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LibreriaMateriales");
        }
    }
}
