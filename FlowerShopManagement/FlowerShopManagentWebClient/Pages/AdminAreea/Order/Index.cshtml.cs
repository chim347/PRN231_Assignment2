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
    public class IndexModel : PageModel
    {
        private readonly FlowerShopBusinessObject.DBContext.ApplicationDBContext _context;

        public IndexModel(FlowerShopBusinessObject.DBContext.ApplicationDBContext context)
        {
            _context = context;
        }

        public IList<FlowerShopBusinessObject.Entities.Order> Order { get;set; } = default!;    

        public async Task OnGetAsync()
        {
            if (_context.Order != null)
            {
                Order = await _context.Order
                .Include(o => o.Account).ToListAsync();
            }
        }
    }
}
