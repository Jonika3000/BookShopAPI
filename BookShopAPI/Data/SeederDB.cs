using BookShopAPI.Data.Entities.Identity;
using BookShopAPI.Data.Entities;
using Microsoft.AspNetCore.Identity;
using System.Data;
using System;
using Microsoft.EntityFrameworkCore;
using BookShopAPI.Constants;

namespace BookShopAPI.Data
{
    public static class SeederDB
    {
        public static void SeedData(this IApplicationBuilder app)
        {
            using (var scope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var service = scope.ServiceProvider;
                var context = service.GetRequiredService<ApplicationContext>();
                var userNamager = service.GetRequiredService<UserManager<UserEntity>>();
                var roleNamager = service.GetRequiredService<RoleManager<RoleEntity>>();
                context.Database.Migrate();  
 
                if (!context.Roles.Any())
                {
                    foreach (string name in Roles.All)
                    {
                        var role = new RoleEntity
                        {
                            Name = name
                        };
                        var result = roleNamager.CreateAsync(role).Result;
                    }
                }
                if (!context.Users.Any())
                {
                    var user = new UserEntity()
                    { 
                        Email = "admin@gmail.com",
                        UserName = "admin@gmail.com",
                        Nick ="admin"
                    };
                    var result = userNamager.CreateAsync(user, "123456").Result;
                    if (result.Succeeded)
                    {
                        result = userNamager.AddToRoleAsync(user, Roles.Admin).Result;
                    }
                }

            }
        }
    }
}
