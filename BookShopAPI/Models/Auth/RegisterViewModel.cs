﻿namespace BookShopAPI.Models.auth
{
    public class RegisterViewModel
    {
        public string NickName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public IFormFile Avatar { get; set; }
    }
}
