using Microsoft.EntityFrameworkCore;
using Seedium.Data;
using Seedium.Models.Domain;
using Seedium.Repositories.Interface;

namespace Seedium.Repositories.Implementation;

public class BlogPostRepository : IBlogPostRepository
{
    private readonly AppDbContext _context;

    public BlogPostRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<BlogPost>> GetAllAsync(
        int pageSize,
        int pageNumber,
        bool isAsc,
        string? sortBy,
        string? filterOn,
        string? filterQuery
    )
    {
        var blogs = _context.BlogPosts.Include("Categories").AsQueryable();

        if (string.IsNullOrWhiteSpace(filterOn) == false && string.IsNullOrWhiteSpace(filterQuery) == false)
        {
            if (filterOn.Equals("Title", StringComparison.OrdinalIgnoreCase))
            {
                blogs = blogs.Where(x => x.Title.Contains(filterQuery));
            }
            if (filterOn.Equals("content"))
            {
                blogs = blogs.Where(x => x.Title.Contains(filterQuery));
            }
        }

        if (!string.IsNullOrWhiteSpace(sortBy))
        {
            if (sortBy.Equals("title"))
            {
                blogs = isAsc
                ? blogs.OrderBy(x => x.Title)
                : blogs.OrderByDescending(x => x.Title);
            } else if (sortBy.Equals("content"))
            {
                blogs = isAsc
                ? blogs.OrderBy(x => x.Content)
                : blogs.OrderByDescending(x => x.Content);
            }
        }

        var skipResults = (pageNumber - 1) * pageSize;

        return await blogs
            .Skip(skipResults)
            .Take(pageSize)
            .AsNoTracking().ToListAsync();
    }

    public async Task<BlogPost?> GetByIdAsync(Guid id) =>
        await _context.BlogPosts.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);

    public async Task<BlogPost?> GetByUrlAsync(string url) =>
        await _context.BlogPosts.AsNoTracking().FirstOrDefaultAsync(x => x.Slug == url);

    public async Task CreateAsync(BlogPost blogPost)
    {
        await _context.BlogPosts.AddAsync(blogPost);
        await _context.SaveChangesAsync();
    }

    public async Task<BlogPost?> UpdateAsync(Guid id, BlogPost blogPost)
    {
        var existingBlogPost = await GetByIdAsync(id);
        if (existingBlogPost != null)
        {
            existingBlogPost.Title = blogPost.Title;
            existingBlogPost.Author = blogPost.Author;
            existingBlogPost.ShortDescription = blogPost.ShortDescription;
            existingBlogPost.Slug = blogPost.Slug;
            existingBlogPost.Content = blogPost.Content;
            existingBlogPost.IsVisible = blogPost.IsVisible;
            existingBlogPost.Categories = blogPost.Categories;
            await _context.SaveChangesAsync();
        }
        return existingBlogPost;
    }

    public async Task DeleteAsync(Guid id)
    {
        var existingBlogPost = await GetByIdAsync(id);
        if (existingBlogPost != null)
        {
            _context.BlogPosts.Remove(existingBlogPost);
            await _context.SaveChangesAsync();
        }
    }
}