using System.ComponentModel.DataAnnotations.Schema;

namespace BookShopAPI.Models.Item
{
    public class ItemImages
    {
        public int Id { get; set; }
        public string Url { get; set; }
        public int ItemId { get; set; }  
    }
}
