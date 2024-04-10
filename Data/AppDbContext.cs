using Microsoft.EntityFrameworkCore;
using Seedium.Models.Domain;

namespace Seedium.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options) { }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<BlogPost>().HasMany(b => b.Categories).WithMany(c => c.BlogPosts);

        List<Category> categories =
        [
            new Category
            {
                Id = Guid.Parse("29316fd9-ee3f-4473-8ca2-e3acf9ad8623"),
                Name = "ASP.NET Core",
                Slug = "dotnet-blogs"
            },
            new Category
            {
                Id = Guid.Parse("34680195-b466-4bec-b015-e6c17fe4f212"),
                Name = "C#",
                Slug = "csharp-blogs"
            },
            new Category
            {
                Id = Guid.Parse("78c279ef-1705-4676-9d37-c31774b68e5b"),
                Name = "Angular",
                Slug = "angular-blogs"
            },
            new Category
            {
                Id = Guid.Parse("a9efd598-49ac-4fe7-814c-aa7ff2947ee1"),
                Name = "HTML",
                Slug = "html-blogs"
            },
            new Category
            {
                Id = Guid.Parse("ada50148-57e7-4702-8b03-7f8f0622b38e"),
                Name = "CSS",
                Slug = "css-blogs"
            },
            new Category
            {
                Id = Guid.Parse("09f35d85-9e8e-4e44-ae91-c276743e2a08"),
                Name = "JavaScript",
                Slug = "js-blogs"
            },
            new Category
            {
                Id = Guid.Parse("37384065-f8dc-4e11-9476-727e38c84773"),
                Name = "TypeScript",
                Slug = "ts-blogs"
            },
            new Category
            {
                Id = Guid.Parse("a0d6745d-b68d-46f5-a03a-6db83a4e7750"),
                Name = "Node",
                Slug = "node-blogs"
            },
            new Category
            {
                Id = Guid.Parse("0703b36b-fd1d-4ec5-99af-540278e3727b"),
                Name = "NestJS",
                Slug = "nest-blogs"
            },
            new Category
            {
                Id = Guid.Parse("675384ca-b046-4de9-843f-3b81d2d89801"),
                Name = "Python",
                Slug = "python-blogs"
            },
            new Category
            {
                Id = Guid.Parse("4e23b927-647c-4114-81c3-63d39bbb890a"),
                Name = "Rust",
                Slug = "rust-blogs"
            },
            new Category
            {
                Id = Guid.Parse("252a07cb-d1df-4029-83fe-a7c35a0d9c47"),
                Name = "React",
                Slug = "react-blogs"
            },
            new Category
            {
                Id = Guid.Parse("e956e7f0-2518-41a5-b8e1-4ec91290f365"),
                Name = "Java",
                Slug = "java-blogs"
            },
            new Category
            {
                Id = Guid.Parse("2a94d6b9-8755-4c1f-beb9-44391e84e229"),
                Name = "Artificial Intelligence",
                Slug = "ai-blogs"
            },
            new Category
            {
                Id = Guid.Parse("cc2bf2f6-41d9-4883-a42a-076a02bc1be8"),
                Name = ".NET Framework",
                Slug = "dotnet-framework-blogs"
            },
            new Category
            {
                Id = Guid.Parse("7ee044bd-7959-45e9-b1f5-5becebec2270"),
                Name = "HTTP",
                Slug = "http-blogs"
            },
        ];

        builder.Entity<Category>().HasData(categories);

        builder.Entity<BlogPost>().Navigation(e => e.Categories).AutoInclude();
    }

    public DbSet<BlogPost> BlogPosts { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<BlogImage> BlogImages { get; set; }
}
