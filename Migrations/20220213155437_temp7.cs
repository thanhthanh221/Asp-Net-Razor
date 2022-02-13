using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Razor.Migrations
{
    public partial class temp7 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Mô Tả",
                table: "Sản Phẩm",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "Thông Tin Mới",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TiêuĐề = table.Column<string>(name: "Tiêu Đề", type: "nvarchar(max)", nullable: false),
                    NộiDung = table.Column<string>(name: "Nội Dung", type: "nvarchar(max)", nullable: false),
                    NgàyTạo = table.Column<DateTime>(name: "Ngày Tạo", type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Thông Tin Mới", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Thuộc Tính",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Tên = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Thuộc Tính", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Giá trị thuộc tính",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MãThuộctính = table.Column<int>(name: "Mã Thuộc tính", type: "int", nullable: false),
                    ThuộcTính = table.Column<string>(name: "Thuộc Tính", type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Giá trị thuộc tính", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Giá trị thuộc tính_Thuộc Tính_Mã Thuộc tính",
                        column: x => x.MãThuộctính,
                        principalTable: "Thuộc Tính",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Thuộc tính sản phẩm",
                columns: table => new
                {
                    ID_Table = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MãSảnPhẩm = table.Column<int>(name: "Mã Sản Phẩm", type: "int", nullable: false),
                    Mãgiátrịthuộctính = table.Column<int>(name: "Mã giá trị thuộc tính", type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Thuộc tính sản phẩm", x => x.ID_Table);
                    table.ForeignKey(
                        name: "FK_Thuộc tính sản phẩm_Giá trị thuộc tính_Mã giá trị thuộc tính",
                        column: x => x.Mãgiátrịthuộctính,
                        principalTable: "Giá trị thuộc tính",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Thuộc tính sản phẩm_Sản Phẩm_Mã Sản Phẩm",
                        column: x => x.MãSảnPhẩm,
                        principalTable: "Sản Phẩm",
                        principalColumn: "Mã Sản Phẩm",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Giá trị thuộc tính_Mã Thuộc tính",
                table: "Giá trị thuộc tính",
                column: "Mã Thuộc tính");

            migrationBuilder.CreateIndex(
                name: "IX_Thuộc tính sản phẩm_Mã giá trị thuộc tính",
                table: "Thuộc tính sản phẩm",
                column: "Mã giá trị thuộc tính");

            migrationBuilder.CreateIndex(
                name: "IX_Thuộc tính sản phẩm_Mã Sản Phẩm",
                table: "Thuộc tính sản phẩm",
                column: "Mã Sản Phẩm");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Thông Tin Mới");

            migrationBuilder.DropTable(
                name: "Thuộc tính sản phẩm");

            migrationBuilder.DropTable(
                name: "Giá trị thuộc tính");

            migrationBuilder.DropTable(
                name: "Thuộc Tính");

            migrationBuilder.DropColumn(
                name: "Mô Tả",
                table: "Sản Phẩm");
        }
    }
}
