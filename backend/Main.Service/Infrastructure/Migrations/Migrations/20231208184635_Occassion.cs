using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Entities.Migrations
{
    /// <inheritdoc />
    public partial class Occassion : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Occasions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    IssuerId = table.Column<string>(type: "text", nullable: false),
                    CreationDateUtc = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    CloseDateUtc = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    OccasionType = table.Column<int>(type: "integer", nullable: false),
                    Topic = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Occasions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Occasions_AspNetUsers_IssuerId",
                        column: x => x.IssuerId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OccasionTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Description = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OccasionTypes", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "OccasionTypes",
                columns: new[] { "Id", "Description" },
                values: new object[,]
                {
                    { 1, "ДТП" },
                    { 2, "Поломка ТС" },
                    { 2048, "Прочее" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Occasions_IssuerId",
                table: "Occasions",
                column: "IssuerId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Occasions");

            migrationBuilder.DropTable(
                name: "OccasionTypes");
        }
    }
}
