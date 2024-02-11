using System.ComponentModel.DataAnnotations;

namespace ConsoleApp.Entities
{
    public class SupplierEntity
    {
        [Key]
        public int SupplierID { get; set; }
        public string SupplierName { get; set; }
        public string ContactName { get; set; }
        public string ContactEmail { get; set; }

        public ICollection<ProductEntity> Products { get; set; }
    }
}