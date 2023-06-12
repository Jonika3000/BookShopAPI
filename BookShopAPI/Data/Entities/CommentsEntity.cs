using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookShopAPI.Data.Entities
{
    [Table("Comments")]
    public class CommentsEntity
    {
        [Key]
        public int Id { get; set; }
        public int UserId { get; set; }
        [ForeignKey("UserId")]
        public UserEntity User { get; set; }
        public int BookId { get; set; }
        [ForeignKey("BookId")]
        public ItemEntity Item { get; set; }
        public string content { get; set; }
    }
}
