using FlowerShopBusinessObject.Entities;
using FlowerShopDAO.Accounts;
using FlowerShopDAO.Categories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlowerShopRepository.Categories
{
    public class CategoryRepository : ICategoryRepository
    {
        public readonly CategoryDAO _categoryDAO;
        public CategoryRepository()
        {
            _categoryDAO = new CategoryDAO();
        }
        public List<Category> GetCategories()
        {
            return _categoryDAO.GetCategories();
        }

        public void SaveCategory(Category category)
        {
            _categoryDAO.SaveCategory(category);
        }
    }
}
