using Microsoft.EntityFrameworkCore;

namespace UrlShortener.Models
{
    public class UrlInfoDataContext : DbContext
    {
        public UrlInfoDataContext(DbContextOptions<UrlInfoDataContext> options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.UseSerialColumns();
            modelBuilder.Entity<UrlInfo>()
                .HasIndex(b => b.Url)
                .IsUnique();
        }

        public DbSet<UrlInfo> UrlInfos { get; set; } = null!;
    }
}
