using FlowerShopBusinessObject.Common;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace FlowerShopBusinessObject.Entities
{
    public class Supplier : BaseAuditableEntity
    {
        [Required, StringLength(40)]
        public string SupplierName { get; set; }
        [Required, StringLength(255)]
        public string SupplierAddress { get; set; }
        [Required, StringLength(12)]
        public string Telephone { get; set; }

        [JsonIgnore]
        public virtual ICollection<FlowerBouquet> FlowerBouquets { get; set; }
    }
}
