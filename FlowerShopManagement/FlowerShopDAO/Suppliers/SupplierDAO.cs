using FlowerShopBusinessObject.DBContext;
using FlowerShopBusinessObject.Entities;
using FlowerShopDAO.FlowerBoquets;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlowerShopDAO.Suppliers
{
    public class SupplierDAO
    {
        private static SupplierDAO instance = null;
        private readonly ApplicationDBContext _dbContext = null;

        public SupplierDAO()
        {
            if (_dbContext == null)
            {
                _dbContext = new ApplicationDBContext();
            }
        }

        public static SupplierDAO Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new SupplierDAO();
                }
                return instance;
            }
        }
        public List<Supplier> GetSuppliers()
        {
            var listSuppliers = new List<Supplier>();
            try
            {
                listSuppliers = _dbContext.Suppliers.ToList();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return listSuppliers;
        }

        public Supplier FindSupplierById(string supplierId)
        {
            var supplier = new Supplier();
            try
            {
                supplier = _dbContext.Suppliers.SingleOrDefault(s => s.Id == Guid.Parse(supplierId));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return supplier;
        }

        public void SaveSupplier(Supplier supplier)
        {
            try
            {
                _dbContext.Suppliers.Add(supplier);
                _dbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void UpdateSupplier(Supplier supplier)
        {
            try
            {
                _dbContext.Entry(supplier).State =
                        Microsoft.EntityFrameworkCore.EntityState.Modified;
                _dbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void DeleteSupplier(Supplier supplier)
        {
            try
            {
                var supplierToDelete = _dbContext
                    .Suppliers
                    .SingleOrDefault(s => s.Id == supplier.Id);
                _dbContext.Suppliers.Remove(supplierToDelete);
                _dbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
