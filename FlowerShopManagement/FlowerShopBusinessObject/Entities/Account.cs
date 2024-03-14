using FlowerShopBusinessObject.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlowerShopBusinessObject.Entities
{
    public class Account : BaseAuditableEntity
    {
        public string AccountPassword { get; set; } = null!;

        public string FullName { get; set; } = null!;

        public string? EmailAddress { get; set; }

        public int? Role { get; set; }

        public virtual ICollection<Order> Orders { get; set; }
    }
}
