using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookShopAPI.Data.Entities
{
    public class SalesEntity
    {
        [Key]
        public int Id { get; set; }
        public string Address { get; set; }
        public string City { get; set; }    
        public int BookId { get; set; }
        [ForeignKey("BookId")]
        public BookEntity Book { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
    }
}
