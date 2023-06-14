using BookShopAPI.Data.Entities;
using BookShopAPI.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BookShopAPI.Models.Item;

namespace BookShopAPI.Controllers
{
    [Route("api/blog/")]
    [ApiController]
    public class BlogController : ControllerBase
    {
        private readonly ApplicationContext applicationContext;
        public BlogController(ApplicationContext applicationContext)
        {
            this.applicationContext = applicationContext;
        }

        [HttpGet("list")]
        public async Task<IActionResult> List()
        {
            var result = await applicationContext.Blogs.ToListAsync();
            return Ok(result);
        }
        [HttpPost("create")]
        public async Task<IActionResult> Create([FromForm] BlogEntity model)
        {
            BlogEntity blog = new BlogEntity
            {
                Title = model.Title,
                Content = model.Content 
            };
            await applicationContext.AddAsync(blog);
            await applicationContext.SaveChangesAsync();
            return Ok(blog);
        }
    }
}
