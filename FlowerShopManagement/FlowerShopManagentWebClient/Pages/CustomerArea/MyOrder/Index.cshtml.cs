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

namespace FlowerShopManagentWebClient.Pages.CustomerArea.MyOrder
{
    public class IndexModel : ClientAbstract
    {
        public IndexModel(IHttpClientFactory http, IHttpContextAccessor httpContextAccessor) : base(http, httpContextAccessor)
        {
        }

        public IList<Order> Order { get;set; } = default!;

        public async Task<ActionResult> OnGetAsync()
        {
            string token = _context.HttpContext.Session.GetString("token");
            string userId = _context.HttpContext.Session.GetString("USERID"); 
            HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            HttpResponseMessage response = await HttpClient.GetAsync($"api/v1/order/customer/{userId}");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                Order = JsonConvert.DeserializeObject<List<Order>>(content);
            }
            return Page();
        }
    }
}
