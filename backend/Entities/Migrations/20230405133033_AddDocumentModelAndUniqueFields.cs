using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Entities.Migrations
{
    /// <inheritdoc />
    public partial class AddDocumentModelAndUniqueFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Document",
                table: "Document");

            migrationBuilder.RenameTable(
                name: "Document",
                newName: "WebsiteDocuments");

            migrationBuilder.RenameIndex(
                name: "IX_Document_Name",
                table: "WebsiteDocuments",
                newName: "IX_WebsiteDocuments_Name");

            migrationBuilder.AddPrimaryKey(
                name: "PK_WebsiteDocuments",
                table: "WebsiteDocuments",
                column: "FileName");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_WebsiteDocuments",
                table: "WebsiteDocuments");

            migrationBuilder.RenameTable(
                name: "WebsiteDocuments",
                newName: "Document");

            migrationBuilder.RenameIndex(
                name: "IX_WebsiteDocuments_Name",
                table: "Document",
                newName: "IX_Document_Name");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Document",
                table: "Document",
                column: "FileName");
        }
    }
}
