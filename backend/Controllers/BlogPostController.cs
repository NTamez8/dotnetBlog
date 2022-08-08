using Microsoft.AspNetCore.Mvc;
using backend.Models;

namespace backend.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class BlogPostController: ControllerBase
    {
        private readonly IBlogPostContext blogPostContext;
        public BlogPostController(IBlogPostContext blogPostContext)
        {
            this.blogPostContext = blogPostContext;
        }


        [HttpGet]
        public async Task<ActionResult> getBlogPost()
        {
            return Ok(await blogPostContext.GetBlogPosts());
        }

        [HttpPost]
        public async Task<ActionResult> AddUser([FromBody] BlogPostDto blogPost)
        {
            var newBlogPost = await blogPostContext.AddBlogPost(blogPost);
            return Ok(newBlogPost);
        }
    }
}