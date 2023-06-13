using BookShopAPI.Data.Entities;
using BookShopAPI.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http.HttpResults;
using BookShopAPI.Models.PublishingHouse;
using BookShopAPI.Helpers;
using System.Drawing;

namespace BookShopAPI.Controllers
{
    [Route("api/PublishingHouses/")]
    [ApiController]
    public class PublishingHousesController : ControllerBase
    {
        private readonly ApplicationContext applicationContext;
        public PublishingHousesController(ApplicationContext applicationContext)
        {
            this.applicationContext = applicationContext;
        }

        [HttpGet("list")]
        public async Task<IActionResult> List()
        {
            var result = await applicationContext.PublishingHouses
                .Select(x => new PublishingHouseEntity
                {
                    Id = x.Id,
                    Name = x.Name,
                    Description = x.Description,
                    Image = x.Image, 
                    Books = x.Books
                })
                .ToListAsync();
            return Ok(result);
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create([FromForm] PublishingHouseCreateViewModel model)
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
                    saveImage.Save(Path.Combine(dirSave, imageName+".png"));
                }
            }
            PublishingHouseEntity PublishingHouse = new PublishingHouseEntity
            {
                Id = model.Id,
                Name = model.Name,
                Description = model.Description,
                Image = imageName 
            };
            await applicationContext.AddAsync(PublishingHouse);
            await applicationContext.SaveChangesAsync();
            return Ok(PublishingHouse);
        }
    }
}
