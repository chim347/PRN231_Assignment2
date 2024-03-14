using FlowerShopBusinessObject.DBContext;
using FlowerShopBusinessObject.Entities;
using FlowerShopDAO.Categories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlowerShopDAO.Customers
{
    public class CustomerDAO
    {
        private static CustomerDAO instance = null;
        private readonly ApplicationDBContext _dbContext = null;
        public CustomerDAO()
        {
            if (_dbContext == null) {
                _dbContext = new ApplicationDBContext();
            }
        }

        public static CustomerDAO Instance
        {
            get
            {
                if (instance == null) {
                    instance = new CustomerDAO();
                }
                return instance;
            }
        }

        public List<Account> GetAllCustomers()
        {
            var listAcc = new List<Account>();
            // 4. Customer
            try {
                listAcc = _dbContext.Accounts.Where(a => a.Role == 4).ToList();
            }
            catch (Exception ex) {
                throw new Exception(ex.Message);
            }
            return listAcc;
        }

        public Account FindCustomerByEmail(string email)
        {
            var acc = new Account();
            try {
                acc = _dbContext.Accounts.FirstOrDefault(c => c.EmailAddress == email && c.Role == 4);
            }
            catch (Exception ex) {
                throw new Exception(ex.Message);
            }
            return acc;
        }

        public Account FindCustomerById(string customerId)
        {
            var customer = new Account();
            try {
                customer = _dbContext.Accounts.SingleOrDefault(c => c.Id == Guid.Parse(customerId) && c.Role == 4);
            }
            catch (Exception ex) {
                throw new Exception(ex.Message);
            }
            return customer;
        }

        public List<Account> Search(string keyword)
        {
            var listCustomers = new List<Account>();
            try {
                listCustomers = _dbContext.Accounts
                    .Where(c => c.FullName.Contains(keyword))
                    .ToList();
            }
            catch (Exception e) {
                throw new Exception(e.Message);
            }
            return listCustomers;
        }

        public void UpdateCustomer(Account customer)
        {
            try {
                _dbContext.Entry(customer).State =
                        Microsoft.EntityFrameworkCore.EntityState.Modified;
                _dbContext.SaveChanges();
            }
            catch (Exception ex) {
                throw new Exception(ex.Message);
            }
        }

        public void DeleteCustomer(Account customer)
        {
            try {
                var customerToDelete = _dbContext
                    .Accounts
                    .SingleOrDefault(c => c.Id == customer.Id);
                _dbContext.Accounts.Remove(customerToDelete);
                _dbContext.SaveChanges();
            }
            catch (Exception ex) {
                throw new Exception(ex.Message);
            }
        }
    }
}
