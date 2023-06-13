using BookShopAPI.Data.Entities;
using BookShopAPI.Data;
using BookShopAPI.Models.Category;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BookShopAPI.Models.Author;
using BookShopAPI.Helpers;
using System.Drawing;

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
                Books = new List<ItemEntity>()
            };
            await applicationContext.AddAsync(author);
            await applicationContext.SaveChangesAsync();
            return Ok(author);
        }
    }
}
