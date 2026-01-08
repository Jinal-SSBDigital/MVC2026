using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Mvc;
using MVC2026.Data;
using MVC2026.Helper;
using MVC2026.Models;
using System.Security.Claims;

namespace MVC2026.Controllers
{
    public class AccountController : Controller
    {
        private readonly AppDbContext context;

        public AccountController(AppDbContext dbContext)
        {
            context = dbContext;
        }
        public IActionResult Index()
        {
            return View();
        }


        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(CustomerLogin Clogin)
        {
            var custm = context.Customers.FirstOrDefault(x => x.Email == Clogin.Email);

            if (custm == null)
            {
                ViewBag.Error = "Invalid Login";
                return View();
            }

            bool isPaawordValid = PasswordHelper.VerifyPasswordHash(Clogin.Password, custm.PasswordHash, custm.PasswordSalt);

            if (!isPaawordValid)
            {
                ViewBag.Error = "Invalid Login";
                return View();
            }
            HttpContext.Session.SetInt32("CustomerId", custm.CustmId);
            return RedirectToAction("Index", "Home");

            //return View();
        }

        [HttpGet]

        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]

        public IActionResult Register(CustomerRegister CReg)
        {
            if (context.Customers.Any(x => x.Email == CReg.Email))
            {
                ViewBag.Error = "Email Already Exists.";
                return View();
            }

            PasswordHelper.CreatePAsswordhash(CReg.Password, out byte[] hash, out byte[] salt);
            var customer = new Customer
            {
                Custm_name = CReg.Custm_name,
                Email = CReg.Email,
                PlainTextPassword = CReg.Password,
                PasswordHash =hash,
                PasswordSalt =salt,
                Mobile = CReg.Mobile,
                Address = CReg.Address
            };

            context.Customers.Add(customer);
            context.SaveChanges();

            HttpContext.Session.SetInt32("CustomerId", customer.CustmId);
            return RedirectToAction("Index", "Home");

        }

        //Google Login 
        public IActionResult GoogleLogin()
        {
            var properties = new AuthenticationProperties
            {
                RedirectUri = Url.Action("GoogleCallback")
            };
            return Challenge(properties, GoogleDefaults.AuthenticationScheme);
        }
        // 🔹 GOOGLE CALLBACK (THIS IS WHAT YOU ASKED)
        public async Task<IActionResult> GoogleCallback()
        {
            try
            {
                var result = await HttpContext.AuthenticateAsync();

                if (!result.Succeeded)
                    return RedirectToAction("Login");

                var Email = result.Principal.FindFirstValue(ClaimTypes.Email);
                var Name = result.Principal.FindFirstValue(ClaimTypes.Name);

                var customer = context.Customer.FirstOrDefault(c => c.Email == Email);
      
                if (customer == null)
                {
                    customer = new Customer
                    {
                        Custm_name = Name,
                        Email = Email,
                        Createddate = DateTime.Now
                    };
                    context.Customers.Add(customer);
                    context.SaveChanges();

                }
                HttpContext.Session.SetInt32("CustomerId", customer.CustmId);
                return RedirectToAction("Index", "Home");
            }
            catch (Exception ex)
            {

                throw;
            }
            
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }
    }
}
