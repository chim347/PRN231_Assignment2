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
using System.Text;

namespace FlowerShopManagentWebClient.Pages.AdminAreea.Order
{
    public class IndexModel : ClientAbstract
    {
        public IndexModel(IHttpClientFactory http, IHttpContextAccessor httpContextAccessor) : base(http, httpContextAccessor)
        {
        }

        public IList<FlowerShopBusinessObject.Entities.Order> Orders { get;set; } = default!;
        public FlowerShopBusinessObject.Entities.Order Order { get;set; } = default!;

        public async Task<ActionResult> OnGetAsync()
        {
            string token = _context.HttpContext.Session.GetString("token");
            string userId = _context.HttpContext.Session.GetString("USERID");
            HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            HttpResponseMessage response = await HttpClient.GetAsync($"api/v1/order");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                Orders = JsonConvert.DeserializeObject<List<FlowerShopBusinessObject.Entities.Order>>(content);
            }
            return Page();
        }

        public async Task<IActionResult> OnPostShipAsync(string id)
        {
            string token = _context.HttpContext.Session.GetString("token");
            HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            string url = $"api/v1/order/shipped/{id}";
            /*var jsonContent = JsonConvert.SerializeObject(Order);
            var httpContent = new StringContent(jsonContent, Encoding.UTF8, "application/json");*/
            var response = await HttpClient.PutAsync(url, null);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToPage("Index");
            }

            return Page();
        }

        public async Task<IActionResult> OnPostCancelAsync(string id)
        {
            string token = _context.HttpContext.Session.GetString("token");
            HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            string url = $"api/v1/order/cancel/{id}";

            var response = await HttpClient.PutAsync(url, null);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToPage("Index");
            }

            return Page();
        }
    }
}
