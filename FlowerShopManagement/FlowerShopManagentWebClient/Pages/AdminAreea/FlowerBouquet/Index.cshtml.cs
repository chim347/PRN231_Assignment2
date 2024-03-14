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
using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace FlowerShopManagentWebClient.Pages.AdminAreea.FlowerBouquet
{
    public class IndexModel : ClientAbstract
    {
        public IndexModel(IHttpClientFactory http, IHttpContextAccessor httpContextAccessor) : base(http, httpContextAccessor)
        {
        }

        public IList<FlowerShopBusinessObject.Entities.FlowerBouquet> FlowerBouquets { get;set; } = default!;
        [BindProperty]
        public string searchString { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            if (!CheckAuthen())
            {
                return RedirectToPage("/Login");
            }
            string token = _context.HttpContext.Session.GetString("token");
            HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            HttpResponseMessage response = await HttpClient.GetAsync("api/v1/FlowerBouquet");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                FlowerBouquets = JsonConvert.DeserializeObject<List<FlowerShopBusinessObject.Entities.FlowerBouquet>>(content);
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            string token = _context.HttpContext.Session.GetString("token");
            HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            string url = $"api/v1/FlowerBouquet/Search?searchString={searchString}";
            HttpResponseMessage response = await HttpClient.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                FlowerBouquets = JsonConvert.DeserializeObject<List<FlowerShopBusinessObject.Entities.FlowerBouquet>>(content);
                return Page();
            }
            ViewData["Message"] = "FlowerBouquet don't exits!";
            await OnGetAsync();
            return Page();
        }
        public async Task OnPostResetAsync()
        {
            await OnGetAsync();
        }
    }
}
