using FlowerShopBusinessObject.Entities;
using FlowerShopDAO.Categories;
using FlowerShopDAO.Customers;

namespace FlowerShopRepository.Customers
{
    public class CustomerRepository : ICustomerRepository
    {
        public readonly CustomerDAO _customerDAO;
        public CustomerRepository()
        {
            _customerDAO = new CustomerDAO();
        }
        public void DeleteCustomer(Account customer)
        {
            _customerDAO.DeleteCustomer(customer);
        }

        public Account GetCustomerByEmail(string email)
        {
            return _customerDAO.FindCustomerByEmail(email);
        }

        public Account GetCustomerById(string id)
        {
            return _customerDAO.FindCustomerById(id);
        }

        public List<Account> GetCustomers()
        {
            return _customerDAO.GetAllCustomers();
        }

        public List<Account> Search(string keyword)
        {
            return _customerDAO.Search(keyword);
        }

        public void UpdateCustomer(Account customer)
        {
            _customerDAO.UpdateCustomer(customer);
        }
    }
}
