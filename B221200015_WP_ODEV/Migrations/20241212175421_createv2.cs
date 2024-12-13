using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace B221200015_WP_ODEV.Migrations
{
    /// <inheritdoc />
    public partial class createv2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Hasta_Bolumler_BolumId",
                table: "Hasta");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Hasta",
                table: "Hasta");

            migrationBuilder.RenameTable(
                name: "Hasta",
                newName: "Hastalar");

            migrationBuilder.RenameIndex(
                name: "IX_Hasta_BolumId",
                table: "Hastalar",
                newName: "IX_Hastalar_BolumId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Hastalar",
                table: "Hastalar",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "HocaMüsaitlikler",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HocaId = table.Column<int>(type: "int", nullable: false),
                    Tarih = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Saat = table.Column<TimeSpan>(type: "time", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HocaMüsaitlikler", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HocaMüsaitlikler_Hocalar_HocaId",
                        column: x => x.HocaId,
                        principalTable: "Hocalar",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_HocaMüsaitlikler_HocaId",
                table: "HocaMüsaitlikler",
                column: "HocaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Hastalar_Bolumler_BolumId",
                table: "Hastalar",
                column: "BolumId",
                principalTable: "Bolumler",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Hastalar_Bolumler_BolumId",
                table: "Hastalar");

            migrationBuilder.DropTable(
                name: "HocaMüsaitlikler");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Hastalar",
                table: "Hastalar");

            migrationBuilder.RenameTable(
                name: "Hastalar",
                newName: "Hasta");

            migrationBuilder.RenameIndex(
                name: "IX_Hastalar_BolumId",
                table: "Hasta",
                newName: "IX_Hasta_BolumId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Hasta",
                table: "Hasta",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Hasta_Bolumler_BolumId",
                table: "Hasta",
                column: "BolumId",
                principalTable: "Bolumler",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
