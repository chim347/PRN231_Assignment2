using FlowerShopManagentWebClient.Pages.Inheritance;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Text.Json;

namespace FlowerShopManagentWebClient.Pages
{
    public class LoginModel : ClientAbstract
    {
        public LoginModel(IHttpClientFactory http, IHttpContextAccessor httpContextAccessor) : base(http, httpContextAccessor)
        {
        }

        [Required]
        [BindProperty]
        public string email { get; set; }
        [BindProperty]
        [Required]
        [DataType(DataType.Password)]
        public string password { get; set; }

        public string ReturnUrl { get; set; }
        [BindProperty]
        public InputModel Input { get; set; }
        public class InputModel
        {
            [Display(Name = "Remember me?")]
            public bool RememberMe { get; set; }
        }
        public async Task<IActionResult> OnPostAsync()
        {
            string url = "api/v1/auth/login";

            var queryParameter = new Dictionary<string, string>
            {
                {"email", email},
                {"password", password}
            };
            string queryString = string.Join("&", queryParameter.Select(x => $"{x.Key}={x.Value}"));
            url = $"{url}?{queryString}";
            HttpResponseMessage respone = await HttpClient.PostAsync(url, null);
            if (respone.IsSuccessStatusCode) {
                var content = await respone.Content.ReadAsStringAsync();

                string[] tokenParts = content.Split('.');
                string payloadBase64 = tokenParts[1];

                int paddingLength = payloadBase64.Length % 4;
                if (paddingLength > 0) {
                    payloadBase64 += new string('=', 4 - paddingLength);
                }

                byte[] payloadBytes = Convert.FromBase64String(payloadBase64);
                string payloadJson = Encoding.UTF8.GetString(payloadBytes);

                JsonDocument payload = JsonDocument.Parse(payloadJson);

                // Truy cập các trường trong payload
                string role = payload.RootElement.GetProperty("role").GetString();
                string userId = payload.RootElement.GetProperty("Id").GetString();
                _context.HttpContext.Session.SetString("role", role);
                if (role.Equals("1")) {
                    _context.HttpContext.Session.SetString("token", content);
                    _context.HttpContext.Session.SetString("USERID", userId);
                    return RedirectToPage("/Manage/AdminPage");
                }
                if (role.Equals("4"))
                {
                    _context.HttpContext.Session.SetString("token", content);
                    _context.HttpContext.Session.SetString("USERID", userId);
                    return RedirectToPage("/CustomerArea/MyOrder/Index");
                }
                else {
                    ViewData["Message"] = "You do not have permission to do this function, only Manager!";
                    return Page();
                }
            }
            ViewData["Message"] = "Wrong email or password!";
            return Page();
        }
        public IActionResult OnPostLogOut()
        {
            HttpContext.Session.Clear();
            return RedirectToPage("/Login");
        }
    }
}
