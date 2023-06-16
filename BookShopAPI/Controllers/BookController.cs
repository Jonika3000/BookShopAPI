using BookShopAPI.Data;
using BookShopAPI.Data.Entities;
using BookShopAPI.Helpers;
using BookShopAPI.Models.Item;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Drawing;
using static System.Net.Mime.MediaTypeNames;

namespace BookShopAPI.Controllers
{
    [Route("api/book/")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly ApplicationContext applicationContext;
        public BookController(ApplicationContext applicationContext)
        {
            this.applicationContext = applicationContext;
        }

        [HttpGet("list")]
        public async Task<IActionResult> List()
        {
            var result = await applicationContext.Books
                .Select(x => new BookEntity
                {
                    Id = x.Id,
                    Name = x.Name,
                    Description = x.Description,
                    Image = x.Image,
                    PageCount = x.PageCount,
                    Price = x.Price,
                    CategoryId = x.CategoryId,
                    PublishingHouseId = x.PublishingHouseId
                })
                .ToListAsync();
            return Ok(result);
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create([FromForm] ItemCreateViewModel model)
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
            BookEntity book = new BookEntity
            {
                Name = model.Name,
                Description = model.Description,
                Image = imageName,
                PageCount = model.PageCount,
                AuthorId = model.AuthorId,
                CategoryId = model.CategoryId,
                PublishingHouseId = model.PublishingHouseId,
                Price = Convert.ToInt32(model.Price)
            };

            await applicationContext.AddAsync(book);
            await applicationContext.SaveChangesAsync();
            return Ok(book);
        }
        [HttpPost("AddImages")]
        public async Task<IActionResult> AddImages([FromForm] ImagesItemCreateViewModel model)
        {
            String imageName = string.Empty;
            if (model.Url != null)
            {
                var fileExp = Path.GetExtension(model.Url.FileName);
                var dirSave = Path.Combine(Directory.GetCurrentDirectory(), "images");
                imageName = Path.GetRandomFileName() + fileExp;
                using (var ms = new MemoryStream())
                {
                    await model.Url.CopyToAsync(ms);
                    var bmp = new Bitmap(System.Drawing.Image.FromStream(ms));
                    var saveImage = ImageWorker.CompressImage(bmp, 700, 700, false);
                    saveImage.Save(Path.Combine(dirSave, imageName));
                }
            }
            ImagesBookEntity img = new ImagesBookEntity
            {
                Url = imageName,
                ItemId = model.ItemId
            };
            await applicationContext.AddAsync(img);
            await applicationContext.SaveChangesAsync();
            return Ok(img);
        }
        [HttpGet("getBookById/{id}")]
        public async Task<IActionResult> GetBookById(string id)
        { 
            var book = await applicationContext.Books
        .Include(b => b.Author).Include(b=>b.publishingHouse)
        .FirstOrDefaultAsync(b => b.Id == Convert.ToInt32(id));
            if (book == null)
                return BadRequest();
            else
            {
                var result = new
                {
                    Name = book.Name,
                    Description = book.Description,
                    Image = book.Image,
                    PageCount = book.PageCount,
                    Price = Convert.ToInt32(book.Price),
                    AuthorName = book.Author.FirstName +" "+ book.Author.Surname,
                    IdAuthor = book.Author.Id,
                    HouseName = book.publishingHouse.Name,
                    HouseId = book.publishingHouse.Id
                };
                return Ok(result);
            }
        }
        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            int bookId = Convert.ToInt32(id);

            var book = await applicationContext.Books.FindAsync(bookId);
            if (book == null)
                return NotFound();
             
            var bookImages = await applicationContext.ImagesBook.Where(img => img.ItemId == bookId).ToListAsync();
             
            foreach (var image in bookImages)
            {
                string imagePath = Path.Combine(Directory.GetCurrentDirectory(), "images", image.Url);
                if (System.IO.File.Exists(imagePath))
                {
                    System.IO.File.Delete(imagePath);
                }
            }
            applicationContext.ImagesBook.RemoveRange(bookImages);
            string imagePathBook = Path.Combine(Directory.GetCurrentDirectory(), "images", book.Image);
            if (System.IO.File.Exists(imagePathBook))
            {
                System.IO.File.Delete(imagePathBook);
            }
            var salesBook = await applicationContext.Sales.Where(s => s.BookId == bookId).ToListAsync();
            applicationContext.Sales.RemoveRange(salesBook);
            applicationContext.Books.Remove(book);

            await applicationContext.SaveChangesAsync();

            return Ok();
        }

        [HttpGet("getBookImagesById/{id}")]
        public async Task<IActionResult> getBookImagesById(string id)
        {
            var result = await applicationContext.Books.Where(b => b.Id == Convert.ToInt32(id)).FirstAsync();
            var images = await applicationContext.ImagesBook.Where(i => i.ItemId == Convert.ToInt32(id)).
                Select(x => new ImagesBookEntity
                {
                    Url = x.Url
                }).ToListAsync();
            if (images == null)
                return BadRequest();
            else
                return Ok(images);
        }
    }
}
