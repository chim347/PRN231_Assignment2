using FlowerShopBusinessObject.Entities;
using FlowerShopDAO.Accounts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlowerShopRepository.Accounts
{
    public class AccountRepository : IAccountRepository
    {
        public readonly AccountDAO _accountDAO;
        public AccountRepository()
        {
            _accountDAO = new AccountDAO();
        }
        public Account GetAccountByEmail(string email)
        {
            return _accountDAO.FindAccountByEmail(email);
        }

        public Account Login(string email, string password)
        {
            return _accountDAO.Login(email, password);
        }

        public List<Account> GetAccounts()
        {
            return _accountDAO.GetAllAccounts();
        }

        public void SaveAccount(Account account)
        {
            _accountDAO.SaveAccount(account);
        }
    }
}
