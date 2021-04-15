using DrAvail.Data;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using DrAvail.Models;

namespace DrAvail.Controllers
{
    public class UserController : Controller
    {
        private readonly ApplicationDbContext _context;

        [BindProperty]
        public Comment Comment { get; set; }

        public UserController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> ViewComments(int doctorID)
        {
            var comments = _context.Comments
                .Where(c => c.DoctorID == doctorID)
                .Include(c => c.Reply);
                

            return View(await comments.ToListAsync());
        }

        
    }
}
