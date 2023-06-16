using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookShopAPI.Data.Entities
{
    [Table("Logs")]
    public class LogsEntity
    {
        [Key]
        public int Id { get; set; }
        public DateTime DateTime { get; set; }
        public string  Action { get; set; } 
    }
}
