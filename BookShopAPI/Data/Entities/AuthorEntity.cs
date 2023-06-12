using System.ComponentModel.DataAnnotations.Schema;

namespace BookShopAPI.Data.Entities
{
    [Table("Authors")]
    public class AuthorEntity
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string Surname { get; set; }
        public string Description { get; set; } 
        public string Year { get; set; }
        public List<ItemEntity> Books { get; set; }
    }
}
