using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using backend.Database;

namespace backend.Models
{
    public class sqlServerBlogPostContext: IBlogPostContext
    {
        private readonly sqlServerDbContext context;

        public sqlServerBlogPostContext(sqlServerDbContext context)
        {
            this.context = context;
        }


        public async Task<IEnumerable<BlogPost>> GetBlogPosts()
        {
            var blogs = await context.Blogs.ToListAsync();
            return blogs;
        }


        public async Task<BlogPost> AddBlogPost(BlogPostDto blogPost)
        {   
            var newBlogPost = new BlogPost{
                Title = blogPost.Title,
                Body = blogPost.Body,
                ShortText = blogPost.ShortText,
                DateCreated = DateTime.Now
            };
            var result = await context.Blogs.AddAsync(newBlogPost);
            await context.SaveChangesAsync();
            return result.Entity;
        }
    }


}