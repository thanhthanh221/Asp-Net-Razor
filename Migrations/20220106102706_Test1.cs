using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Razor.Migrations
{
    public partial class Test1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "blogs",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Content = table.Column<string>(type: "ntext", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_blogs", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Kho",
                columns: table => new
                {
                    MãKho = table.Column<int>(name: "Mã Kho", type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TênKho = table.Column<int>(name: "Tên Kho", type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Kho", x => x.MãKho);
                });

            migrationBuilder.CreateTable(
                name: "Nguời Mua",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nvarchar = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    CMND = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Nguời Mua", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Nhân Viên",
                columns: table => new
                {
                    MãNhânViên = table.Column<int>(name: "Mã Nhân Viên", type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HọTên = table.Column<string>(name: "Họ Tên", type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CMND = table.Column<int>(type: "int", nullable: false),
                    NgaySinh = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SốĐiệnThoại = table.Column<int>(name: "Số Điện Thoại", type: "int", nullable: false),
                    NhanVienMaNV = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Nhân Viên", x => x.MãNhânViên);
                    table.ForeignKey(
                        name: "FK_Nhân Viên_Nhân Viên_NhanVienMaNV",
                        column: x => x.NhanVienMaNV,
                        principalTable: "Nhân Viên",
                        principalColumn: "Mã Nhân Viên",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Sản Phẩm",
                columns: table => new
                {
                    MãSảnPhẩm = table.Column<int>(name: "Mã Sản Phẩm", type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TênSảnPhẩm = table.Column<string>(name: "Tên Sản Phẩm", type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Giá = table.Column<int>(type: "int", nullable: false),
                    SốLượng = table.Column<int>(name: "Số Lượng", type: "int", nullable: false),
                    MãKho = table.Column<int>(name: "Mã Kho", type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sản Phẩm", x => x.MãSảnPhẩm);
                    table.ForeignKey(
                        name: "FK_Sản Phẩm_Kho_Mã Kho",
                        column: x => x.MãKho,
                        principalTable: "Kho",
                        principalColumn: "Mã Kho",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Hóa Đơn",
                columns: table => new
                {
                    MãHóaĐơn = table.Column<int>(name: "Mã Hóa Đơn", type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MãNhânViênBán = table.Column<int>(name: "Mã Nhân Viên Bán", type: "int", nullable: false),
                    NgườiMua = table.Column<int>(name: "Người Mua", type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Hóa Đơn", x => x.MãHóaĐơn);
                    table.ForeignKey(
                        name: "FK_Hóa Đơn_Nguời Mua_Người Mua",
                        column: x => x.NgườiMua,
                        principalTable: "Nguời Mua",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Hóa Đơn_Nhân Viên_Mã Nhân Viên Bán",
                        column: x => x.MãNhânViênBán,
                        principalTable: "Nhân Viên",
                        principalColumn: "Mã Nhân Viên",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Hóa Đơn_Mã Nhân Viên Bán",
                table: "Hóa Đơn",
                column: "Mã Nhân Viên Bán");

            migrationBuilder.CreateIndex(
                name: "IX_Hóa Đơn_Người Mua",
                table: "Hóa Đơn",
                column: "Người Mua");

            migrationBuilder.CreateIndex(
                name: "IX_Nhân Viên_NhanVienMaNV",
                table: "Nhân Viên",
                column: "NhanVienMaNV");

            migrationBuilder.CreateIndex(
                name: "IX_Sản Phẩm_Mã Kho",
                table: "Sản Phẩm",
                column: "Mã Kho");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "blogs");

            migrationBuilder.DropTable(
                name: "Hóa Đơn");

            migrationBuilder.DropTable(
                name: "Sản Phẩm");

            migrationBuilder.DropTable(
                name: "Nguời Mua");

            migrationBuilder.DropTable(
                name: "Nhân Viên");

            migrationBuilder.DropTable(
                name: "Kho");
        }
    }
}
