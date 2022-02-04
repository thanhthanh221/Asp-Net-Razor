using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Razor.Migrations
{
    public partial class temp5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Hóa Đơn_Nguời Mua_Người Mua",
                table: "Hóa Đơn");

            migrationBuilder.DropForeignKey(
                name: "FK_Hóa Đơn_Nhân Viên_Mã Nhân Viên Bán",
                table: "Hóa Đơn");

            migrationBuilder.DropTable(
                name: "Nguời Mua");

            migrationBuilder.DropTable(
                name: "Nhân Viên");

            migrationBuilder.DropIndex(
                name: "IX_Hóa Đơn_Người Mua",
                table: "Hóa Đơn");

            migrationBuilder.DropColumn(
                name: "Anh",
                table: "Sản Phẩm");

            migrationBuilder.DropColumn(
                name: "Tên Kho",
                table: "Kho");

            migrationBuilder.DropColumn(
                name: "Lương",
                table: "AspNetUsers");

            migrationBuilder.RenameColumn(
                name: "Người Mua",
                table: "Hóa Đơn",
                newName: "Tiền");

            migrationBuilder.RenameColumn(
                name: "Mã Nhân Viên Bán",
                table: "Hóa Đơn",
                newName: "ID_Shiper");

            migrationBuilder.RenameIndex(
                name: "IX_Hóa Đơn_Mã Nhân Viên Bán",
                table: "Hóa Đơn",
                newName: "IX_Hóa Đơn_ID_Shiper");

            migrationBuilder.AddColumn<byte[]>(
                name: "Anh1",
                table: "Sản Phẩm",
                type: "varbinary(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Đã Bán",
                table: "Sản Phẩm",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Hãng Giao Hàng",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TênHãng = table.Column<string>(name: "Tên Hãng", type: "nvarchar(max)", nullable: false),
                    giá = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Hãng Giao Hàng", x => x.ID);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Hóa Đơn_Hãng Giao Hàng_ID_Shiper",
                table: "Hóa Đơn",
                column: "ID_Shiper",
                principalTable: "Hãng Giao Hàng",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Hóa Đơn_Hãng Giao Hàng_ID_Shiper",
                table: "Hóa Đơn");

            migrationBuilder.DropTable(
                name: "Hãng Giao Hàng");

            migrationBuilder.DropColumn(
                name: "Anh1",
                table: "Sản Phẩm");

            migrationBuilder.DropColumn(
                name: "Đã Bán",
                table: "Sản Phẩm");

            migrationBuilder.RenameColumn(
                name: "Tiền",
                table: "Hóa Đơn",
                newName: "Người Mua");

            migrationBuilder.RenameColumn(
                name: "ID_Shiper",
                table: "Hóa Đơn",
                newName: "Mã Nhân Viên Bán");

            migrationBuilder.RenameIndex(
                name: "IX_Hóa Đơn_ID_Shiper",
                table: "Hóa Đơn",
                newName: "IX_Hóa Đơn_Mã Nhân Viên Bán");

            migrationBuilder.AddColumn<string>(
                name: "Anh",
                table: "Sản Phẩm",
                type: "ntext",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Tên Kho",
                table: "Kho",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Lương",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Nguời Mua",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CMND = table.Column<int>(type: "int", nullable: false),
                    HọTên = table.Column<string>(name: "Họ Tên", type: "nvarchar(30)", maxLength: 30, nullable: true),
                    SốĐiệnThoại = table.Column<string>(name: "Số Điện Thoại", type: "nvarchar(10)", maxLength: 10, nullable: false),
                    email = table.Column<string>(type: "nvarchar(max)", nullable: true)
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
                    Ảnh = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    CMND = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HọTên = table.Column<string>(name: "Họ Tên", type: "nvarchar(50)", maxLength: 50, nullable: false),
                    NgaySinh = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SốĐiệnThoại = table.Column<string>(name: "Số Điện Thoại", type: "nvarchar(10)", maxLength: 10, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Nhân Viên", x => x.MãNhânViên);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Hóa Đơn_Người Mua",
                table: "Hóa Đơn",
                column: "Người Mua");

            migrationBuilder.AddForeignKey(
                name: "FK_Hóa Đơn_Nguời Mua_Người Mua",
                table: "Hóa Đơn",
                column: "Người Mua",
                principalTable: "Nguời Mua",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Hóa Đơn_Nhân Viên_Mã Nhân Viên Bán",
                table: "Hóa Đơn",
                column: "Mã Nhân Viên Bán",
                principalTable: "Nhân Viên",
                principalColumn: "Mã Nhân Viên",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
