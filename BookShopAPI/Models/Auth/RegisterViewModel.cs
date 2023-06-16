namespace BookShopAPI.Models.Auth
{
    public class RegisterViewModel
    {
        public string Nick { get; set; }  
        public string Email { get; set; } 
        public string Password { get; set; } 
        public string ConfirmPassword { get; set; }
    }
}
