using FlowerShopBusinessObject.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlowerShopRepository.Customers
{
    public interface ICustomerRepository
    {
        List<Account> GetCustomers();
        List<Account> Search(string keyword);
        void UpdateCustomer(Account customer);
        void DeleteCustomer(Account customer);
        Account GetCustomerByEmail(string email);
        Account GetCustomerById(string id);
    }
}
