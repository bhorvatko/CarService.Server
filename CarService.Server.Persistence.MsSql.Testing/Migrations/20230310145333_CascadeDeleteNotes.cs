using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CarService.Server.Persistence.MsSql.Testing.Migrations
{
    /// <inheritdoc />
    public partial class CascadeDeleteNotes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Notes_Warrants_WarrantId",
                table: "Notes");

            migrationBuilder.AddForeignKey(
                name: "FK_Notes_Warrants_WarrantId",
                table: "Notes",
                column: "WarrantId",
                principalTable: "Warrants",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Notes_Warrants_WarrantId",
                table: "Notes");

            migrationBuilder.AddForeignKey(
                name: "FK_Notes_Warrants_WarrantId",
                table: "Notes",
                column: "WarrantId",
                principalTable: "Warrants",
                principalColumn: "Id");
        }
    }
}
