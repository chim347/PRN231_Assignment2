using FlowerShopBusinessObject.Entities;
using FlowerShopManagement.Models;
using FlowerShopRepository.Accounts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace FlowerShopManagement.Controllers.Auth
{
    [Route("api/v1/auth")]
    [ApiController]
    public class AuthsController : ControllerBase
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IConfiguration _configuration;
        private readonly JwtSettings _jwtSettings;
        public AuthsController(IAccountRepository accountRepository, IConfiguration configuration, IOptions<JwtSettings> jwtSettings)
        {
            _accountRepository = accountRepository;
            _configuration = configuration;
            _jwtSettings = jwtSettings.Value;
        }

        [HttpPost("login")]
        public IActionResult Login(string email, string password)
        {
            // Admin
            /*if (model.Email.Equals(_configuration["Credentials:Email"])) {
                var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Email, _configuration["Credentials:Email"]),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(ClaimTypes.Role, "Admin")
                };

                var token = GetToken(authClaims);

                return Ok(new JwtSecurityTokenHandler().WriteToken(token));
            }
            var user = _accountRepository.GetAccountByEmail(model.Email);
            if (user != null) {

                // 2. staff
                // 3. manager
                // 4. customer
                var role = user.Role;
                var claimRole = "";
                switch (role) {
                    case 2:
                        claimRole = "Staff";
                        break;
                    case 3:
                        claimRole = "Manager";
                        break;
                    case 4:
                        claimRole = "Customer";
                        break;
                    default:
                        break;
                }
                var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Email, user.EmailAddress),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(ClaimTypes.Role, claimRole)
                };

                var token = GetToken(authClaims);

                return Ok(new JwtSecurityTokenHandler().WriteToken(token));
            }
            return Unauthorized("Email or password wrong, please try login!");*/
            var user = _accountRepository.Login(email, password);
            if (user != null) {
                var token = GetToken(user);
                return Ok(token);
            }
            return Unauthorized("Email or password wrong, please try login!");
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] RegisterModel model)
        {
            var userExists = _accountRepository.GetAccountByEmail(model.Email);
            if (model.Email.Equals(_configuration["Credentials:Email"]) || userExists != null)
                return StatusCode(StatusCodes.Status400BadRequest, new Response { Status = "Error", Message = "User already exists!" });

            Account user = new Account
            {
                EmailAddress = model.Email,
                FullName = model.FullName,
                AccountPassword = model.AccountPassword,
                Role = 4
            };
            _accountRepository.SaveAccount(user);
            return StatusCode(StatusCodes.Status201Created, new Response { Status = "Success", Message = "User created successfully!" });
        }

        private string GetToken(Account user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key));
            var secretKey = _configuration["JwtSettings:Key"];
            if (secretKey == null) {
                return "Not Found SecretKey";
            }

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Issuer = _jwtSettings.Issuer,
                Audience = _jwtSettings.Audience,
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim("Id", user.Id.ToString()),
                    new Claim(ClaimTypes.Role, user.Role.ToString()),
                }),
                Expires = DateTime.Now.AddHours(3),
                SigningCredentials = new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);

            return tokenString;
        }
    }
}
