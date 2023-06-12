using System.ComponentModel.DataAnnotations.Schema;

namespace BookShopAPI.Data.Entities
{
    [Table("Blogs")]
    public class BlogEntity
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
    }
}
