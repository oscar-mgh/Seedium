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
        string? filterOn,
        string? filterQuery,
        string? sortBy,
        bool sortDesc,
        int pageNumber,
        int pageSize
    )
    {
        var blogs = _context.BlogPosts.Include(x => x.Categories).AsQueryable();

        if (!string.IsNullOrWhiteSpace(filterOn) && !string.IsNullOrWhiteSpace(filterQuery))
        {
            if (filterOn.Equals("Title", StringComparison.OrdinalIgnoreCase))
            {
                blogs = blogs.Where(x => x.Title.Contains(filterQuery, StringComparison.OrdinalIgnoreCase));
            }
            if (filterOn.Equals("Content", StringComparison.OrdinalIgnoreCase))
            {
                blogs = blogs.Where(x => x.Title.Contains(filterQuery, StringComparison.OrdinalIgnoreCase));
            }
        }

        if (!string.IsNullOrWhiteSpace(sortBy))
        {
            if (sortBy.Equals("Title", StringComparison.OrdinalIgnoreCase))
            {
                blogs = sortDesc
                ? blogs.OrderByDescending(x => x.Title)
                : blogs.OrderBy(x => x.Title);
            }
            else if (sortBy.Equals("Content", StringComparison.OrdinalIgnoreCase))
            {
                blogs = sortDesc
                ? blogs.OrderByDescending(x => x.Content)
                : blogs.OrderBy(x => x.Content);
            }
        }

        var skipResults = (pageNumber - 1) * pageSize;

        return await _context.BlogPosts
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
