using Microsoft.EntityFrameworkCore.Migrations;

namespace Stroll.Migrations
{
    public partial class InitialSeed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "appointment_statuses",
                columns: new[] { "uid", "status" },
                values: new object[,]
                {
                    { 1, "open" },
                    { 2, "taken" },
                    { 3, "cancelled" }
                });

            migrationBuilder.InsertData(
                table: "user_types",
                columns: new[] { "uid", "type" },
                values: new object[,]
                {
                    { 1, "client" },
                    { 2, "business_employee" },
                    { 3, "business_admin" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "appointment_statuses",
                keyColumn: "uid",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "appointment_statuses",
                keyColumn: "uid",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "appointment_statuses",
                keyColumn: "uid",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "user_types",
                keyColumn: "uid",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "user_types",
                keyColumn: "uid",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "user_types",
                keyColumn: "uid",
                keyValue: 3);
        }
    }
}
