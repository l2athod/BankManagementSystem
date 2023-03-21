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
        public UserDetail GetAdminDetailsById(long id)
        {
            return customerRepository.GetAdminDetailsById(id);
        }

        public List<AccountModel> GetAllAccountList()
        {
            return customerRepository.GetAllAccountList();
        }

        public bool InsertAccountModel(AccountModel model)
        {
            return customerRepository.InsertAccountModel(model);
        }

        public List<AccountModel> AccountId(int AccountId)
        {
            return customerRepository.AccountId(AccountId);
        }

        public void UpdateAccountModel(AccountModel accountModel)
        {
            customerRepository.UpdateAccountModel(accountModel);
        }
        public void DeleteAccount(string Id)
        {
            customerRepository.DeleteAccount(Id);
        }
    }
}