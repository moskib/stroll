using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Stroll.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "appointment_statuses",
                columns: table => new
                {
                    uid = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    status = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_appointment_statuses", x => x.uid);
                });

            migrationBuilder.CreateTable(
                name: "businesses",
                columns: table => new
                {
                    uid = table.Column<Guid>(nullable: false),
                    name = table.Column<string>(maxLength: 150, nullable: true),
                    address = table.Column<string>(maxLength: 300, nullable: true),
                    city = table.Column<string>(maxLength: 50, nullable: true),
                    phone = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_businesses", x => x.uid);
                });

            migrationBuilder.CreateTable(
                name: "user_types",
                columns: table => new
                {
                    uid = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    type = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user_types", x => x.uid);
                });

            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    uid = table.Column<Guid>(nullable: false),
                    email = table.Column<string>(nullable: false),
                    password = table.Column<string>(nullable: false),
                    type = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_users", x => x.uid);
                    table.ForeignKey(
                        name: "FK_users_user_types_type",
                        column: x => x.type,
                        principalTable: "user_types",
                        principalColumn: "uid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "business_users",
                columns: table => new
                {
                    uid = table.Column<Guid>(nullable: false),
                    business_id = table.Column<Guid>(nullable: false),
                    user_id = table.Column<Guid>(nullable: false),
                    first_name = table.Column<string>(maxLength: 50, nullable: true),
                    last_name = table.Column<string>(maxLength: 50, nullable: true),
                    phone = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_business_users", x => x.uid);
                    table.ForeignKey(
                        name: "FK_business_users_businesses_business_id",
                        column: x => x.business_id,
                        principalTable: "businesses",
                        principalColumn: "uid",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_business_users_users_user_id",
                        column: x => x.user_id,
                        principalTable: "users",
                        principalColumn: "uid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "client_users",
                columns: table => new
                {
                    user_id = table.Column<Guid>(nullable: false),
                    first_name = table.Column<string>(maxLength: 50, nullable: true),
                    last_name = table.Column<string>(maxLength: 50, nullable: true),
                    phone = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_client_users", x => x.user_id);
                    table.ForeignKey(
                        name: "FK_client_users_users_user_id",
                        column: x => x.user_id,
                        principalTable: "users",
                        principalColumn: "uid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "schedules",
                columns: table => new
                {
                    uid = table.Column<Guid>(nullable: false),
                    business_user_id = table.Column<Guid>(nullable: false),
                    client_user_id = table.Column<Guid>(nullable: false),
                    start_time = table.Column<DateTime>(nullable: false),
                    end_time = table.Column<DateTime>(nullable: false),
                    status = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_schedules", x => x.uid);
                    table.ForeignKey(
                        name: "FK_schedules_business_users_business_user_id",
                        column: x => x.business_user_id,
                        principalTable: "business_users",
                        principalColumn: "uid",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_schedules_client_users_client_user_id",
                        column: x => x.client_user_id,
                        principalTable: "client_users",
                        principalColumn: "user_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_schedules_appointment_statuses_status",
                        column: x => x.status,
                        principalTable: "appointment_statuses",
                        principalColumn: "uid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_business_users_business_id",
                table: "business_users",
                column: "business_id");

            migrationBuilder.CreateIndex(
                name: "IX_business_users_user_id",
                table: "business_users",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_schedules_business_user_id",
                table: "schedules",
                column: "business_user_id");

            migrationBuilder.CreateIndex(
                name: "IX_schedules_client_user_id",
                table: "schedules",
                column: "client_user_id");

            migrationBuilder.CreateIndex(
                name: "IX_schedules_status",
                table: "schedules",
                column: "status");

            migrationBuilder.CreateIndex(
                name: "IX_users_type",
                table: "users",
                column: "type");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "schedules");

            migrationBuilder.DropTable(
                name: "business_users");

            migrationBuilder.DropTable(
                name: "client_users");

            migrationBuilder.DropTable(
                name: "appointment_statuses");

            migrationBuilder.DropTable(
                name: "businesses");

            migrationBuilder.DropTable(
                name: "users");

            migrationBuilder.DropTable(
                name: "user_types");
        }
    }
}
