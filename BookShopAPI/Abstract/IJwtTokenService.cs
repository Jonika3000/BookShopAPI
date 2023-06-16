using BookShopAPI.Data.Entities.Identity;

namespace BookShopAPI.Abstract
{
    public interface IJwtTokenService
    {
        Task<string> CreateToken(UserEntity user);
    }
}
