using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BalanceService.Migrations
{
    /// <inheritdoc />
    public partial class User_AddedBalanceId_BalanceAndBalanceIdIsRequered : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "BalanceId",
                table: "Users",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BalanceId",
                table: "Users");
        }
    }
}
