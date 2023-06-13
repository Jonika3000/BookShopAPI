using BookShopAPI.Data.Entities;
using Microsoft.AspNetCore.Mvc;
using static System.Net.Mime.MediaTypeNames;
using System;
using BookShopAPI.Data;
using BookShopAPI.Models.Category;
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
            var result = await applicationContext.Categories
                .Select(x => new CategoryViewModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    Description = x.Description 
                })
                .ToListAsync();
            return Ok(result);
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
