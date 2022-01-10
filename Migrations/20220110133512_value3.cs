using Microsoft.EntityFrameworkCore.Migrations;

namespace Razor.Migrations
{
    public partial class value3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Ảnh",
                table: "Nhân Viên",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Ảnh",
                table: "Nhân Viên");
        }
    }
}
