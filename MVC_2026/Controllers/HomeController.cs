using MapLocation.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MVC2026.Data;
using MVC2026.Models;
using System.Diagnostics;

namespace MapLocation.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _context;

        public HomeController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Home/Index
        public ActionResult Index()
        {
            // Check if user is logged in
            var userEmail = HttpContext.Session.GetString("UserEmail");

            if (string.IsNullOrEmpty(userEmail))
            {
                // User is NOT logged in - show modal after 5 seconds
                ViewBag.AutoShowLogin = true;
            }
            else
            {
                // User IS logged in - don't show modal
                ViewBag.AutoShowLogin = false;
            }

            return View();
        }

        // GET: Home/Museums
        public ActionResult Museums()
        {
            // Check if user is logged in
            var userEmail = HttpContext.Session.GetString("UserEmail");

            if (string.IsNullOrEmpty(userEmail))
            {

                // User not logged in - redirect to Index with login modal
                return RedirectToAction("Index", new { showLogin = true });
            }

            var model = new MuseumViewModel { States = _context.States.Select(s => new SelectListItem { Value = s.StateId.ToString(), Text = s.StateName }).ToList() };
            return View(model);
        }

        // Rest of your controller methods remain the same
        public ActionResult VirtualTours()
        {
            var userEmail = HttpContext.Session.GetString("UserEmail");

            if (string.IsNullOrEmpty(userEmail))
            {
                // User not logged in - redirect to Index with login modal
                return RedirectToAction("Index", new { showLogin = true });
            }
            return View();
        }

        public ActionResult Artefacts()
        {
            var userEmail = HttpContext.Session.GetString("UserEmail");

            if (string.IsNullOrEmpty(userEmail))
            {
                // User not logged in - redirect to Index with login modal
                return RedirectToAction("Index", new { showLogin = true });
            }
            return View();
        }

        public ActionResult About()
        {
            var userEmail = HttpContext.Session.GetString("UserEmail");

            if (string.IsNullOrEmpty(userEmail))
            {
                // User not logged in - redirect to Index with login modal
                return RedirectToAction("Index", new { showLogin = true });
                ViewBag.Message = "Learn more about Museum Explorer";
            }
      
            return View();
        }

        public ActionResult Contact()
        {
            var userEmail = HttpContext.Session.GetString("UserEmail");

            if (string.IsNullOrEmpty(userEmail))
            {
                // User not logged in - redirect to Index with login modal
                return RedirectToAction("Index", new { showLogin = true });
                ViewBag.Message = "Get in touch with us";
            }
        
            return View();
        }

        public ActionResult BookTickets()
        {
            return View();
        }

        public ActionResult FAQ()
        {
            return View();
        }

        public ActionResult Privacy()
        {
            return View();
        }
    }
}