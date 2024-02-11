using System.ComponentModel.DataAnnotations;

namespace ConsoleApp.Entities
{
    public class OrderEntity
    {
        [Key]
        public int OrderID { get; set; }
        public int CustomerID { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal TotalAmount { get; set; }

        public CustomerEntity Customer { get; set; }
        public ICollection<OrderDetailEntity> OrderDetails { get; set; }
    }
}