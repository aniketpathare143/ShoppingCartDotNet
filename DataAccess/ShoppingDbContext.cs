using Microsoft.EntityFrameworkCore;
using ShoppingCartAPIs.Models;

namespace ShoppingCartAPIs.DataAccess
{
    public class ShoppingDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<UserReview> UserReviews { get; set; }
        public DbSet<PlacedOrder> PlacedOrders { get; set; }

        public ShoppingDbContext(DbContextOptions<ShoppingDbContext> options)
            : base(options)
        { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var connectionString = configuration.GetConnectionString("DbConnectionLocal");
            optionsBuilder.UseSqlServer(connectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .Property(e => e.DOB)
                .HasColumnType("Date");

            // Configure the relationship between PlacedOrder and Product
            modelBuilder.Entity<PlacedOrder>()
                .HasOne(p => p.Product)
                .WithMany()
                .HasForeignKey(p => p.ProductId)
                .OnDelete(DeleteBehavior.Restrict); // Specify the desired cascade behavior here
        }
    }
}
