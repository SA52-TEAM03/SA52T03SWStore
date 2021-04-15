using Microsoft.EntityFrameworkCore.Migrations;

namespace SA52T03_SWStore.Data.Migrations
{
    public partial class AcCode : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ACode_Category_OrderDetailId",
                table: "ACode");

            migrationBuilder.DropForeignKey(
                name: "FK_ACode_OrderDetail_OrderDetailId1",
                table: "ACode");

            migrationBuilder.DropIndex(
                name: "IX_ACode_OrderDetailId1",
                table: "ACode");

            migrationBuilder.DropColumn(
                name: "OrderDetailId1", 
                table: "ACode");

            migrationBuilder.AddForeignKey(
                name: "FK_ACode_OrderDetail_OrderDetailId",
                table: "ACode",
                column: "OrderDetailId",
                principalTable: "OrderDetail",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ACode_OrderDetail_OrderDetailId",
                table: "ACode");

            migrationBuilder.AddColumn<int>(
                name: "OrderDetailId1",
                table: "ACode",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ACode_OrderDetailId1",
                table: "ACode",
                column: "OrderDetailId1");

            migrationBuilder.AddForeignKey(
                name: "FK_ACode_Category_OrderDetailId",
                table: "ACode",
                column: "OrderDetailId",
                principalTable: "Category",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ACode_OrderDetail_OrderDetailId1",
                table: "ACode",
                column: "OrderDetailId1",
                principalTable: "OrderDetail",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
