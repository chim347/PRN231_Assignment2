using FlowerShopBusinessObject.DBContext;
using FlowerShopBusinessObject.Entities;
using FlowerShopDAO.Accounts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlowerShopDAO.Categories
{
    public class CategoryDAO
    {
        private static CategoryDAO instance = null;
        private readonly ApplicationDBContext _dbContext = null;
        public CategoryDAO()
        {
            if (_dbContext == null) {
                _dbContext = new ApplicationDBContext();
            }
        }

        public static CategoryDAO Instance
        {
            get
            {
                if (instance == null) {
                    instance = new CategoryDAO();
                }
                return instance;
            }
        }

        public List<Category> GetCategories()
        {
            var listCategories = new List<Category>();
            try {
                listCategories = _dbContext.Categories.ToList();

            }
            catch (Exception ex) {
                throw new Exception(ex.Message);
            }
            return listCategories;
        }

        public void SaveCategory(Category category)
        {
            try {
                _dbContext.Categories.Add(category);
                _dbContext.SaveChanges();
            }
            catch (Exception ex) {
                throw new Exception(ex.Message);
            }
        }
    }
}
