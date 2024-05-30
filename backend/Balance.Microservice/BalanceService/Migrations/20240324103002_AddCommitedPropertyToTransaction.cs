using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BalanceService.Migrations
{
    /// <inheritdoc />
    public partial class AddCommitedPropertyToTransaction : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Commited",
                table: "Transactions",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Commited",
                table: "Transactions");
        }
    }
}
