using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SA52T03_SWStore.Data.Migrations
{
    public partial class PutBackOrderDateAndRemoveOrderState : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OrderState",
                table: "Order");

            migrationBuilder.AddColumn<DateTime>(
                name: "OrderDate",
                table: "Order",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OrderDate",
                table: "Order");

            migrationBuilder.AddColumn<bool>(
                name: "OrderState",
                table: "Order",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
