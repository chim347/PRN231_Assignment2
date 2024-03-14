using System.ComponentModel.DataAnnotations;

namespace FlowerShopManagement.Models
{
    public class RegisterModel
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string FullName { get; set; }
        [Required]
        public string AccountPassword { get; set; }
    }
}
