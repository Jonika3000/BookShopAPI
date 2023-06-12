using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookShopAPI.Data.Entities
{
    [Table("Books")]
    public class ItemEntity
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public int PageCount { get; set;}
        public int CategoryId { get; set; }
        [ForeignKey(nameof(CategoryId))]
        public CategoryEntity Category { get; set; }
    }
}
