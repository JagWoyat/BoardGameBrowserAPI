using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;

namespace BoardGameBrowserAPI.Data
{
    public class BoardGameBrowserDbContext : DbContext
    {
        public BoardGameBrowserDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<BoardGame> BoardGames { get; set; }
        public DbSet<Designer> Designers { get; set; }
        public DbSet<Category> Categories { get; set; }

    }
}
