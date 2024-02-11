using ConsoleApp.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

public partial class ProductCatalogContext : DbContext
{
    public DbSet<CustomerEntity> Customers { get; set; }
    public DbSet<ProductEntity> Products { get; set; }
    public DbSet<OrderEntity> Orders { get; set; }
    public DbSet<OrderDetailEntity> OrderDetails { get; set; }
    public DbSet<SupplierEntity> Suppliers { get; set; }
    public DbSet<CategoryEntity> Categories { get; set; }
    public DbSet<ProductCategoryEntity> ProductCategories { get; set; }

    public ProductCatalogContext(DbContextOptions<ProductCatalogContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<ProductEntity>(entity =>
        {
            entity.ToTable("Products");

            entity.Property(e => e.ProductID)
                .HasColumnName("ProductID")
                .ValueGeneratedOnAdd();

            entity.Property(e => e.ProductName).IsRequired();
            entity.Property(e => e.Price).HasColumnType("decimal(18,2)");
            entity.Property(e => e.AvailableQuantity).IsRequired().HasColumnType("int");

            entity.Property(e => e.SupplierId).IsRequired();

            // Relationships
            entity.HasOne(e => e.Supplier)
                .WithMany(s => s.Products)
                .HasForeignKey(e => e.SupplierId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<OrderEntity>(entity =>
        {
            entity.ToTable("Orders");

            entity.Property(e => e.TotalAmount).HasColumnType("decimal(18,2)");
        });

        modelBuilder.Entity<ProductCategoryEntity>(entity =>
        {
            entity.ToTable("ProductCategories");

            entity.HasKey(pc => new { pc.ProductID, pc.CategoryID });

            entity.HasOne(pc => pc.Category)
                .WithMany(c => c.ProductCategories)
                .HasForeignKey(pc => pc.CategoryID)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(pc => pc.Product)
                .WithMany(p => p.ProductCategories)
                .HasForeignKey(pc => pc.ProductID)
                .OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<OrderDetailEntity>(entity =>
        {
            entity.HasKey(od => new { od.OrderID, od.ProductID });

            entity.Property(e => e.UnitPrice).HasColumnType("decimal(18,2)");

            // Relationships
            entity.HasOne(od => od.Product)
                .WithMany(p => p.OrderDetails)
                .HasForeignKey(od => od.ProductID)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(od => od.Order)
                .WithMany(o => o.OrderDetails)
                .HasForeignKey(od => od.OrderID)
                .OnDelete(DeleteBehavior.Restrict);
        });
    }
    public ProductEntity GetProductById(int productId)
    {
        return Products.FirstOrDefault(p => p.ProductID == productId);
    }

    public void AddProduct(ProductEntity product)
    {
        if (product == null)
        {
            throw new ArgumentNullException(nameof(product));
        }

        Products.Add(product);
        SaveChanges();
    }

    public void UpdateProduct(ProductEntity product)
    {
        if (product == null)
        {
            throw new ArgumentNullException(nameof(product));
        }

        Entry(product).State = EntityState.Modified;
        SaveChanges();
    }

    public void DeleteProduct(int productId)
    {
        var product = Products.FirstOrDefault(p => p.ProductID == productId);

        if (product != null)
        {
            Products.Remove(product);
            SaveChanges();
        }
    }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Projects\\Datalagring\\ConsoleApp\\ConsoleApp\\Data\\ProductCatalog.mdf;Integrated Security=True;Connect Timeout=30;Encrypt=True")
                .LogTo(Console.WriteLine, LogLevel.Information);
        }

}
