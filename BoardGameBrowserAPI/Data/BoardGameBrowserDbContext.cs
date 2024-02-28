using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;

namespace BoardGameBrowserAPI.Data
{
    public class BoardGameBrowserDbContext : IdentityDbContext<IdentityUser>
    {
        public BoardGameBrowserDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<IdentityRole>().HasData(
                new IdentityRole
                {
                    Name = "Administrator",
                    NormalizedName = "ADMINISTRATOR"
                }
                );
        }

        public DbSet<BoardGame> BoardGames { get; set; }
        public DbSet<Designer> Designers { get; set; }
        public DbSet<Category> Categories { get; set; }

    }
}
