using FlowerShopBusinessObject.Entities;
using FlowerShopManagement.Models;
using FlowerShopRepository.Accounts;
using FlowerShopRepository.Categories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace FlowerShopManagement.Controllers.Categories
{
    [Authorize(Roles = "1")]
    [Route("api/v1/category")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private ICategoryRepository _categoryRepository;
        public CategoryController(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Category>> GetCategories() => _categoryRepository.GetCategories();

        [HttpPost]
        public IActionResult PostCategory(Category category)
        {
            _categoryRepository.SaveCategory(category);
            return NoContent();
        }
    }
}
