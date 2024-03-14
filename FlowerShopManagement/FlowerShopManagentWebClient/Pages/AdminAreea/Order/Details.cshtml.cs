using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using FlowerShopBusinessObject.DBContext;
using FlowerShopBusinessObject.Entities;

namespace FlowerShopManagentWebClient.Pages.AdminAreea.Order
{
    public class DetailsModel : PageModel
    {
        private readonly FlowerShopBusinessObject.DBContext.ApplicationDBContext _context;

        public DetailsModel(FlowerShopBusinessObject.DBContext.ApplicationDBContext context)
        {
            _context = context;
        }

      public FlowerShopBusinessObject.Entities.Order Order { get; set; } = default!; 

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null || _context.Order == null)
            {
                return NotFound();
            }

            var order = await _context.Order.FirstOrDefaultAsync(m => m.Id == id);
            if (order == null)
            {
                return NotFound();
            }
            else 
            {
                Order = order;
            }
            return Page();
        }
    }
}
