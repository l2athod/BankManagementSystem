using OnlineBanking.DataAccessLayer;
using OnlineBanking.Models;

namespace OnlineBanking.Services
{
    public class AccountService
    {
        private readonly AccountRepository _accountRepository;

        public AccountService(AccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }
        public Dictionary<string,string> Login(HttpContext httpContext, Login login)
        {
            return _accountRepository.Login(httpContext,login);
        }
    }
}
