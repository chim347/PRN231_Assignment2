using FlowerShopBusinessObject.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FlowerShopBusinessObject.Entities
{
    public class Order : BaseAuditableEntity
    {
        public DateTime OrderDate { get; set; }
        public DateTime? ShippedDate { get; set; }
        [Required]
        public int Total { get; set; }
        [Required]
        public int OrderStatus { get; set; }
        [Required]
        public string Freight { get; set; }
        [ForeignKey("Account")]
        public Guid AccountID { get; set; }

        public virtual Account Account { get; set; }
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
