using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Entities.Migrations
{
    /// <inheritdoc />
    public partial class AddTariffRestrictions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OccasionMessages");

            migrationBuilder.DropCheckConstraint(
                name: "CK_Tariff_Price",
                table: "Tariffs");

            migrationBuilder.RenameColumn(
                name: "Price",
                table: "Tariffs",
                newName: "PricePerMinute");

            migrationBuilder.AddColumn<long>(
                name: "MaxAllowedMinutes",
                table: "Tariffs",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "MinAllowedMinutes",
                table: "Tariffs",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddCheckConstraint(
                name: "CK_Tariff_PricePerMinute",
                table: "Tariffs",
                sql: "\"PricePerMinute\" > 0");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropCheckConstraint(
                name: "CK_Tariff_PricePerMinute",
                table: "Tariffs");

            migrationBuilder.DropColumn(
                name: "MaxAllowedMinutes",
                table: "Tariffs");

            migrationBuilder.DropColumn(
                name: "MinAllowedMinutes",
                table: "Tariffs");

            migrationBuilder.RenameColumn(
                name: "PricePerMinute",
                table: "Tariffs",
                newName: "Price");

            migrationBuilder.CreateTable(
                name: "OccasionMessages",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Attachment = table.Column<Guid>(type: "uuid", nullable: true),
                    AuthorId = table.Column<string>(type: "text", nullable: false),
                    IsFromManager = table.Column<bool>(type: "boolean", nullable: false),
                    OccasionId = table.Column<Guid>(type: "uuid", nullable: false),
                    Text = table.Column<string>(type: "text", nullable: false),
                    Time = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    TopicAuthorId = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OccasionMessages", x => x.Id);
                });

            migrationBuilder.AddCheckConstraint(
                name: "CK_Tariff_Price",
                table: "Tariffs",
                sql: "\"Price\" > 0");
        }
    }
}
