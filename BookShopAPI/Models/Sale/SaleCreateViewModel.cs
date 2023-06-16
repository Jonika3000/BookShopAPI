using BookShopAPI.Data.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookShopAPI.Models.Sale
{
    public class SaleCreateViewModel
    {
        public int Id { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public int BookId { get; set; } 
        public string FullName { get; set; }
        public string Email { get; set; }
    }
}
