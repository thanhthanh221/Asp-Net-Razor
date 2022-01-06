using Microsoft.EntityFrameworkCore.Migrations;

namespace Razor.Migrations
{
    public partial class Test3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Nhân Viên_Nhân Viên_NhanVienMaNV",
                table: "Nhân Viên");

            migrationBuilder.DropIndex(
                name: "IX_Nhân Viên_NhanVienMaNV",
                table: "Nhân Viên");

            migrationBuilder.DropColumn(
                name: "NhanVienMaNV",
                table: "Nhân Viên");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "NhanVienMaNV",
                table: "Nhân Viên",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Nhân Viên_NhanVienMaNV",
                table: "Nhân Viên",
                column: "NhanVienMaNV");

            migrationBuilder.AddForeignKey(
                name: "FK_Nhân Viên_Nhân Viên_NhanVienMaNV",
                table: "Nhân Viên",
                column: "NhanVienMaNV",
                principalTable: "Nhân Viên",
                principalColumn: "Mã Nhân Viên",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
