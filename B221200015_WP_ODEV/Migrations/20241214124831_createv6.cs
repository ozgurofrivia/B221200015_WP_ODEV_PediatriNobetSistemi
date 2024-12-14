using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace B221200015_WP_ODEV.Migrations
{
    /// <inheritdoc />
    public partial class createv6 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Resim",
                table: "Hocalar",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Resim",
                table: "Asistanlar",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Resim",
                table: "Hocalar");

            migrationBuilder.AlterColumn<string>(
                name: "Resim",
                table: "Asistanlar",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);
        }
    }
}
