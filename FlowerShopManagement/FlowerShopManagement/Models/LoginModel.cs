using System.ComponentModel.DataAnnotations;

namespace FlowerShopManagement.Models
{
    public class LoginModel
    {
        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
