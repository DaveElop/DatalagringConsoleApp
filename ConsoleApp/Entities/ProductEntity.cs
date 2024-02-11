using ConsoleApp.Entities;
using System.ComponentModel.DataAnnotations;

public class ProductEntity
{
    [Key]
    public int ProductID { get; set; }
    public string ProductName { get; set; }
    public decimal Price { get; set; }
    public int AvailableQuantity { get; set; }
    public int SupplierId { get; set; }
    public int CategoryId { get; set; }
    public SupplierEntity Supplier { get; set; }
    public CategoryEntity Category { get; set; }
    public ICollection<OrderDetailEntity> OrderDetails { get; set; }
    public ICollection<ProductCategoryEntity> ProductCategories { get; set; }
}
