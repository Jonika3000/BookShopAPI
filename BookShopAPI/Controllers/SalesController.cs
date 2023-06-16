using BookShopAPI.Data.Entities;
using BookShopAPI.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BookShopAPI.Models.Sale;

namespace BookShopAPI.Controllers
{
    [Route("api/sales/")]
    [ApiController]
    public class SalesController : ControllerBase
    {
        private readonly ApplicationContext applicationContext;
        public SalesController(ApplicationContext applicationContext)
        {
            this.applicationContext = applicationContext;
        }
        [HttpGet("list")]
        public async Task<IActionResult> List()
        {
            var result = await applicationContext.Sales.ToListAsync();
            return Ok(result);
        }
        [HttpPost("create")]
        public async Task<IActionResult> Create([FromForm] SaleCreateViewModel model)
        {
            SalesEntity sale = new SalesEntity
            { 
                Address = model.Address,
                City = model.City,
                BookId = model.BookId,  
                FullName = model.FullName,  
                Email = model.Email 
            };
            await applicationContext.AddAsync(sale);
            await applicationContext.SaveChangesAsync();
            return Ok(sale);
        }
    }
}
