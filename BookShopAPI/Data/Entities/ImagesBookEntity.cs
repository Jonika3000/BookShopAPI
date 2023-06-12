using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookShopAPI.Data.Entities
{
    [Table("ImagesBook")]
    public class ImagesBookEntity
    {
        [Key]
        public int Id { get;set; }
        public string Url { get; set; }
        public int ItemId { get; set; }
        [ForeignKey(nameof(ItemId))]
        public ItemEntity Item { get; set; }
    }
}
