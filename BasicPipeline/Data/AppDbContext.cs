using BasicPipeline.Classes;
using Microsoft.EntityFrameworkCore;

namespace BasicPipeline.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Products> Products { get; set; }
        public DbSet<Customers> Customers { get; set; }
        public DbSet<Orders> Orders { get; set; }
        public DbSet<Order_Details> Order_Details { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=.;Database=SALES;Trusted_Connection=True;TrustServerCertificate=True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Order_Details>()
                .HasKey(od => new { od.OrderID, od.ProductID });

            modelBuilder.Entity<Order_Details>()
                .HasOne<Orders>()
                .WithMany()
                .HasForeignKey(od => od.OrderID);

            modelBuilder.Entity<Order_Details>()
                .HasOne<Products>()
                .WithMany()
                .HasForeignKey(od => od.ProductID);

            modelBuilder.Entity<Orders>()
                .HasOne<Customers>()
                .WithMany()
                .HasForeignKey(o => o.CustomerID);
        }
    }
}
