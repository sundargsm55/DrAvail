using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DrAvail.Controllers
{
    public class RoleController : Controller
    {
        RoleManager<IdentityRole> RoleManager { get; set; }

        public RoleController(RoleManager<IdentityRole> roleManager)
        {
            this.RoleManager = roleManager;
        }
        public IActionResult Index()
        {
            var roles = RoleManager.Roles.ToList();
            return View(roles);
        }

        public IActionResult Create()
        {
            return View(new IdentityRole());
        }

        [HttpPost]
        public async Task<IActionResult> Create(IdentityRole role)
        {
            var result = await RoleManager.CreateAsync(role);
            if (result.Succeeded)
            {
                return RedirectToAction("Index");

            }
            ViewBag.Error = result.Errors;
            return View(role);
        }

        public async Task<IActionResult> DeleteAsync(string id)
        {
            if (id == null) return NotFound();

            var role = await RoleManager.FindByIdAsync(id);

            if (role == null) return NotFound();

            return View(role);

        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmedAsync(string id)
        {
            if (id == null) return NotFound();

            var role = await RoleManager.FindByIdAsync(id);

            if (role == null) return NotFound();

            var result = await RoleManager.DeleteAsync(role);

            if (result.Succeeded)
            {
                return RedirectToAction(nameof(Index));
            }

            ViewBag.Error = result.Errors;
            return View(role);

        }
    }
}
