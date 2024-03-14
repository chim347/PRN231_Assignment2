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
using System.Net.Http.Headers;
using Newtonsoft.Json;
using System.Text;

namespace FlowerShopManagentWebClient.Pages.AdminAreea.FlowerBouquet
{
    public class CreateModel : ClientAbstract
    {
        public CreateModel(IHttpClientFactory http, IHttpContextAccessor httpContextAccessor) : base(http, httpContextAccessor)
        {
        }

        public async Task<IActionResult> OnGet()
        {
            if (!CheckAuthen())
            {
                return RedirectToPage("/Login");
            }
            string token = _context.HttpContext.Session.GetString("token");
            HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            HttpResponseMessage responseSupplier = await HttpClient.GetAsync("api/v1/supplier");
            HttpResponseMessage responseCategories = await HttpClient.GetAsync("api/v1/category");

            if (responseSupplier.IsSuccessStatusCode && responseCategories.IsSuccessStatusCode)
            {
                var contentSupplier = await responseSupplier.Content.ReadAsStringAsync();
                var contentCategory = await responseCategories.Content.ReadAsStringAsync();
                var tempSupplier = JsonConvert.DeserializeObject<List<Supplier>>(contentSupplier);
                var tempCategory = JsonConvert.DeserializeObject<List<Category>>(contentCategory);
                ViewData["CategoryID"] = new SelectList(tempCategory, "Id", "CategoryName");
                ViewData["SupplierID"] = new SelectList(tempSupplier, "Id", "SupplierName");
            }
            
            return Page();
        }

        [BindProperty]
        public FlowerShopBusinessObject.Entities.FlowerBouquet FlowerBouquet { get; set; } = default!;


        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            string token = _context.HttpContext.Session.GetString("token");
            HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            string url = "api/v1/FlowerBouquet";
            var jsonContent = JsonConvert.SerializeObject(FlowerBouquet);
            var httpContent = new StringContent(jsonContent, Encoding.UTF8, "application/json");
            var response = await HttpClient.PostAsync(url, httpContent);
            if (response.IsSuccessStatusCode)
            {
                return RedirectToPage("Index");
            }
            else
            {
                ViewData["Message"] = "Create Fail: " + await response.Content.ReadAsStringAsync();
                await OnGet();
                return Page();
            }
        }
    }
}
