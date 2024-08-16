using OrderService.Model;

namespace OrderService.Repository
{
    /// <summary>
    /// Order Interface
    /// </summary>
    public interface IOrder
    {
        Task<List<OrderDetails>> GetAllOrders();
        Task<OrderDetails> GetOrdersById(int id);
        Task<bool> AddOrders(OrderDetails orderDetails);
    }
}
