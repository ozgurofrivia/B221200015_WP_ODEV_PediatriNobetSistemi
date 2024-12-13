using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace B221200015_WP_ODEV.Migrations
{
    /// <inheritdoc />
    public partial class createv3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HocaMüsaitlikler_Hocalar_HocaId",
                table: "HocaMüsaitlikler");

            migrationBuilder.DropPrimaryKey(
                name: "PK_HocaMüsaitlikler",
                table: "HocaMüsaitlikler");

            migrationBuilder.RenameTable(
                name: "HocaMüsaitlikler",
                newName: "HocaMusaitlikler");

            migrationBuilder.RenameIndex(
                name: "IX_HocaMüsaitlikler_HocaId",
                table: "HocaMusaitlikler",
                newName: "IX_HocaMusaitlikler_HocaId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_HocaMusaitlikler",
                table: "HocaMusaitlikler",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_HocaMusaitlikler_Hocalar_HocaId",
                table: "HocaMusaitlikler",
                column: "HocaId",
                principalTable: "Hocalar",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HocaMusaitlikler_Hocalar_HocaId",
                table: "HocaMusaitlikler");

            migrationBuilder.DropPrimaryKey(
                name: "PK_HocaMusaitlikler",
                table: "HocaMusaitlikler");

            migrationBuilder.RenameTable(
                name: "HocaMusaitlikler",
                newName: "HocaMüsaitlikler");

            migrationBuilder.RenameIndex(
                name: "IX_HocaMusaitlikler_HocaId",
                table: "HocaMüsaitlikler",
                newName: "IX_HocaMüsaitlikler_HocaId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_HocaMüsaitlikler",
                table: "HocaMüsaitlikler",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_HocaMüsaitlikler_Hocalar_HocaId",
                table: "HocaMüsaitlikler",
                column: "HocaId",
                principalTable: "Hocalar",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
