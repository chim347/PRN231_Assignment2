using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using FlowerShopBusinessObject.DBContext;
using FlowerShopBusinessObject.Entities;
using FlowerShopManagentWebClient.Pages.Inheritance;
using System.Net.Http.Headers;
using Newtonsoft.Json;

namespace FlowerShopManagentWebClient.Pages.AdminAreea.FlowerBouquet
{
    public class DeleteModel : ClientAbstract
    {
        public DeleteModel(IHttpClientFactory http, IHttpContextAccessor httpContextAccessor) : base(http, httpContextAccessor)
        {
        }

        [BindProperty]
        public FlowerShopBusinessObject.Entities.FlowerBouquet FlowerBouquet { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (!CheckAuthen())
            {
                return RedirectToPage("/Login");
            }
            string token = _context.HttpContext.Session.GetString("token");

            HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            string url = $"api/v1/FlowerBouquet/{id}";
            HttpResponseMessage response = await HttpClient.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                FlowerBouquet = JsonConvert.DeserializeObject<FlowerShopBusinessObject.Entities.FlowerBouquet>(content);
            }
            else
            {
                ViewData["Message"] = "Error: " + await response.Content.ReadAsStringAsync();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(Guid? id)
        {
            string token = _context.HttpContext.Session.GetString("token");
            HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            string url = $"api/v1/FlowerBouquet/{FlowerBouquet.Id}";
            HttpResponseMessage response = await HttpClient.DeleteAsync(url);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToPage("Index");
            }
            else
            {
                ViewData["Message"] = await response.Content.ReadAsStringAsync();
                return Page();
            }
        }
    }
}
