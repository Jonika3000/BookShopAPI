using BookShopAPI.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Reflection.Metadata;

namespace BookShopAPI.Data
{
    public class ApplicationContext:DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options)
          : base(options)
        {

        }
        public DbSet<PublishingHouseEntity> PublishingHouses { get; set; }
        public DbSet<AuthorEntity> Authors { get; set; }
        public DbSet<BlogEntity> Blogs { get; set; }
        public DbSet<CategoryEntity> Categories { get; set; } 
        public DbSet<ImagesBookEntity> ImagesBook { get; set; }
        public DbSet<ItemEntity> Books { get; set; }
        public DbSet<SalesEntity> Sales { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ItemEntity>()
                .ToTable(tb => tb.HasTrigger("trg_Books_Insert"));
        }
    }
}
