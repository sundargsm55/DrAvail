using DrAvail.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DrAvail.Controllers
{
    public class AdminController : Controller
    {
        private readonly Microsoft.Extensions.Configuration.IConfiguration _configuration;
        public AdminController(Microsoft.Extensions.Configuration.IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public IActionResult Index()
        {
            return View();
        }

        [Authorize(Roles = "Administrators")]
        public IActionResult PendingApproval()
        {
            DoctorService doctorService = new DoctorService();

            return View(doctorService.GetDocotrsByVerification());
        }
    }
}
