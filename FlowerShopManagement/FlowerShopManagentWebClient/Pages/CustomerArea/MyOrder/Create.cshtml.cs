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
using FlowerShopManagentWebClient.Models;
using NuGet.Packaging;
using Microsoft.AspNetCore.Http;

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
        public IList<CartItems> CartItems { get; set; } = new List<CartItems>();
        [BindProperty]
        public string Freight { get; set; }
        public CreateOrder CreateOrder { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync()
        {
            await LoadFlowerBouquets();
            CartItems = GetCartItemsFromSession();
            return Page();
        }
        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            string action = Request.Form["action"];
            if (action == "AddToOrder")
            {
                if (Quantity < 1)
                {
                    ModelState.AddModelError("Quantity", "Quantity must be greater than 0");
                    await LoadFlowerBouquets();
                    CartItems = GetCartItemsFromSession();
                    return Page();
                }
                var selectedBouquetId = Request.Form["FlowerBouquet.Id"].ToString();
                if (!Guid.TryParse(selectedBouquetId, out Guid bouquetId))
                {
                    ModelState.AddModelError("FlowerBouquet.Id", "Invalid Flower Bouquet ID");
                    await LoadFlowerBouquets();
                    CartItems = GetCartItemsFromSession();
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

                    CartItems = GetCartItemsFromSession();

                    // Add FlowerBouquet to CartItems
                    // check giỏ hàng
                    var existingCartItem = CartItems.FirstOrDefault(item => item.FlowerBouquet.Id == flowerBouquet.Id);
                    if (existingCartItem != null)
                    {
                        if (existingCartItem.Quantity + Quantity > flowerBouquet.UnitsInStock)
                        {
                            TempData["ErrorMessage"] = "Quantity must be less than or equal to units in stock";
                            await LoadFlowerBouquets();
                            CartItems = GetCartItemsFromSession();
                            return Page();
                        }
                        else
                        {
                            // count lên
                            existingCartItem.Quantity += Quantity;
                        }
                    }
                    else
                    {
                        var cart = new CartItems { FlowerBouquet = flowerBouquet, Quantity = Quantity };
                        CartItems.Add(cart);
                    }
                    SaveCartItemsToSession(CartItems);

                    TempData["SuccessMessage"] = $"Added {Quantity} {flowerBouquet.FlowerBouquetName}(s) to cart.";
                }
                else
                {
                    TempData["ErrorMessage"] = "Failed to fetch Flower Bouquet.";
                }

                await LoadFlowerBouquets();

                
            }
            else if (action == "CreateOrder")
            {
                string token = _context.HttpContext.Session.GetString("token");
                string userId = _context.HttpContext.Session.GetString("USERID");
                HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                CartItems = GetCartItemsFromSession();
                var cartItems = CartItems;
                var freight = Freight;
                var order = new CreateOrder
                {
                    OrderDate = DateTime.Now,
                    Total = CalculateTotal(cartItems.ToList()),
                    Freight = freight,
                    CustomerID = userId,
                    OrderDetails = cartItems.Select(item => new CreateOrderDetail
                    {
                        FlowerBouquetID = item.FlowerBouquet.Id.ToString(),
                        Quantity = item.Quantity,
                        UnitPrice = item.FlowerBouquet.UnitPrice
                    }).ToList()
                };


                string url = "api/v1/order";
                var jsonContent = JsonConvert.SerializeObject(order);
                var httpContent = new StringContent(jsonContent, Encoding.UTF8, "application/json");
                var response = await HttpClient.PostAsync(url, httpContent);
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToPage("Index");
                }
                else
                {
                    ViewData["Message"] = "Create Fail: " + await response.Content.ReadAsStringAsync();
                    await OnGetAsync();
                }
            }
            return Page();
        }

        private int CalculateTotal(List<CartItems> cartItems)
        {
            int total = 0;
            foreach (var item in cartItems)
            {
                total += item.Quantity * item.FlowerBouquet.UnitPrice;
            }
            return total;
        }

        private IList<CartItems> GetCartItemsFromSession()
        {
            var json = _context.HttpContext.Session.GetString("CartItems");
            return string.IsNullOrEmpty(json) ? new List<CartItems>() : JsonConvert.DeserializeObject<List<CartItems>>(json);
        }

        private void SaveCartItemsToSession(IList<CartItems> cartItems)
        {
            var json = JsonConvert.SerializeObject(cartItems);
            _context.HttpContext.Session.SetString("CartItems", json);
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
