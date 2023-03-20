using OnlineBanking.DataAccessLayer;
using OnlineBanking.Models;

namespace OnlineBanking.Services
{
    public class AdminService
    {
        private readonly AdminRepository customerRepository;
        public AdminService(AdminRepository _customerRepository) 
        {
            customerRepository = _customerRepository;
        }
        public List<Customer> GetCustomers()
        {
            List<Customer> list = customerRepository.GetCustomers();
            return list;
        }
        public bool CreateUpdateCustomer(Customer customer) 
        {
            return customerRepository.CreateUpdateCustomer(customer);
        }
        public Customer GetCustomerById(long id)
        {
            return customerRepository.GetCustomerById(id);
        }
        public bool DeleteCustomer(long? id)
        {
            return customerRepository.DeleteCustomer(id);
        }
        public List<TransactionModel> GetTransactions()
        {
            return customerRepository.GetTransactions();
        }
    }
}