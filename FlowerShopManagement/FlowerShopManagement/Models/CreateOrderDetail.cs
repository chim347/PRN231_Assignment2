using System.ComponentModel.DataAnnotations;

namespace FlowerShopManagement.Models
{
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
