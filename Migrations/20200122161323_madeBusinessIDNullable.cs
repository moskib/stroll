using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Stroll.Migrations
{
    public partial class madeBusinessIDNullable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_business_users_businesses_business_id",
                table: "business_users");

            migrationBuilder.AlterColumn<Guid>(
                name: "business_id",
                table: "business_users",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AddForeignKey(
                name: "FK_business_users_businesses_business_id",
                table: "business_users",
                column: "business_id",
                principalTable: "businesses",
                principalColumn: "uid",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_business_users_businesses_business_id",
                table: "business_users");

            migrationBuilder.AlterColumn<Guid>(
                name: "business_id",
                table: "business_users",
                type: "uuid",
                nullable: false,
                oldClrType: typeof(Guid),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_business_users_businesses_business_id",
                table: "business_users",
                column: "business_id",
                principalTable: "businesses",
                principalColumn: "uid",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
