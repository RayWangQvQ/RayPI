using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace RayPI.Repository.EFRepository.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "ray");

            migrationBuilder.CreateTable(
                name: "ArticleEntity",
                schema: "ray",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false),
                    CreateName = table.Column<string>(maxLength: 128, nullable: true),
                    CreateId = table.Column<long>(nullable: true),
                    CreateTime = table.Column<DateTime>(nullable: true),
                    UpdateName = table.Column<string>(maxLength: 128, nullable: true),
                    UpdateId = table.Column<long>(nullable: true),
                    UpdateTime = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DeleteTime = table.Column<DateTime>(nullable: true),
                    Title = table.Column<string>(nullable: false),
                    SubTitle = table.Column<string>(nullable: true),
                    Content = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ArticleEntity", x => x.Id);
                });

            migrationBuilder.InsertData(
                schema: "ray",
                table: "ArticleEntity",
                columns: new[] { "Id", "Content", "CreateId", "CreateName", "CreateTime", "DeleteTime", "IsDeleted", "SubTitle", "Title", "UpdateId", "UpdateName", "UpdateTime" },
                values: new object[] { 1L, "这是内容", null, "", null, null, false, "来自DbContext的OnModelCreating", "这是一条初始化的数据", null, "", null });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ArticleEntity",
                schema: "ray");
        }
    }
}
