using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using BookShopAPI.Data.Entities;
using BookShopAPI.Data.Entities.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace BookShopAPI.Data
{
    public class ApplicationContext : IdentityDbContext<UserEntity, RoleEntity, int,
        IdentityUserClaim<int>, UserRoleEntity, IdentityUserLogin<int>,
        IdentityRoleClaim<int>, IdentityUserToken<int>>
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options)
          : base(options)
        {

        }
        public DbSet<PublishingHouseEntity> PublishingHouses { get; set; }
        public DbSet<AuthorEntity> Authors { get; set; }
        public DbSet<LogsEntity> Logs { get; set; }
        public DbSet<BlogEntity> Blogs { get; set; }
        public DbSet<CategoryEntity> Categories { get; set; } 
        public DbSet<ImagesBookEntity> ImagesBook { get; set; }
        public DbSet<BookEntity> Books { get; set; }
        public DbSet<SalesEntity> Sales { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BookEntity>()
                .ToTable(tb => tb.HasTrigger("InsertBookTrigger"));
            base.OnModelCreating(modelBuilder);
              
            modelBuilder.Entity<AuthorEntity>()
             .ToTable(tb => tb.HasTrigger("InsertAuthorTrigger"));

            modelBuilder.Entity<UserRoleEntity>(ur =>
            {
                ur.HasKey(ur => new { ur.UserId, ur.RoleId });

                ur.HasOne(ur => ur.Role)
                    .WithMany(r => r.UserRoles)
                    .HasForeignKey(r => r.RoleId)
                    .IsRequired();

                ur.HasOne(ur => ur.User)
                    .WithMany(r => r.UserRoles)
                    .HasForeignKey(u => u.UserId)
                    .IsRequired();
            });
        } 
    }
}
