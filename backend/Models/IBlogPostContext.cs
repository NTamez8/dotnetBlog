using Microsoft.AspNetCore.Mvc;
namespace backend.Models
{
    public interface IBlogPostContext
    {
        public Task<IEnumerable<BlogPost>> GetBlogPosts();

        public Task<BlogPost> AddBlogPost(BlogPostDto blogPost);
    }    
}