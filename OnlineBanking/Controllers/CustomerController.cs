using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using OnlineBanking.Models;
using OnlineBanking.Services;
using OnlineBanking.Utilities;
using System.Text.Json;

namespace OnlineBanking.Controllers
{
    [AuthenticationCustomer]
    public class CustomerController: Controller
    {
        private readonly CustomerService _customerService;
        private static long _customerId { get; set; }

        public CustomerController(CustomerService customerService)
        {
            _customerService = customerService;
        }

        [HttpGet]
        public IActionResult Home(Dictionary<string,string>? UserDetails)
        {
            if (UserDetails.ContainsKey("LoggerId"))
            {
                _customerId = Convert.ToInt64(UserDetails["LoggerId"]);
                HttpContext.Response.Redirect("/Customer/Home");
            }

            ViewBag.LoggerId = _customerId.ToString();
            UserDetail user = _customerService.GetCustomerDetailsById(_customerId);
            return View(user);
        }


        [HttpGet]
        public IActionResult CustomerTransactionsById()
        {
            List<TransactionModel> transaction = _customerService.CustomerTransactionsById(_customerId);
            return View(transaction);
        }
        [HttpGet]
        public IActionResult CreateTransaction()
        {
            AccountTransaction accountTransaction = new AccountTransaction();
            accountTransaction.AccountBalance = _customerService.GetAccountNumberWithAmount(_customerId);
            foreach(var item in accountTransaction.AccountBalance)
            {
                accountTransaction.accounts.Add(new SelectListItem()
                {
                    Text = item.Key,
                    Value = item.Key 
                });
            }
            accountTransaction.transactionModel.CustomerId = _customerId;
            return View(accountTransaction);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateTransaction(AccountTransaction accountTransaction,string CurrentBalance, string accountlist)
        {
            List<SelectListItem> CustomerAccounts = JsonSerializer.Deserialize<List<SelectListItem>>(accountlist);
            if(accountTransaction.transactionModel.TransferAmount > Convert.ToDecimal(CurrentBalance))
            {
                ModelState.AddModelError("transactionModel.TransferAmount", "Transfer amount is bigger than current balance");
                return View(accountTransaction);
            }
            if (ModelState.IsValid)
            {
                bool res = _customerService.CreateTransaction(accountTransaction.transactionModel);
                if (res)
                {
                    return RedirectToAction("CustomerTransactionsById");
                }
            }
            accountTransaction.accounts = CustomerAccounts;
            return View(accountTransaction);
        }

        [HttpPost]
        public IActionResult AccountBalanceInfo(Dictionary<string,string> model,  string selectedOption)
        {
            return Ok(model[selectedOption]);
        }
    }
}