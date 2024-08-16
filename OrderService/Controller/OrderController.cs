using Microsoft.AspNetCore.Mvc;
using OrderService.Model;
using OrderService.Repository;

namespace OrderService.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        /// <summary>
        /// Declare Order interface
        /// </summary>
        private readonly IOrder _iorder;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="iorder"></param>
        public OrderController(IOrder iorder)
        {
            _iorder = iorder;
        }

        /// <summary>
        /// Get all orders details
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("GetAllOrders")]
        public async Task<ActionResult> GetAllOrders()
        {
            return Ok(await _iorder.GetAllOrders());
        }


        /// <summary>
        /// Get orders details by Id
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetOrdersById/{orderId}")]
        public async Task<ActionResult> GetOrdersById(int orderId)
        {
            return Ok(await _iorder.GetOrdersById(orderId));
        }


        /// <summary>
        /// Add new order details and publish into rabbitMq
        /// </summary>
        /// <param name="orderDetails"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("AddOrders")]
        public async Task<ActionResult> AddOrders(OrderDetails orderDetails)
        {
            if (ModelState.IsValid)
            {
                return Ok(await _iorder.AddOrders(orderDetails));
            }
            else
            {
              
                return BadRequest(ModelState);
            }
        }
    }
}
