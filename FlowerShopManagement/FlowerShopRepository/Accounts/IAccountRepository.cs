using FlowerShopBusinessObject.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlowerShopRepository.Accounts
{
    public interface IAccountRepository
    {
        Account GetAccountByEmail(string email);
        Account Login(string email, string password);
        List<Account> GetAccounts();
        void SaveAccount(Account account);
    }
}
