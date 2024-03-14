using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using FlowerShopBusinessObject.DBContext;
using FlowerShopBusinessObject.Entities;
using FlowerShopManagentWebClient.Pages.Inheritance;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

namespace FlowerShopManagentWebClient.Pages.CustomerArea.MyOrder
{
    public class CreateModel : ClientAbstract
    {
        public CreateModel(IHttpClientFactory http, IHttpContextAccessor httpContextAccessor) : base(http, httpContextAccessor)
        {
        }

        [BindProperty]
        public Order Order { get; set; } = default!;

        [BindProperty(SupportsGet = true)]
        public int Quantity { get; set; }
        public FlowerBouquet FlowerBouquet { get; set; } = default!;
        public IList<FlowerBouquet> CartItems { get; set; } = new List<FlowerBouquet>();

        public async Task<IActionResult> OnGetAsync()
        {
            await LoadFlowerBouquets();

            return Page();
        }

        /*public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            string token = _context.HttpContext.Session.GetString("token");
            HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            string url = $"api/v1/FlowerBouquet/{id}";
            HttpResponseMessage response = await HttpClient.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var flowerBouquet = JsonConvert.DeserializeObject<FlowerBouquet>(content);
                if (Request.Query.ContainsKey("quantity") && int.TryParse(Request.Query["quantity"], out int quantity))
                {
                    if (quantity < 1)
                    {
                        ModelState.AddModelError("Quantity", "Quantity must be greater than 0");
                        return Page();
                    }

                    if (_context.HttpContext.Session.TryGetValue("OrderDetails", out byte[] value))
                    {
                        var orderDetails = JsonConvert.DeserializeObject<List<FlowerBouquet>>(Encoding.UTF8.GetString(value));
                        orderDetails.Add(flowerBouquet);
                        _context.HttpContext.Session.Set("OrderDetails", Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(orderDetails)));
                    }
                    else
                    {
                        var orderDetails = new List<FlowerBouquet> { flowerBouquet };
                        _context.HttpContext.Session.Set("OrderDetails", Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(orderDetails)));
                    }
                }
                else
                {
                    if (_context.HttpContext.Session.TryGetValue("OrderDetails", out byte[] value))
                    {
                        var orderDetails = JsonConvert.DeserializeObject<List<FlowerBouquet>>(Encoding.UTF8.GetString(value));
                        orderDetails.Add(flowerBouquet);
                        _context.HttpContext.Session.Set("OrderDetails", Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(orderDetails)));
                    }
                    else
                    {
                        var orderDetails = new List<FlowerBouquet> { flowerBouquet };
                        _context.HttpContext.Session.Set("OrderDetails", Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(orderDetails)));
                    }
                }

            }
            return Page();
        }*/
        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (Quantity < 1)
            {
                ModelState.AddModelError("Quantity", "Quantity must be greater than 0");
                await LoadFlowerBouquets();
                return Page();
            }
            var selectedBouquetId = Request.Form["FlowerBouquet.Id"].ToString();
            if (!Guid.TryParse(selectedBouquetId, out Guid bouquetId))
            {
                ModelState.AddModelError("FlowerBouquet.Id", "Invalid Flower Bouquet ID");
                await LoadFlowerBouquets();
                return Page();
            }

            string token = _context.HttpContext.Session.GetString("token");
            HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            string url = $"api/v1/FlowerBouquet/{bouquetId}";
            HttpResponseMessage response = await HttpClient.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var flowerBouquet = JsonConvert.DeserializeObject<FlowerBouquet>(content);

                // Add FlowerBouquet to CartItems
                for (int i = 0; i < Quantity; i++)
                {
                    CartItems.Add(flowerBouquet);
                }

                TempData["SuccessMessage"] = $"Added {Quantity} {flowerBouquet.FlowerBouquetName}(s) to cart.";
            }
            else
            {
                TempData["ErrorMessage"] = "Failed to fetch Flower Bouquet.";
            }

            await LoadFlowerBouquets();
            return Page();
        }

        private async Task LoadFlowerBouquets()
        {
            string token = _context.HttpContext.Session.GetString("token");
            HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            HttpResponseMessage response = await HttpClient.GetAsync("api/v1/FlowerBouquet");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var listFlowerBouquetId = JsonConvert.DeserializeObject<List<FlowerBouquet>>(content);

                var selectListItems = listFlowerBouquetId.Select(fb => new SelectListItem
                {
                    Value = fb.Id.ToString(),
                    Text = $"{fb.FlowerBouquetName} - {fb.UnitsInStock}"
                }).ToList();
                selectListItems.Insert(0, new SelectListItem { Value = "", Text = "Choose..." });
                ViewData["Id"] = selectListItems;
            }
        }
    }
}
