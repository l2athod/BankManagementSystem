using Microsoft.AspNetCore.Mvc.Rendering;
using OnlineBanking.DataAccessLayer;
using OnlineBanking.Models;

namespace OnlineBanking.Services
{
    public class CustomerService
    {
        private readonly CustomerRepository customerRepository;

        public CustomerService(CustomerRepository _customerRepository)
        {
            customerRepository = _customerRepository;
        }
        public List<TransactionModel> CustomerTransactionsById(long id)
        {
            return customerRepository.CustomerTransactionsById(id);
        }
        public bool CreateTransaction(TransactionModel transaction) 
        {
            return customerRepository.CreateTransaction(transaction);
        }
        public Dictionary<string,string> GetAccountNumberWithAmount(long id)
        {
            return customerRepository.GetAccountNumberWithAmount(id);
        }

        public UserDetail GetCustomerDetailsById(long id)
        {
            return customerRepository.GetCustomerDetailsById(id);
        }
    }
}
