namespace BookShopAPI.Data.Entities
{
    public class PublishingHouseEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description {get; set; }
        public string Image { get; set; }
        public List<ItemEntity> Books { get; set; }
    }
}
