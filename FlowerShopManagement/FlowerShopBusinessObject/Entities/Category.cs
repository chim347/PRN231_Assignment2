using FlowerShopBusinessObject.Common;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace FlowerShopBusinessObject.Entities
{
    public class Category : BaseAuditableEntity
    {
        [Required, StringLength(40)]
        public string CategoryName { get; set; }
        [Required, StringLength(255)]
        public string Description { get; set; }

        [JsonIgnore]
        public virtual ICollection<FlowerBouquet> FlowerBouquets { get; set; }
    }
}
