using BookShopAPI.Data.Entities;
using BookShopAPI.Data;
using BookShopAPI.Models.Category;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BookShopAPI.Models.Author;
using BookShopAPI.Helpers;
using System.Drawing;
using Microsoft.Data.SqlClient;
using System.Numerics;

namespace BookShopAPI.Controllers
{
    [Route("api/author/")]
    [ApiController]
    public class AuthorController : ControllerBase
    {
        private readonly ApplicationContext applicationContext;
        public AuthorController(ApplicationContext applicationContext)
        {
            this.applicationContext = applicationContext;
        }

        [HttpGet("list")]
        public async Task<IActionResult> List()
        {
            var result = await applicationContext.Authors
                .Select(x => new AuthorEntity
                {
                    Id = x.Id,
                    FirstName = x.FirstName,
                    Surname = x.Surname,
                    Description = x.Description,
                    Year = x.Year,
                    Books = x.Books
                })
                .ToListAsync();
            return Ok(result);
        }
        [HttpGet("authorById/{id}")]
        public async Task<IActionResult> authorById(string id)
        {
           SqlParameter param = new  SqlParameter("@id", id);
            var result = await applicationContext.Authors.FromSqlRaw($"EXEC GetAuthorDataById @id",param).ToListAsync();
            if (result[0] == null)
                return BadRequest();
            else
            return Ok(result[0]);
        }
        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var author = await applicationContext.Authors.FindAsync(Convert.ToInt32(id));
            if (author == null)
                return NotFound();
            string imagePath = Path.Combine(Directory.GetCurrentDirectory(), "images", author.Image);
            if (System.IO.File.Exists(imagePath))
            {
                System.IO.File.Delete(imagePath);
            }
            
            var authorBooks = await applicationContext.Books.Where(b => b.AuthorId == 
            Convert.ToInt32(id)).ToListAsync();
            applicationContext.Books.RemoveRange(authorBooks);
            applicationContext.Authors.Remove(author);
            await applicationContext.SaveChangesAsync();

            return Ok();
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create([FromForm] AuthorCreateViewModel model)
        {
            String imageName = string.Empty;
            if (model.Image != null)
            {
                var fileExp = Path.GetExtension(model.Image.FileName);
                var dirSave = Path.Combine(Directory.GetCurrentDirectory(), "images");
                imageName = Path.GetRandomFileName() + fileExp;
                using (var ms = new MemoryStream())
                {
                    await model.Image.CopyToAsync(ms);
                    var bmp = new Bitmap(System.Drawing.Image.FromStream(ms));
                    var saveImage = ImageWorker.CompressImage(bmp, 700, 700, false);
                    saveImage.Save(Path.Combine(dirSave, imageName));
                }
            }
            AuthorEntity author = new AuthorEntity
            {
                Id = model.Id,
                FirstName = model.FirstName,
                Surname = model.Surname,
                Description = model.Description,
                Year = model.Year,
                Image= imageName,
                Books = new List<BookEntity>()
            };
            await applicationContext.AddAsync(author);
            await applicationContext.SaveChangesAsync();
            return Ok(author);
        }
    }
}
