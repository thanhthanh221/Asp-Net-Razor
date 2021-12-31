using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Bogus;
using Razor.model;    

namespace Razor.Migrations
{
    public partial class initdb : Migration
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
                Randomizer.Seed = new Random(8675309);
                var fakerAc = new Faker<Blog>();

                fakerAc.RuleFor(p => p.Title ,fakerAc =>fakerAc.Lorem.Sentence(5,5));
                fakerAc.RuleFor(p => p.Created , fakerAc => fakerAc.Date.Between(new DateTime(2021,1,1),new DateTime(2021,11,30)));
                fakerAc.RuleFor(p => p.Content ,fakerAc =>fakerAc.Lorem.Paragraphs(1,4));

                for (int i = 0; i < 150; i++){
                    Blog blog = fakerAc.Generate();
                    migrationBuilder.InsertData(
                        table :"blogs",
                        columns: new[] {"Title","Created","Content"},
                        values :new object[] {
                            blog.Title,
                            blog.Created,
                            blog.Content
                        }

                    );                    
                }

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "blogs");
        }
    }
}
