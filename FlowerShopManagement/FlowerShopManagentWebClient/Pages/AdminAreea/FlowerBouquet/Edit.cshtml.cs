using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FlowerShopBusinessObject.DBContext;
using FlowerShopBusinessObject.Entities;
using FlowerShopManagentWebClient.Pages.Inheritance;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using System.Text;

namespace FlowerShopManagentWebClient.Pages.AdminAreea.FlowerBouquet
{
    public class EditModel : ClientAbstract
    {
        public EditModel(IHttpClientFactory http, IHttpContextAccessor httpContextAccessor) : base(http, httpContextAccessor)
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
            // LOAD ADDITION
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

            // LOAD ENTITY
            HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            string url = $"api/v1/FlowerBouquet/{id}";
            HttpResponseMessage response = await HttpClient.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                FlowerBouquet = JsonConvert.DeserializeObject<FlowerShopBusinessObject.Entities.FlowerBouquet>(content);
            }

            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            string token = _context.HttpContext.Session.GetString("token");
            HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            string url = $"api/v1/FlowerBouquet/{FlowerBouquet.Id}";
            var jsonContent = JsonConvert.SerializeObject(FlowerBouquet);
            var httpContent = new StringContent(jsonContent, Encoding.UTF8, "application/json");
            var response = await HttpClient.PutAsync(url, httpContent);
            if (response.IsSuccessStatusCode)
            {
                return RedirectToPage("Index");
            }
            else
            {
                ViewData["Message"] = "Update Fail: " + await response.Content.ReadAsStringAsync();
                await OnGetAsync(FlowerBouquet.Id);
                return Page();
            }
        }
    }
}
