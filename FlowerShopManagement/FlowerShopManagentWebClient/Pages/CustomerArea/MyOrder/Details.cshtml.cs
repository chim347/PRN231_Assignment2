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
    public class DetailsModel : ClientAbstract
    {
        public DetailsModel(IHttpClientFactory http, IHttpContextAccessor httpContextAccessor) : base(http, httpContextAccessor)
        {
        }

        public Order Order { get; set; } = default!;
        public IList<OrderDetail> OrderDetail { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            string token = _context.HttpContext.Session.GetString("token");
            string userId = _context.HttpContext.Session.GetString("USERID");
            HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            HttpResponseMessage response = await HttpClient.GetAsync($"api/v1/order/customer/detail/{id}");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                Order = JsonConvert.DeserializeObject<Order>(content);
                if(Order != null)
                {
                    OrderDetail = Order.OrderDetails.ToList();
                }
            }
            return Page();
        }
    }
}
