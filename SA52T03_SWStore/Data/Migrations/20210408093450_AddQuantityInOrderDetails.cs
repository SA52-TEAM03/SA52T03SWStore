using Microsoft.EntityFrameworkCore.Migrations;

namespace SA52T03_SWStore.Data.Migrations
{
    public partial class AddQuantityInOrderDetails : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Quantity",
                table: "OrderDetail",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "OrderDetail");
        }
    }
}
