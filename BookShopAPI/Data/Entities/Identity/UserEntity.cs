using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace BookShopAPI.Data.Entities.Identity
{
    public class UserEntity : IdentityUser<int>
    {
        [StringLength(100)]
        public string Nick { get; set; }
        public virtual ICollection<UserRoleEntity> UserRoles { get; set; }
    }
}
