using FlowerShopBusinessObject.Entities;
using System.ComponentModel.DataAnnotations;

namespace FlowerShopManagentWebClient.Models
{
    public class CartItems
    {
        public FlowerBouquet FlowerBouquet { get; set; } = default!;
        public int Quantity { get; set; }
    }

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

    public class CreateOrderDetail
    {
        [Required]
        public string FlowerBouquetID { get; set; }
        [Required]
        public int UnitPrice { get; set; }
        [Required]
        public int Quantity { get; set; }
    }
}
