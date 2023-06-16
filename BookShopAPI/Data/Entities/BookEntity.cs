using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookShopAPI.Data.Entities
{
    [Table("Books")]
    public class BookEntity
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public int PageCount { get; set;}
        public int Price { get; set; }   
        public int CategoryId { get; set; }
        [ForeignKey(nameof(CategoryId))]
        public CategoryEntity Category { get; set; }
        public int PublishingHouseId { get; set; }

        [ForeignKey(nameof(PublishingHouseId))]
        public PublishingHouseEntity  publishingHouse { get; set; }
        public int AuthorId { get; set; }

        [ForeignKey(nameof(AuthorId))]
        public AuthorEntity Author { get; set; }
    }
}
