using DrAvail.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;


namespace DrAvail.Controllers
{
    public class AdminController : Controller
    {
        private readonly Microsoft.Extensions.Configuration.IConfiguration _configuration;

        private readonly ILogger _logger;
        public AdminController(Microsoft.Extensions.Configuration.IConfiguration configuration, ILogger<AdminController> logger)
        {
            _configuration = configuration;
            _logger = logger;
        }
        public IActionResult Index()
        {
            return View();
        }

        [Authorize(Roles = "Administrators")]
        public IActionResult PendingApproval(int? pageIndex)
        {
            DoctorService doctorService = new DoctorService();
            var Message = $"Administrator visited PendingApproval page at {DateTime.Now.ToString()}";
            _logger.LogInformation(Message);
            return View(doctorService.GetDocotrsByVerification());
        }
    }
}
