using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using OnlineBanking.DataAccessLayer;
using OnlineBanking.Models;
using OnlineBanking.Services;
using OnlineBanking.Utilities;
using System.Data;

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
            UserDetail user = adminService.GetAdminDetailsById(_adminId);
            return View(user);
        }
        [HttpGet]
        public IActionResult CustomerDetails()
        {
            List<Customer> list = adminService.GetCustomers();
            return View(list);  
        }
        [HttpGet]
        public IActionResult CreateUpdateCustomer(long id, string? accountNumber) 
        {
            if(id is 0)
            {
                if (accountNumber == null) return View("Create");
                Customer customer = new Customer();
                customer.AccountNumber = accountNumber;
                return View(customer);
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








        public bool IsInserted { get; private set; }
        [HttpGet]
        public IActionResult Accounts()
        {
            var accountModels = adminService.GetAllAccountList();
            if (accountModels.Count == 0)
            {
                TempData["InfoMessage"] = "Data not Availbe in Database.";
            }

            return View(accountModels);
        }

        // GET: AccountController/Details/5
        public ActionResult Detail(int id)
        {
            return View();
        }

        // GET: AccountController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AccountController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(AccountModel accountModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    bool isInserted = adminService.InsertAccountModel(accountModel);

                    if (isInserted)
                    {
                        TempData["SuccessMessage"] = "Account Details is Saved";
                    }
                    else
                    {
                        TempData["ErrorMessage"] = "Account Details Not Saved";
                    }

                    return RedirectToAction("CreateUpdateCustomer",new { AccountNumber = accountModel.AccountNumber });
                }
                else
                {
                    // If the model state is invalid, add error messages to TempData
                    foreach (var key in ModelState.Keys)
                    {
                        if (ModelState[key].Errors.Count > 0)
                        {
                            TempData["ErrorMessage"] = ModelState[key].Errors[0].ErrorMessage;
                            break;
                        }
                    }

                    return View(accountModel);
                }
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "An error occurred while saving the account details";
                return View(accountModel);
            }
        }

        // GET: AccountController/Edit/5
        public ActionResult Edit(int id)
        {
            var AccountModel = adminService.AccountId(id).FirstOrDefault();
            if (AccountModel == null)
            {
                TempData["InfoMessage"] = "Account is Not Available with ID" + id.ToString();
                return RedirectToAction("Home");
            }
            return View(AccountModel);
        }

        // POST: AccountController/Edit/5
        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(AccountModel accountModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    adminService.UpdateAccountModel(accountModel);
                }
                return RedirectToAction("Home");
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return View();
            }
        }


        // GET: AccountController/Delete/5

        [HttpGet]
        public IActionResult Delete(string id)
        {
            adminService.DeleteAccount(id);
            return RedirectToAction("Home");
        }










    }
}

