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
        [HttpPost("edit/{id}")]
        public async Task<IActionResult> Edit([FromForm] CategoryEntity model)
        {
            var itemEdit = await applicationContext.Categories.FirstOrDefaultAsync(a => a.Id == model.Id);
            if (itemEdit == null)
            {
                return NotFound();
            }
            itemEdit.Slug = model.Slug;
            itemEdit.Description = model.Description;
            itemEdit.Name = model.Name;

            applicationContext.Entry(itemEdit).State = EntityState.Modified;
            await applicationContext.SaveChangesAsync();

            return Ok(itemEdit);

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
        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var Category = await applicationContext.Categories.FindAsync(Convert.ToInt32(id));
            if (Category == null)
                return NotFound();

             
            var CategoryBooks = await applicationContext.Books.Where(b => b.CategoryId ==
            Convert.ToInt32(id)).ToListAsync();
            applicationContext.Books.RemoveRange(CategoryBooks);
            applicationContext.Categories.Remove(Category);
            await applicationContext.SaveChangesAsync();

            return Ok();
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
