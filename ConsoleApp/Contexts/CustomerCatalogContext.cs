using ConsoleApp.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace ConsoleApp.Contexts
{
    public class CustomerCatalogContext : DbContext
    {
        public CustomerCatalogContext(DbContextOptions<CustomerCatalogContext> options) : base(options)
        {
        }

        public DbSet<CustomerEntity> Customers { get; set; }
        public DbSet<AddressEntity> Addresses { get; set; }
        public DbSet<OrderEntity> Orders { get; set; }
        public DbSet<OrderDetailEntity> OrderItems { get; set; }
        public DbSet<ProductEntity> Products { get; set; }
        public DbSet<ProductCategoryEntity> ProductCategories { get; set; }
        public DbSet<CategoryEntity> Categories { get; set; }

        public static CustomerCatalogContext Create(DbContextOptions<CustomerCatalogContext> options)
        {
            return new CustomerCatalogContext(options);
        }

        public static CustomerCatalogContext Create(IConfiguration configuration)
        {
            var optionsBuilder = new DbContextOptionsBuilder<CustomerCatalogContext>()
                .UseSqlServer(configuration.GetConnectionString("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Projects\\Datalagring\\ConsoleApp\\ConsoleApp\\Data\\CustomerCatalog.mdf;Integrated Security=True;Connect Timeout=30;Encrypt=True"));

            return new CustomerCatalogContext(optionsBuilder.Options);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CustomerEntity>(entity =>
            {
                entity.ToTable("Customers"); 
                entity.Property(c => c.CustomerID).ValueGeneratedOnAdd();
                entity.Property(c => c.FirstName).IsRequired();
                entity.Property(c => c.LastName).IsRequired();
                entity.Property(c => c.Email).IsRequired().HasMaxLength(255);

                // Relationships
                entity.HasOne(c => c.Address)
                    .WithMany()
                    .HasForeignKey(c => c.AddressID);
            });

            modelBuilder.Entity<OrderEntity>(entity =>
            {
                entity.ToTable("Orders");
                entity.HasKey(o => o.OrderID);
                entity.Property(o => o.OrderDate).IsRequired();
                entity.Property(o => o.TotalAmount).IsRequired();

                // Relationships
                entity.HasOne(o => o.Customer)
                    .WithMany(c => c.Orders)
                    .HasForeignKey(o => o.CustomerID);
            });

            modelBuilder.Entity<OrderDetailEntity>(entity =>
            {
                entity.ToTable("OrderDetails");
                entity.HasKey(oi => oi.OrderDetailID);
                entity.Property(oi => oi.Quantity).IsRequired();
                entity.Property(oi => oi.UnitPrice).IsRequired();
                entity.Property(oi => oi.Amount).HasColumnType("decimal(18, 2)").IsRequired();

                // Relationships
                entity.HasOne(oi => oi.Order)
                    .WithMany(o => o.OrderDetails)
                    .HasForeignKey(oi => oi.OrderID);

                entity.HasOne(oi => oi.Product)
                    .WithMany(p => p.OrderDetails)
                    .HasForeignKey(oi => oi.ProductID);
            });


        }

    }
}
