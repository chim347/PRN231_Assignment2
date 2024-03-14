using FlowerShopBusinessObject.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlowerShopRepository.Categories
{
    public interface ICategoryRepository
    {
        List<Category> GetCategories();
        void SaveCategory(Category category);
    }
}
