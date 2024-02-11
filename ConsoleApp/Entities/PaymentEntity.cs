using System.ComponentModel.DataAnnotations;

namespace ConsoleApp.Entities
{
    public class PaymentEntity
    {
        [Key]
        public int Id { get; set; }
        public decimal Amount { get; set; }
        public DateTime PaymentDate { get; set; }

        // Foreign Key
        public int OrderId { get; set; }

        // Navigation property for the order relationship
        public OrderEntity Order { get; set; }
    }
}