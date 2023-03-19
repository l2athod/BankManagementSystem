using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineBanking.Models;
using OnlineBanking.Services;
using OnlineBanking.Utilities;

namespace OnlineBanking.Controllers
{
    [AuthenticationAdmin]
    public class AdminController : Controller
    {
        private readonly AdminService adminService;
        private static long _adminId { get; set; }

        public AdminController(AdminService _adminService)
        {
            adminService = _adminService;
        }
        [HttpGet]
        public IActionResult Home(Dictionary<string,string>? UserDetail)
        {
            if(UserDetail.ContainsKey("LoggerId")) {
                _adminId = Convert.ToInt64(UserDetail["LoggerId"]);
                HttpContext.Response.Redirect("/Admin/Home");
            }
            return View();
        }
        [HttpGet]
        public IActionResult CustomerDetails()
        {
            List<Customer> list = adminService.GetCustomers();
            return View(list);
        }
        [HttpGet]
        public IActionResult CreateUpdateCustomer(long id) 
        {
            if(id is 0)
            {
                return View();
            }
            else
            {
                Customer customer = adminService.GetCustomerById(id);
                return View(customer);
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateUpdateCustomer(Customer customer)
        {
            if (ModelState.IsValid)
            {
                bool res = adminService.CreateUpdateCustomer(customer);
                if (res)
                {
                    return RedirectToAction("CustomerDetails");
                }
            }
            return View(customer);
        }
        [HttpGet]
        public IActionResult DeleteCustomer(int id)
        {
            Customer customer = adminService.GetCustomerById(id);
            return View(customer);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteCustomer(Customer customer)
        {
            bool res = adminService.DeleteCustomer(customer.CustomerId);
            if(res)
            {
                return RedirectToAction("CustomerDetails");
            }
            return View(customer);
        }

        [HttpGet]
        public IActionResult TransactionHistory()
        {
            List<TransactionModel> list = adminService.GetTransactions();
            return View(list);
        }
    }
}

