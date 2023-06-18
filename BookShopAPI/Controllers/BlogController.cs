using BookShopAPI.Data.Entities;
using BookShopAPI.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BookShopAPI.Models.Item;
using BookShopAPI.Helpers;
using BookShopAPI.Models.Author;
using System.Drawing;

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

        [HttpGet("blog/{id}")]
        public async Task<IActionResult> GetBlogById(string id)
        {
            var result = await applicationContext.Blogs.Where(b=>b.Id == 
            Convert.ToInt32(id)).FirstAsync();
            if (result == null)
                return BadRequest();
            else
            return Ok(result);
        }
        [HttpGet("list")]
        public async Task<IActionResult> List()
        {
            var result = await applicationContext.Blogs.ToListAsync();
            return Ok(result);
        }
        [HttpPost("edit/{id}")]
        public async Task<IActionResult> Edit([FromForm] BlogEntity model)
        {
            var itemEdit = await applicationContext.Blogs.FirstOrDefaultAsync(a => a.Id == model.Id);
            if (itemEdit == null)
            {
                return NotFound();
            }  
            itemEdit.Title = model.Title;
            itemEdit.Content = model.Content; 

            applicationContext.Entry(itemEdit).State = EntityState.Modified;
            await applicationContext.SaveChangesAsync();

            return Ok(itemEdit);

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
        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var blog = await applicationContext.Blogs.FindAsync(Convert.ToInt32(id));
            if (blog == null)
                return NotFound(); 
            applicationContext.Blogs.Remove(blog);
            await applicationContext.SaveChangesAsync();

            return Ok();
        }
    }
}
