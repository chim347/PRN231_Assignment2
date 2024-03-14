using FlowerShopBusinessObject.DBContext;
using FlowerShopBusinessObject.Entities;
using FlowerShopDAO.Categories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlowerShopDAO.FlowerBoquets
{
    public class FlowerBouquetDAO
    {
        private static FlowerBouquetDAO instance = null;
        private readonly ApplicationDBContext _dbContext = null;

        public FlowerBouquetDAO()
        {
            if (_dbContext == null) {
                _dbContext = new ApplicationDBContext();
            }
        }

        public static FlowerBouquetDAO Instance
        {
            get
            {
                if (instance == null) {
                    instance = new FlowerBouquetDAO();
                }
                return instance;
            }
        }

        public List<FlowerBouquet> GetFlowerBouquets()
        {
            var listFlowerBouquets = new List<FlowerBouquet>();
            try {
                listFlowerBouquets = _dbContext.FlowerBouquet.ToList();
                listFlowerBouquets.ForEach(f =>
                {
                    f.Category = _dbContext.Categories.Find(f.CategoryID);
                    f.Supplier = _dbContext.Suppliers.Find(f.SupplierID);
                });
            }
            catch (Exception e) {
                throw new Exception(e.Message);
            }
            return listFlowerBouquets;
        }

        public List<FlowerBouquet> Search(string keyword)
        {
            var listFlowerBouquets = new List<FlowerBouquet>();
            try {
                listFlowerBouquets = _dbContext.FlowerBouquet.Where(f => f.FlowerBouquetName.Contains(keyword)).ToList();
                listFlowerBouquets.ForEach(f =>
                {
                    f.Category = _dbContext.Categories.Find(f.CategoryID);
                    f.Supplier = _dbContext.Suppliers.Find(f.SupplierID);
                });
            }
            catch (Exception e) {
                throw new Exception(e.Message);
            }
            return listFlowerBouquets;
        }

        public List<FlowerBouquet> FindAllFlowerBouquetsByCategoryId(string categoryId)
        {
            var listFlowerBouquets = new List<FlowerBouquet>();
            try {
                listFlowerBouquets = _dbContext.FlowerBouquet.Where(f => f.CategoryID == Guid.Parse(categoryId)).ToList();
            }
            catch (Exception e) {
                throw new Exception(e.Message);
            }
            return listFlowerBouquets;
        }

        public List<FlowerBouquet> FindAllFlowerBouquetsBySupplierId(string supplierId)
        {
            var listFlowerBouquets = new List<FlowerBouquet>();
            try {
                listFlowerBouquets = _dbContext.FlowerBouquet.Where(f => f.SupplierID == Guid.Parse(supplierId)).ToList();
            }
            catch (Exception e) {
                throw new Exception(e.Message);
            }
            return listFlowerBouquets;
        }

        public FlowerBouquet FindFlowerBouquetById(string flowerBouquetId)
        {
            var flowerBouquet = new FlowerBouquet();
            var string1 = flowerBouquetId;
            try {
                flowerBouquet = _dbContext.FlowerBouquet.SingleOrDefault(f => f.Id == Guid.Parse(flowerBouquetId));
                flowerBouquet.Category = _dbContext.Categories.Find(flowerBouquet.CategoryID);
                flowerBouquet.Supplier = _dbContext.Suppliers.Find(flowerBouquet.SupplierID);
                flowerBouquet.OrderDetails = _dbContext.OrderDetail.Where(o => o.FlowerBouquetID == Guid.Parse(flowerBouquetId)).ToList();
            }
            catch (Exception ex) {
                throw new Exception(ex.Message);
            }
            return flowerBouquet;
        }

        public void SaveFlowerBouquet(FlowerBouquet flowerBouquet)
        {
            try {
                _dbContext.FlowerBouquet.Add(flowerBouquet);
                _dbContext.SaveChanges();
            }
            catch (Exception ex) {
                throw new Exception(ex.Message);
            }
        }

        public void UpdateFlowerBouquet(FlowerBouquet flowerBouquet)
        {
            try {
                _dbContext.Entry(flowerBouquet).State =
                        Microsoft.EntityFrameworkCore.EntityState.Modified;
                _dbContext.SaveChanges();
            }
            catch (Exception ex) {
                throw new Exception(ex.Message);
            }
        }

        public void DeleteFlowerBouquet(FlowerBouquet flowerBouquet)
        {
            try {
                var flowerBouquetToDelete = _dbContext
                    .FlowerBouquet
                    .SingleOrDefault(f => f.Id == flowerBouquet.Id);
                _dbContext.FlowerBouquet.Remove(flowerBouquetToDelete);
                _dbContext.SaveChanges();
            }
            catch (Exception ex) {
                throw new Exception(ex.Message);
            }
        }
    }
}
