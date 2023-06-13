namespace BookShopAPI.Models.Item
{
    public class ImagesItemCreateViewModel
    {
        public int Id { get; set; }
        public IFormFile Url { get; set; }
        public int ItemId { get; set; }
    }
}
