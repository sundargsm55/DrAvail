using DrAvail.Data;
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
        private readonly ApplicationDbContext _context;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
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
        public IActionResult Contact([FromQuery] string subject, [FromQuery] string message)
        {
            if (!string.IsNullOrWhiteSpace(subject))
            {
                ViewBag.subject = subject;
            }
            if (!string.IsNullOrWhiteSpace(message))
            {
                ViewBag.message = message;
            }

            return View();
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Microsoft.AspNetCore.Authorization.AllowAnonymous]

        public async Task<IActionResult> Contact(string email, string subject, string message)
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
                var ip = HttpContext.Connection.RemoteIpAddress.ToString() + ":" + HttpContext.Connection.RemotePort.ToString();
                _logger.LogInformation($"Ip Address: {ip} \nUserName: {email}\nSubject: {subject}\nMessage:{message}");
                Message messageObj = new Message();
                messageObj.IP = ip;
                messageObj.Email = email;
                messageObj.Subject = subject;
                messageObj.MessageText = message;

                _context.Messages.Add(messageObj);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Thanks));
            }
                
            return View();
        }

        [Microsoft.AspNetCore.Authorization.AllowAnonymous]
        public IActionResult Thanks()
        {
            string referer = Request.Headers["Referer"].ToString();
            if (!string.IsNullOrEmpty(referer) && referer.Contains("/Home/Contact"))
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
