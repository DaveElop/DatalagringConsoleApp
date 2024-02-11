using System.ComponentModel.DataAnnotations;

namespace ConsoleApp.Entities
{
    public class CategoryEntity
    {
        [Key]
        public int CategoryID { get; set; }
        public string CategoryName { get; set; }

        public ICollection<ProductCategoryEntity> ProductCategories { get; set; }
        public ICollection<ProductEntity> Products { get; set; }
    }
}