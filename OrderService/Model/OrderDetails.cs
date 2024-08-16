using System.ComponentModel.DataAnnotations;

namespace OrderService.Model
{
    /// <summary>
    /// Order details model
    /// </summary>
    public class OrderDetails
    {
        public int id { get; set; }
        [Required]
        public string name { get; set; }

        [Required]
        public string description { get; set; }

        [Required]
        [RegularExpression("([0-9]+)", ErrorMessage = "Please enter valid Number")]
        public int price { get; set; }

        [Required]
        [RegularExpression("([0-9]+)", ErrorMessage = "Please enter valid Number")]
        public int quantity { get; set; }
    }
}
