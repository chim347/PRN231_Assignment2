using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using FlowerShopBusinessObject.DBContext;
using FlowerShopBusinessObject.Entities;

namespace FlowerShopManagentWebClient.Pages.AdminAreea.Order
{
    public class CreateModel : PageModel
    {
        private readonly FlowerShopBusinessObject.DBContext.ApplicationDBContext _context;

        public CreateModel(FlowerShopBusinessObject.DBContext.ApplicationDBContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
        ViewData["AccountID"] = new SelectList(_context.Accounts, "Id", "AccountPassword");
            return Page();
        }

        [BindProperty]
        public FlowerShopBusinessObject.Entities.Order Order { get; set; } = default!;
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
          if (!ModelState.IsValid || _context.Order == null || Order == null)
            {
                return Page();
            }

            _context.Order.Add(Order);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
