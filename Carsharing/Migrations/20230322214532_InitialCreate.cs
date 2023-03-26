using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Carsharing.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "role",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false),
                    name = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("role_pkey", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "tarrif",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false),
                    description = table.Column<string>(type: "text", nullable: false),
                    price = table.Column<int>(type: "integer", nullable: false),
                    period = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("tarrif_pkey", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "client",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false),
                    email = table.Column<string>(type: "text", nullable: false),
                    password = table.Column<string>(type: "text", nullable: false),
                    role_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("client_pkey", x => x.id);
                    table.ForeignKey(
                        name: "client_role_id_fkey",
                        column: x => x.role_id,
                        principalTable: "role",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "car_model",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false),
                    tarrif_id = table.Column<int>(type: "integer", nullable: true),
                    name = table.Column<string>(type: "text", nullable: false),
                    description = table.Column<string>(type: "text", nullable: false),
                    source_img = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("car_model_pkey", x => x.id);
                    table.ForeignKey(
                        name: "car_model_tarrif_id_fkey",
                        column: x => x.tarrif_id,
                        principalTable: "tarrif",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "client_info",
                columns: table => new
                {
                    passport_type = table.Column<string>(type: "text", nullable: false),
                    passport_num = table.Column<int>(type: "integer", nullable: false),
                    rider_num = table.Column<int>(type: "integer", nullable: false),
                    telephone_num = table.Column<int>(type: "integer", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false),
                    surname = table.Column<string>(type: "text", nullable: false),
                    age = table.Column<int>(type: "integer", nullable: false),
                    card_number = table.Column<int>(type: "integer", nullable: true),
                    balance = table.Column<int>(type: "integer", nullable: true, defaultValueSql: "0"),
                    client_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("client_info_pkey", x => new { x.passport_type, x.passport_num, x.rider_num, x.telephone_num });
                    table.ForeignKey(
                        name: "client_info_client_id_fkey",
                        column: x => x.client_id,
                        principalTable: "client",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "car_park",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false),
                    car_model_id = table.Column<int>(type: "integer", nullable: true),
                    is_opened = table.Column<bool>(type: "boolean", nullable: true, defaultValueSql: "false"),
                    goverment_number = table.Column<int>(type: "integer", nullable: false),
                    is_taken = table.Column<bool>(type: "boolean", nullable: true, defaultValueSql: "false")
                },
                constraints: table =>
                {
                    table.PrimaryKey("car_park_pkey", x => x.id);
                    table.ForeignKey(
                        name: "car_park_car_model_id_fkey",
                        column: x => x.car_model_id,
                        principalTable: "car_model",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "subscription",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false),
                    end_date = table.Column<DateOnly>(type: "date", nullable: true),
                    start_date = table.Column<DateOnly>(type: "date", nullable: true),
                    is_active = table.Column<bool>(type: "boolean", nullable: true, defaultValueSql: "false"),
                    car_id = table.Column<int>(type: "integer", nullable: true),
                    client_id = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("subscription_pkey", x => x.id);
                    table.ForeignKey(
                        name: "subscription_car_id_fkey",
                        column: x => x.car_id,
                        principalTable: "car_park",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "subscription_client_id_fkey",
                        column: x => x.client_id,
                        principalTable: "client",
                        principalColumn: "id");
                });

            migrationBuilder.CreateIndex(
                name: "car_model_name_key",
                table: "car_model",
                column: "name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "car_model_source_img_key",
                table: "car_model",
                column: "source_img",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_car_model_tarrif_id",
                table: "car_model",
                column: "tarrif_id");

            migrationBuilder.CreateIndex(
                name: "car_park_goverment_number_key",
                table: "car_park",
                column: "goverment_number",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_car_park_car_model_id",
                table: "car_park",
                column: "car_model_id");

            migrationBuilder.CreateIndex(
                name: "Role_id_unique",
                table: "client",
                column: "role_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "client_info_card_number_key",
                table: "client_info",
                column: "card_number",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "client_info_client_id_key",
                table: "client_info",
                column: "client_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_subscription_car_id",
                table: "subscription",
                column: "car_id");

            migrationBuilder.CreateIndex(
                name: "IX_subscription_client_id",
                table: "subscription",
                column: "client_id");

            migrationBuilder.CreateIndex(
                name: "tarrif_name_key",
                table: "tarrif",
                column: "name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "tarrif_period_key",
                table: "tarrif",
                column: "period",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "client_info");

            migrationBuilder.DropTable(
                name: "subscription");

            migrationBuilder.DropTable(
                name: "car_park");

            migrationBuilder.DropTable(
                name: "client");

            migrationBuilder.DropTable(
                name: "car_model");

            migrationBuilder.DropTable(
                name: "role");

            migrationBuilder.DropTable(
                name: "tarrif");
        }
    }
}
