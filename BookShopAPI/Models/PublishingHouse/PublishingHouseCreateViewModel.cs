using BookShopAPI.Data.Entities;

namespace BookShopAPI.Models.PublishingHouse
{
    public class PublishingHouseCreateViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public IFormFile Image { get; set; } 
    }
}
