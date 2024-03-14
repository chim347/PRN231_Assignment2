using System.ComponentModel.DataAnnotations;

namespace FlowerShopManagement.Models
{
    public class CreateOrder
    {
        [Required]
        public DateTime OrderDate { get; set; }
        [Required]
        public int Total { get; set; }
        [Required]
        public string Freight { get; set; }
        [Required]
        public string CustomerID { get; set; }
        [Required]
        public List<CreateOrderDetail> OrderDetails { get; set; }
    }
}
