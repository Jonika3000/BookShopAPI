using BookShopAPI.Models.Category;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookShopAPI.Models.Item
{
    public class ItemCreateViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public IFormFile Image { get; set; }
        public string Price { get; set; }
        public int PageCount { get; set; }
        public int CategoryId { get; set; }
        public int AuthorId { get; set; }
        public int PublishingHouseId { get; set; }
    }
}
