using ConsoleApp.Entities;

namespace ConsoleApp.Services
{
    public class OrderService
    {
        private readonly ProductCatalogContext _dbContext;

        public OrderService(ProductCatalogContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void CreateOrder(int customerId, int productId, int quantity, decimal productPrice)
        {
            var order = new OrderEntity
            {
                CustomerID = customerId,
                OrderDate = DateTime.Now,

            };

            var orderItem = new OrderDetailEntity
            {
                Order = order,
                ProductID = productId,
                Quantity = quantity,
                UnitPrice = productPrice,
                Amount = quantity * productPrice
            };

            order.OrderDetails.Add(orderItem);

            order.TotalAmount = order.OrderDetails.Sum(item => item.Amount);

            _dbContext.Orders.Add(order);
            _dbContext.SaveChanges();
        }
    }
}