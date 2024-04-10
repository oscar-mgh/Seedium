using System.ComponentModel.DataAnnotations;

namespace Seedium.Models.DTO;

public class CreateBlogPostRequestDto
{
    [Required(ErrorMessage = "Title is required")]
    [MinLength(5, ErrorMessage = "Title must be at least 5 characters")]
    public string Title { get; set; }

    [Required(ErrorMessage = "ShortDescription is required")]
    public string ShortDescription { get; set; }

    [Required(ErrorMessage = "Content is required")]
    public string Content { get; set; }

    [Url(ErrorMessage = "Invalid Url")]
    public string FeaturedImageUrl { get; set; }

    public string Slug { get; set; }

    public DateTime PublishedDate { get; set; }

    public string Author { get; set; }

    public bool IsVisible { get; set; }

    public Guid[] Categories { get; set; }
}
