using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Seedium.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BlogImages",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FileName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FileExtension = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Url = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BlogImages", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BlogPosts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ShortDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FeaturedImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Slug = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PublishedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Author = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsVisible = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BlogPosts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Slug = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BlogPostCategory",
                columns: table => new
                {
                    BlogPostsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CategoriesId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BlogPostCategory", x => new { x.BlogPostsId, x.CategoriesId });
                    table.ForeignKey(
                        name: "FK_BlogPostCategory_BlogPosts_BlogPostsId",
                        column: x => x.BlogPostsId,
                        principalTable: "BlogPosts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BlogPostCategory_Categories_CategoriesId",
                        column: x => x.CategoriesId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Name", "Slug" },
                values: new object[,]
                {
                    { new Guid("0703b36b-fd1d-4ec5-99af-540278e3727b"), "NestJS", "nest-blogs" },
                    { new Guid("09f35d85-9e8e-4e44-ae91-c276743e2a08"), "JavaScript", "js-blogs" },
                    { new Guid("252a07cb-d1df-4029-83fe-a7c35a0d9c47"), "React", "react-blogs" },
                    { new Guid("29316fd9-ee3f-4473-8ca2-e3acf9ad8623"), "ASP.NET Core", "dotnet-blogs" },
                    { new Guid("2a94d6b9-8755-4c1f-beb9-44391e84e229"), "Artificial Intelligence", "ai-blogs" },
                    { new Guid("34680195-b466-4bec-b015-e6c17fe4f212"), "C#", "csharp-blogs" },
                    { new Guid("37384065-f8dc-4e11-9476-727e38c84773"), "TypeScript", "ts-blogs" },
                    { new Guid("4e23b927-647c-4114-81c3-63d39bbb890a"), "Django", "django-blogs" },
                    { new Guid("675384ca-b046-4de9-843f-3b81d2d89801"), "Python", "python-blogs" },
                    { new Guid("78c279ef-1705-4676-9d37-c31774b68e5b"), "Angular", "angular-blogs" },
                    { new Guid("7ee044bd-7959-45e9-b1f5-5becebec2270"), "HTTP", "http-blogs" },
                    { new Guid("a0d6745d-b68d-46f5-a03a-6db83a4e7750"), "Node", "node-blogs" },
                    { new Guid("a9efd598-49ac-4fe7-814c-aa7ff2947ee1"), "HTML", "html-blogs" },
                    { new Guid("ada50148-57e7-4702-8b03-7f8f0622b38e"), "CSS", "css-blogs" },
                    { new Guid("cc2bf2f6-41d9-4883-a42a-076a02bc1be8"), ".NET Framework", "dotnet-framework-blogs" },
                    { new Guid("e956e7f0-2518-41a5-b8e1-4ec91290f365"), "Java", "java-blogs" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_BlogPostCategory_CategoriesId",
                table: "BlogPostCategory",
                column: "CategoriesId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BlogImages");

            migrationBuilder.DropTable(
                name: "BlogPostCategory");

            migrationBuilder.DropTable(
                name: "BlogPosts");

            migrationBuilder.DropTable(
                name: "Categories");
        }
    }
}
