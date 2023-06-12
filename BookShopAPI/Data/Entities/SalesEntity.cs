namespace BookShopAPI.Data.Entities
{
    public class SalesEntity
    {
        public int Id { get; set; }
        public string Adress { get; set; }
        public string City { get; set; }    
        public int BookId { get; set; } 
        public ItemEntity Book { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
    }
}
