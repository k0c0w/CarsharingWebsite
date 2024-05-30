using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Entities.Migrations
{
    /// <inheritdoc />
    public partial class Occasion_FKFix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Occasions_IssuerId",
                table: "Occasions");

            migrationBuilder.CreateIndex(
                name: "IX_Occasions_IssuerId",
                table: "Occasions",
                column: "IssuerId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Occasions_IssuerId",
                table: "Occasions");

            migrationBuilder.CreateIndex(
                name: "IX_Occasions_IssuerId",
                table: "Occasions",
                column: "IssuerId",
                unique: true);
        }
    }
}
