using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace B221200015_WP_ODEV.Migrations
{
    /// <inheritdoc />
    public partial class createv9 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Durum",
                table: "AcilDurumlar",
                newName: "Konu");

            migrationBuilder.AddColumn<TimeSpan>(
                name: "Saat",
                table: "AcilDurumlar",
                type: "time",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0));

            migrationBuilder.AddColumn<DateTime>(
                name: "Tarih",
                table: "AcilDurumlar",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Saat",
                table: "AcilDurumlar");

            migrationBuilder.DropColumn(
                name: "Tarih",
                table: "AcilDurumlar");

            migrationBuilder.RenameColumn(
                name: "Konu",
                table: "AcilDurumlar",
                newName: "Durum");
        }
    }
}
