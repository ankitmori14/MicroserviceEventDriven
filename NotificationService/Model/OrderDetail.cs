using System.ComponentModel.DataAnnotations;

namespace NotificationService.Model
{
    /// <summary>
    /// order details Model
    /// </summary>
    public class OrderDetail
    {
        public int id { get; set; }
        public string name { get; set; }

        public string description { get; set; }

        public int price { get; set; }

        public int quantity { get; set; }

    }
}
