using FlowerShopBusinessObject.DBContext;
using FlowerShopBusinessObject.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlowerShopDAO.Accounts
{
    public class AccountDAO
    {
        private static AccountDAO instance = null;
        private readonly ApplicationDBContext _dbContext = null;
        public AccountDAO()
        {
            if (_dbContext == null) {
                _dbContext = new ApplicationDBContext();
            }
        }

        public static AccountDAO Instance
        {
            get
            {
                if (instance == null) {
                    instance = new AccountDAO();
                }
                return instance;
            }
        }

        public List<Account> GetAllAccounts()
        {
            var listAcc = new List<Account>();
            try {
                listAcc = _dbContext.Accounts.ToList();

            }
            catch (Exception ex) {
                throw new Exception(ex.Message);
            }
            return listAcc;
        }

        public Account FindAccountByEmail(string email)
        {
            var acc = new Account();
            try {
                acc = _dbContext.Accounts.FirstOrDefault(c => c.EmailAddress == email);
            }
            catch (Exception ex) {
                throw new Exception(ex.Message);
            }
            return acc;
        }

        public Account Login(string email, string password)
        {
            var acc = new Account();
            try {
                acc = _dbContext.Accounts.FirstOrDefault(c => c.EmailAddress == email && c.AccountPassword == password);
            }
            catch (Exception ex) {
                throw new Exception(ex.Message);
            }
            return acc;
        }

        public void SaveAccount(Account account)
        {
            try {
                _dbContext.Accounts.Add(account);
                _dbContext.SaveChanges();
            }
            catch (Exception ex) {
                throw new Exception(ex.Message);
            }
        }
    }
}
