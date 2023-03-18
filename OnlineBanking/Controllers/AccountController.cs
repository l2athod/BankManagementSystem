using System.Data;
using Microsoft.AspNetCore.Mvc;
using OnlineBanking.Models;
using OnlineBanking.Services;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Data.SqlClient;

namespace OnlineBanking.Controllers
{
    public class AccountController : Controller
    {
        private readonly AccountService _accountService;
        private readonly string? _connectionstring;
        public AccountController(AccountService accountService, IConfiguration configuration)
        {
            _accountService = accountService;
            _connectionstring = configuration.GetConnectionString("DbConnection");
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(Login login)
        {
            if(String.IsNullOrEmpty(login.UserName) || String.IsNullOrEmpty(login.Password))
            {
                ModelState.AddModelError(String.Empty, "Please enter Username and Password");
                return View();
            }
            Dictionary<string, string> userDetails = _accountService.Login(login);
            if (userDetails.Count > 0)
            {
                HttpContext.Session.SetString("UserName", login.UserName);
                if (userDetails["Role"] == "Admin")
                {
                    return RedirectToAction("Home", "Admin", userDetails);
                }

                else if (userDetails["Role"] == "Customer")
                {
                    return RedirectToAction("Home", "Customer", userDetails);
                }
            }
            return View(login);
        }
        [HttpGet]  
        public IActionResult Logout()
        {
            HttpContext.Session.Remove("UserName");
            HttpContext.Session.Clear();
            return RedirectToAction("Login","Account");  
        }  
    }
}