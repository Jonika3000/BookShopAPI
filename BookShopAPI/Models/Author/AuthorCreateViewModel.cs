using BookShopAPI.Data.Entities;

namespace BookShopAPI.Models.Author
{
    public class AuthorCreateViewModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string Surname { get; set; }
        public IFormFile Image { get; set; }
        public string Description { get; set; }
        public string Year { get; set; } 
    }
}
