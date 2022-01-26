using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Razor.Migrations
{
    public partial class temp3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Ảnh",
                table: "Nhân Viên");

            migrationBuilder.AddColumn<byte[]>(
                name: "Anh_New",
                table: "Nhân Viên",
                type: "varbinary(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Anh_New",
                table: "Nhân Viên");

            migrationBuilder.AddColumn<string>(
                name: "Ảnh",
                table: "Nhân Viên",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
