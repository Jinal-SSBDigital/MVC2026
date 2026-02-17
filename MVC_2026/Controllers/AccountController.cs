using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Mvc;
using MVC2026.Data;
using MVC2026.Models;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Claims;

namespace MapLocation.Controllers
{
    public class AccountController : Controller
    {
        private readonly AppDbContext _context;

        public AccountController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Account/Login
        public ActionResult Login()
        {
            // If already logged in, redirect to home
            var userEmail = HttpContext.Session.GetString("UserEmail");
            if (!string.IsNullOrEmpty(userEmail))
            {
                return RedirectToAction("Index", "Home");
            }

            return View();
        }

        // POST: Account/Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(CustomerLogin model, bool remember = false)
        {
            //if (ModelState.IsValid)
            //{
                // Check credentials in database
                var user = _context.Customers.FirstOrDefault(x => x.Email == model.Email);

            if (user != null)
                {
                    // Login successful - Set session
                    HttpContext.Session.SetString("UserEmail", user.Email);
                    HttpContext.Session.SetString("UserName", user.Email.Split('@')[0]);
                    HttpContext.Session.SetInt32("UserId", user.CustmId);

                    // Set session timeout based on remember me
                    if (remember)
                    {
                        HttpContext.Session.SetString("RememberMe", "true");
                    }

                    // Set TempData to indicate successful login
                    TempData["LoginSuccess"] = true;
                    TempData["WelcomeMessage"] = $"Welcome back!";

                    // Redirect to home page
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    // Login failed
                    ModelState.AddModelError("", "Invalid email or password");
                    ViewBag.Error = "Invalid email or password. Please try again.";
                }
            //}

            return View(model);
        }

        // GET: Account/Register
        public ActionResult Register()
        {
            //// If already logged in, redirect to home
            //var userEmail = HttpContext.Session.GetString("UserEmail");
            //if (!string.IsNullOrEmpty(userEmail))
            //{
            //    return RedirectToAction("Index", "Home");
            //}
            HttpContext.Session.Clear();
            ModelState.Clear();
            //HttpContext.Session.Remove("UserEmail");
            return View();
        }

        // POST: Account/Register
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(CustomerLogin model)
        {
            if (ModelState.IsValid)
            {
                // Check if email already exists
                var existingUser = _context.CustomerLogins.FirstOrDefault(u => u.Email == model.Email);

                if (existingUser != null)
                {
                    ModelState.AddModelError("Email", "This email is already registered. Please login instead.");
                    ViewBag.Error = "Email already registered";
                    return View(model);
                }

                // Add new user
                _context.CustomerLogins.Add(model);
                _context.SaveChanges();

                // Auto login after registration
                HttpContext.Session.SetString("UserEmail", model.Email);
                HttpContext.Session.SetString("UserName", model.Email.Split('@')[0]);
                HttpContext.Session.SetInt32("UserId", model.Id);

                TempData["LoginSuccess"] = true;
                TempData["WelcomeMessage"] = "Account created successfully! Welcome to Museum Explorer!";

                return RedirectToAction("Index", "Home");
            }

            return View(model);
        }

        // GET: Account/Logout
        public ActionResult Logout()
        {
            // Clear session
            HttpContext.Session.Clear();

            TempData["LogoutMessage"] = "You have been logged out successfully";

            return RedirectToAction("Index", "Home");
        }

        // GET: Account/ForgotPassword
        public ActionResult ForgotPassword()
        {
            return View();
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
        //  GOOGLE CALLBACK (THIS IS WHAT YOU ASKED)
        public async Task<IActionResult> GoogleCallback()
        {
            try
            {
                var result = await HttpContext.AuthenticateAsync();

                if (!result.Succeeded)
                    return RedirectToAction("Login");

                var Email = result.Principal.FindFirstValue(ClaimTypes.Email);
                var Name = result.Principal.FindFirstValue(ClaimTypes.Name);

                var customer = _context.Customer.FirstOrDefault(c => c.Email == Email);

                if (customer == null)
                {
                    customer = new Customer
                    {
                        Custm_name = Name,
                        Email = Email,
                        Createddate = DateTime.Now
                    };
                    _context.Customers.Add(customer);
                    _context.SaveChanges();

                }
                HttpContext.Session.SetInt32("CustomerId", customer.CustmId);
                HttpContext.Session.SetString("UserEmail", customer.Email);
                HttpContext.Session.SetString("UserName", customer.Email.Split('@')[0]);

                // Set TempData to indicate successful login
                TempData["LoginSuccess"] = true;
                TempData["WelcomeMessage"] = $"Welcome back!";
                return RedirectToAction("Index", "Home");
            }
            catch (Exception ex)
            {

                throw;
            }

        }
    }
}