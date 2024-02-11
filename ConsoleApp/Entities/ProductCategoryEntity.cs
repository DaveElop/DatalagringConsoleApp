using System.ComponentModel.DataAnnotations;

namespace ConsoleApp.Entities
{
    public class ProductCategoryEntity
    {
        [Key]
        public int ProductID { get; set; }
        public int CategoryID { get; set; }

        public ProductEntity Product { get; set; }
        public CategoryEntity Category { get; set; }
    }
}