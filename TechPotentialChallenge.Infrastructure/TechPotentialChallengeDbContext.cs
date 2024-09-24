using Microsoft.EntityFrameworkCore;
using TechPotentialChallenge.Domain.Model;

namespace TechPotentialChallenge.Infrastructure
{
    public class TechPotentialChallengeDbContext : DbContext
    {
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (Config.Environment == Environment.Production)
                optionsBuilder.UseSqlite(Config.ConnectionString);
            else
                optionsBuilder.UseInMemoryDatabase("Memory");
        }
    }
}
