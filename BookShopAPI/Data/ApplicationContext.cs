using BookShopAPI.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;

namespace BookShopAPI.Data
{
    public class ApplicationContext:DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options)
          : base(options)
        {

        }
        public DbSet<AuthorEntity> Authors { get; set; }
        public DbSet<BlogEntity> Blogs { get; set; }
        public DbSet<CategoryEntity> Categories { get; set; }
        public DbSet<CommentsEntity> Comments { get; set; }
        public DbSet<ImagesBookEntity> ImagesBook { get; set; }
        public DbSet<ItemEntity> Books { get; set; }
        public DbSet<UserEntity> Users { get; set; }
    }
}
