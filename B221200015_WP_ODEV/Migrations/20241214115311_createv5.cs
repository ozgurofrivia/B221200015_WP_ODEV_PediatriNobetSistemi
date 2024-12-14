using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace B221200015_WP_ODEV.Migrations
{
    /// <inheritdoc />
    public partial class createv5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Resim",
                table: "Asistanlar",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Resim",
                table: "Asistanlar");
        }
    }
}
