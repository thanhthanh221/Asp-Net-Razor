using Microsoft.EntityFrameworkCore.Migrations;

namespace Razor.Migrations
{
    public partial class Test2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "nvarchar",
                table: "Nguời Mua",
                newName: "Họ Tên");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Họ Tên",
                table: "Nguời Mua",
                newName: "nvarchar");
        }
    }
}
