using Microsoft.EntityFrameworkCore;

namespace RestaurantRaterMVC.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<RestaurantEntity> Restaurants { get; set; }
        public DbSet<RatingEntity> Ratings { get; set; }
    }
}
