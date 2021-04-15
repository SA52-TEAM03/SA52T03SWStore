using Microsoft.EntityFrameworkCore.Migrations;

namespace SA52T03_SWStore.Data.Migrations
{
    public partial class ACode : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ACode",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderDetailId = table.Column<int>(nullable: false),
                    ACChain = table.Column<string>(nullable: true),
                    OrderDetailId1 = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ACode", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ACode_Category_OrderDetailId",
                        column: x => x.OrderDetailId,
                        principalTable: "Category",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ACode_OrderDetail_OrderDetailId1",
                        column: x => x.OrderDetailId1,
                        principalTable: "OrderDetail",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ACode_OrderDetailId",
                table: "ACode",
                column: "OrderDetailId");

            migrationBuilder.CreateIndex(
                name: "IX_ACode_OrderDetailId1",
                table: "ACode",
                column: "OrderDetailId1");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ACode");
        }
    }
}
