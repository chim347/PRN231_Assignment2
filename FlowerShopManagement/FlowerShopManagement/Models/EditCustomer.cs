using System.ComponentModel.DataAnnotations;

namespace FlowerShopManagement.Models
{
    public class EditCustomer
    {
        [Required]
        public string CustomerName { get; set; }
        public string? AccountPassword { get; set; }
    }
}
