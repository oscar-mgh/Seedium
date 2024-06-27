using Seedium.Models.Domain;

namespace Seedium.Data;

public class Seeder
{
    public static async Task Seed(
        AppDbContext appDbContext
    )
    {
        if (!appDbContext.BlogPosts.Any())
        {
            var blogPosts = new List<BlogPost>
            {
                new()
                {
                    Id = Guid.Parse("b4b4cc74-11ac-41ce-ba2e-779d04cc12f6"),
                    Title = "Creating an Interactive HTML Website with ChatGPT: Enhancing User Engagement",
                    ShortDescription = "While HTML (Hypertext Markup Language) is the backbone of any website, adding conversational elements can take user engagement to a whole new level. In this blog post, we will explore in detail how to create an HTML website with ChatGPT, a powerful language model developed by OpenAI. By integrating ChatGPT into your website, you can provide users with a personalized and interactive experience. So, let''s dive in and learn how to harness the capabilities of ChatGPT to enhance your HTML website!",
                    Content = $"{BlogPostContent.HtmlWebsiteUsingChatgpt}",
                    FeaturedImageUrl = "https://images.pexels.com/photos/16037283/pexels-photo-16037283/free-photo-of-open-laptop-on-desk.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=1",
                    Slug = "html-website-using-chatgpt",
                    Author = "Admin",
                    PublishedDate = DateTime.Parse("2023-08-08 11:33:50"),
                    IsVisible = false
                },
                new()
                {
                    Id = Guid.Parse("c5c16487-1c86-469a-8e07-06d750171158"),
                    Title = "Mastering HTTP Requests with JavaScript: A Comprehensive Guide",
                    ShortDescription = "In the ever-evolving landscape of web development, interacting with external APIs and sending HTTP requests is a fundamental skill. JavaScript, being the language of the web, provides developers with several methods and libraries to efficiently handle this task.",
                    Content = $"{BlogPostContent.HttpRequestsJavascript}",
                    FeaturedImageUrl = "https://images.pexels.com/photos/5483077/pexels-photo-5483077.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=1",
                    Slug = "http-requests-javascript",
                    Author = "AI Generated",
                    PublishedDate = DateTime.Parse("2023-09-02 14:56:00"),
                    IsVisible = true
                },
                new()
                {
                    Id = Guid.Parse("d02b4737-4692-4fd1-984c-61d2219813b3"),
                    Title = "Building a Simple Web API Using C# and .NET Core: A Step-by-Step Guide",
                    ShortDescription = "Building a robust and efficient web API is a fundamental skill for modern application development. In this comprehensive guide, we will walk you through the process of creating a simple web API using C# and .NET Core.",
                    Content = $"{BlogPostContent.WebApiCsharp}",
                    FeaturedImageUrl = "https://images.pexels.com/photos/7367/startup-photo.jpg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=1",
                    Slug = "web-api-csharp",
                    Author = "AI Generated",
                    PublishedDate = DateTime.Parse("2024-04-03 11:25:00"),
                    IsVisible = true
                },
                new()
                {
                    Id = Guid.Parse("da90a019-9bc2-4cea-b843-5650a8c9cd61"),
                    Title = "Reading an Excel File in C#: A Step-by-Step Guide",
                    ShortDescription = "Reading data from Excel files is a common requirement in many applications. In this comprehensive guide, we will walk you through the process of reading an Excel file in C# using the .NET framework",
                    Content = $"{BlogPostContent.ReadExcelCsharp}",
                    FeaturedImageUrl = "https://images.pexels.com/photos/590022/pexels-photo-590022.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=1",
                    Slug = "read-excel-csharp",
                    Author = "AI Generated",
                    PublishedDate = DateTime.Parse("2024-05-04 12:20:00"),
                    IsVisible = true
                },
                new()
                {
                    Id = Guid.Parse("cf8e76ac-1a2e-40b1-ab6d-bf2f6a11b5b9"),
                    Title = "Building a Simple Navigation Bar: A Step-by-Step Guide",
                    ShortDescription = "A navigation bar is an essential component of any website or web application, providing users with easy access to different sections and pages.",
                    Content = $"{BlogPostContent.SimpleNavigationBar}",
                    FeaturedImageUrl = "https://images.pexels.com/photos/109371/pexels-photo-109371.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=1",
                    Slug = "simple-navigation-bar",
                    Author = "AI Generated",
                    PublishedDate = DateTime.Parse("2024-04-04 14:24:00"),
                    IsVisible = true
                },
                new()
                {
                    Id = Guid.Parse("292596c8-1e6e-4e69-990e-1737294752ed"),
                    Title = "Creating a Simple Calculator Application using ASP.NET Core Console App",
                    ShortDescription = "ASP.NET Core is a powerful framework for building cross-platform web applications. While it is commonly used for developing web applications, ASP.NET Core can also be leveraged to create console applications. In this blog post, we will explore in detail how to create a simple calculator application using ASP.NET Core console app. We will cover the step-by-step process of setting up the project, implementing the calculator logic, and running the application. So, let''s get started!",
                    Content = $"{BlogPostContent.SimpleCalculatorAppAspnetCore}",
                    FeaturedImageUrl = "https://images.pexels.com/photos/1181263/pexels-photo-1181263.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=1",
                    Slug = "simple-calculator-app-aspnet-core",
                    Author = "AI Generated",
                    PublishedDate = DateTime.Now,
                    IsVisible = true,
                }
            };

            var dbCategories = appDbContext.Categories.AsQueryable();
            blogPosts[0].Categories = [
                .. dbCategories.Where(x => x.Id == Guid.Parse("a9efd598-49ac-4fe7-814c-aa7ff2947ee1")),
                .. dbCategories.Where(x => x.Id == Guid.Parse("2a94d6b9-8755-4c1f-beb9-44391e84e229"))
            ];
            blogPosts[1].Categories = [
                .. dbCategories.Where(x => x.Id == Guid.Parse("7ee044bd-7959-45e9-b1f5-5becebec2270")),
                .. dbCategories.Where(x => x.Id == Guid.Parse("09f35d85-9e8e-4e44-ae91-c276743e2a08"))
            ];
            blogPosts[2].Categories = [
                .. dbCategories.Where(x => x.Id == Guid.Parse("34680195-b466-4bec-b015-e6c17fe4f212")),
                .. dbCategories.Where(x => x.Id == Guid.Parse("29316fd9-ee3f-4473-8ca2-e3acf9ad8623"))
            ];
            blogPosts[3].Categories = [
                .. dbCategories.Where(x => x.Id == Guid.Parse("34680195-b466-4bec-b015-e6c17fe4f212"))
            ];
            blogPosts[4].Categories = [
                .. dbCategories.Where(x => x.Id == Guid.Parse("a9efd598-49ac-4fe7-814c-aa7ff2947ee1")),
                .. dbCategories.Where(x => x.Id == Guid.Parse("ada50148-57e7-4702-8b03-7f8f0622b38e"))
            ];
            blogPosts[5].Categories = [
                .. dbCategories.Where(x => x.Id == Guid.Parse("34680195-b466-4bec-b015-e6c17fe4f212")),
                .. dbCategories.Where(x => x.Id == Guid.Parse("29316fd9-ee3f-4473-8ca2-e3acf9ad8623"))
            ];

            await appDbContext.BlogPosts.AddRangeAsync(blogPosts);
            await appDbContext.SaveChangesAsync();
        }
    }
}