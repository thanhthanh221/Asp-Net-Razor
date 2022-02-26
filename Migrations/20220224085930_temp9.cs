using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Razor.Migrations
{
    public partial class temp9 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Mã Danh Mục",
                table: "Sản Phẩm",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Danh Mục",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TênDanhMục = table.Column<string>(name: "Tên Danh Mục", type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Danh Mục", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Sản Phẩm Bán theo hóa đơn",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Product_ID = table.Column<int>(type: "int", nullable: false),
                    MãHóaĐơn = table.Column<int>(name: "Mã Hóa Đơn", type: "int", nullable: false),
                    SốLượng = table.Column<int>(name: "Số Lượng", type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sản Phẩm Bán theo hóa đơn", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Sản Phẩm Bán theo hóa đơn_Hóa Đơn_Mã Hóa Đơn",
                        column: x => x.MãHóaĐơn,
                        principalTable: "Hóa Đơn",
                        principalColumn: "Mã Hóa Đơn",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Sản Phẩm Bán theo hóa đơn_Sản Phẩm_Product_ID",
                        column: x => x.Product_ID,
                        principalTable: "Sản Phẩm",
                        principalColumn: "Mã Sản Phẩm",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Sản Phẩm_Mã Danh Mục",
                table: "Sản Phẩm",
                column: "Mã Danh Mục");

            migrationBuilder.CreateIndex(
                name: "IX_Sản Phẩm Bán theo hóa đơn_Mã Hóa Đơn",
                table: "Sản Phẩm Bán theo hóa đơn",
                column: "Mã Hóa Đơn");

            migrationBuilder.CreateIndex(
                name: "IX_Sản Phẩm Bán theo hóa đơn_Product_ID",
                table: "Sản Phẩm Bán theo hóa đơn",
                column: "Product_ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Sản Phẩm_Danh Mục_Mã Danh Mục",
                table: "Sản Phẩm",
                column: "Mã Danh Mục",
                principalTable: "Danh Mục",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Sản Phẩm_Danh Mục_Mã Danh Mục",
                table: "Sản Phẩm");

            migrationBuilder.DropTable(
                name: "Danh Mục");

            migrationBuilder.DropTable(
                name: "Sản Phẩm Bán theo hóa đơn");

            migrationBuilder.DropIndex(
                name: "IX_Sản Phẩm_Mã Danh Mục",
                table: "Sản Phẩm");

            migrationBuilder.DropColumn(
                name: "Mã Danh Mục",
                table: "Sản Phẩm");
        }
    }
}
