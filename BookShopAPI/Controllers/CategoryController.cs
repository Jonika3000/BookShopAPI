using BookShopAPI.Data.Entities;
using Microsoft.AspNetCore.Mvc; 
using BookShopAPI.Data; 
using Microsoft.EntityFrameworkCore;

namespace BookShopAPI.Controllers
{
    [Route("api/category/")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ApplicationContext applicationContext; 
        public CategoryController(ApplicationContext applicationContext)
        {
            this.applicationContext = applicationContext; 
        }

        [HttpGet("list")]
        public async Task<IActionResult> List()
        {
            var result = await applicationContext.Categories.ToListAsync();
            return Ok(result);
        }
        [HttpGet("category/{slug}")]
        public async Task<IActionResult> GetBooksBySlug(string Slug)
        {
            if(Slug == "all")
            {
                var allBooks = await applicationContext.Books.ToListAsync();
                return Ok(allBooks);
            }
            else
            {
                var result = await applicationContext.Books.Where(b => b.Category.Slug == Slug).ToListAsync();
                return Ok(result);
            } 
            return BadRequest();
        }
        [HttpPost("create")]
        public async Task<IActionResult> Create([FromForm] CategoryEntity model)
        {
            CategoryEntity category = new CategoryEntity
            {
                Name = model.Name,
                Description = model.Description,
                Slug = model.Slug
            };
            await applicationContext.AddAsync(category);
            await applicationContext.SaveChangesAsync();
            return Ok(category);
        }
    }
} 
