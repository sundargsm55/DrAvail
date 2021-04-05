using DrAvail.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace DrAvail.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        [Microsoft.AspNetCore.Authorization.AllowAnonymous]
        public IActionResult Privacy()
        {
            return View();
        }

        //GET
        [Microsoft.AspNetCore.Authorization.AllowAnonymous]
        public IActionResult Contact()
        {
            return View();
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Microsoft.AspNetCore.Authorization.AllowAnonymous]

        public IActionResult Contact(string email, string subject, string message)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                ViewBag.Errors = "Please enter a valid email address";
            }
            else if (string.IsNullOrWhiteSpace(subject))
            {
                ViewBag.Errors = "Please enter a valid description";
            }
            else if (string.IsNullOrWhiteSpace(message))
            {
                ViewBag.Errors = "Please enter a valid message";
            }
            else
            {

                return RedirectToAction(nameof(Thanks));
            }
                
            return View();
        }

        [Microsoft.AspNetCore.Authorization.AllowAnonymous]
        public IActionResult Thanks()
        {
            string referer = Request.Headers["Referer"].ToString();
            ViewBag.Page = referer;
            if (!string.IsNullOrEmpty(referer) && referer.Contains("Contact"))
            {
                return View();
            }


            return NotFound();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
