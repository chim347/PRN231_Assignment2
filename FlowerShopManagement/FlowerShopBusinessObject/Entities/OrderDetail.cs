using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FlowerShopBusinessObject.Entities
{
    public class OrderDetail
    {
        [ForeignKey("Order")]
        public Guid OrderID { get; set; }
        [ForeignKey("FlowerBouquet")]
        public Guid FlowerBouquetID { get; set; }
        [Required]
        public int UnitPrice { get; set; }
        [Required]
        public int Quantity { get; set; }
        [Required]
        public int Discount { get; set; }

        public DateTime Created { get; set; } = DateTime.Now;

        public string? CreateBy { get; set; }

        public DateTime? LastModified { get; set; }

        public string? LastModifiedBy { get; set; }

        public bool IsDeleted { get; set; } = false;

        public virtual Order Order { get; set; }
        public virtual FlowerBouquet FlowerBouquet { get; set; }
    }
}
