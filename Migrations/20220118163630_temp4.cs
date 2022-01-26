using Microsoft.EntityFrameworkCore.Migrations;

namespace Razor.Migrations
{
    public partial class temp4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Anh_New",
                table: "Nhân Viên",
                newName: "Ảnh");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Ảnh",
                table: "Nhân Viên",
                newName: "Anh_New");
        }
    }
}
