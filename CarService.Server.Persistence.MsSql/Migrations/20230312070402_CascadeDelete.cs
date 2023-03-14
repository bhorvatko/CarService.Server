using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CarService.Server.Persistence.MsSql.Migrations
{
    /// <inheritdoc />
    public partial class CascadeDelete : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "WarrantId",
                table: "Notes",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Notes_WarrantId",
                table: "Notes",
                column: "WarrantId");

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

            migrationBuilder.DropIndex(
                name: "IX_Notes_WarrantId",
                table: "Notes");

            migrationBuilder.DropColumn(
                name: "WarrantId",
                table: "Notes");
        }
    }
}
