using BookShopAPI.Data.Entities;
using BookShopAPI.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http.HttpResults;
using BookShopAPI.Models.PublishingHouse;
using BookShopAPI.Helpers;
using System.Drawing;
using Microsoft.Data.SqlClient;

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
        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var House = await applicationContext.PublishingHouses.FindAsync(Convert.ToInt32(id));
            if (House == null)
                return NotFound();

            string imagePath = Path.Combine(Directory.GetCurrentDirectory(), "images", House.Image);
            if (System.IO.File.Exists(imagePath))
            {
                System.IO.File.Delete(imagePath);
            }
            var HouseBooks = await applicationContext.Books.Where(b => b.PublishingHouseId ==
            Convert.ToInt32(id)).ToListAsync();
            applicationContext.Books.RemoveRange(HouseBooks);
            applicationContext.PublishingHouses.Remove(House);
            await applicationContext.SaveChangesAsync();

            return Ok();
        }
        [HttpGet("publishingHouse/{id}")]
        public async Task<IActionResult> publishingHouseById(string id)
        {
            SqlParameter param = new SqlParameter("@HouseId", id);
            var result = await applicationContext.PublishingHouses.FromSqlRaw($"EXEC GetHouseDataById @HouseId", param)
                .ToListAsync();
            if (result[0] == null)
                return BadRequest();
            else
                return Ok(result[0]); 
        }
    }
}
