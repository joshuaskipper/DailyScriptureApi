using DailyScriptureApi.Models.Domains;
using Microsoft.EntityFrameworkCore;

namespace DailyScriptureApi.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Verse> Verses { get; set; }
    }
}
