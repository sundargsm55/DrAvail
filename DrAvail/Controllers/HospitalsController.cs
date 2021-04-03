using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DrAvail.Data;
using DrAvail.Models;
using Microsoft.Extensions.Configuration;
using DrAvail.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;

namespace DrAvail.Controllers
{
    public class HospitalsController : DI_BaseController
    {
        //private readonly ApplicationDbContext _context;
        private readonly IConfiguration _configuration;

        [BindProperty]
        public Hospital hospital { get; set; }

        public HospitalsController(ApplicationDbContext context,
            Microsoft.AspNetCore.Authorization.IAuthorizationService authorizationService,
            UserManager<IdentityUser> userManager, IConfiguration configuration)
            : base(context, authorizationService, userManager)
        {
            _configuration = configuration;
        }

        // GET: Hospitals
        [AllowAnonymous]
        public async Task<IActionResult> Index(string searchString, string currentFilter, int? pageIndex)
        {
            if (searchString != null)
            {
                pageIndex = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewData["CurrentFilter"] = searchString;

            //IQueryable<Hospital> hospitalsIQ = from h in _context.Hospitals select h;

            IQueryable<Hospital> hospitalsIQ = Context.Hospitals.Include(h => h.Doctors);

            if (!string.IsNullOrEmpty(searchString))
            {
                hospitalsIQ = hospitalsIQ.Where(d => d.Name.Contains(searchString));
            }

            var isAuthorized = User.IsInRole(Constants.AdministratorsRole);

            var currentUserId = UserManager.GetUserId(User);

            // Only verified doctors are shown UNLESS you're authorized to see them
            // or you are the owner.
            if (!isAuthorized)
            {
                hospitalsIQ = hospitalsIQ.Where(h => h.IsVerified == true || h.OwnerID == currentUserId);
            }

            var pageSize = _configuration.GetValue("PageSize", 4); //Sets pageSize to 3 from Configuration, 4 if configuration fails.
            
            return View(await DrAvail.Services.PaginatedList<Hospital>.CreateAsync(
                hospitalsIQ.AsNoTracking(), pageIndex ?? 1, pageSize));

            //return View(await _context.Hospitals.Include(h => h.Doctors).ToListAsync());
        }

        // GET: Hospitals/Details/5
        [AllowAnonymous]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hospital = await Context.Hospitals
                .Include(h => h.Doctors)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (hospital == null)
            {
                return NotFound();
            }

            var isAuthorized = User.IsInRole(Constants.AdministratorsRole);

            var currentUserId = UserManager.GetUserId(User);

            if (!isAuthorized
                && currentUserId != hospital.OwnerID
                && !hospital.IsVerified)
            {
                return Forbid();
            }

            return View(hospital);
        }

        [HttpPost, ActionName("Details")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DetailsOnPost(int? id, bool status)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hospital = await Context.Hospitals
                .Include(h => h.Doctors)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (hospital == null)
            {
                return NotFound();
            }

            var operation = (status == true) ? Operations.Approve : Operations.Reject;

            var isAuthorized = await AuthorizationService.AuthorizeAsync(User, hospital, operation);

            if (!isAuthorized.Succeeded)
            {
                return Forbid();
            }
            hospital.IsVerified = status;
            Context.Hospitals.Update(hospital);
            await Context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        // GET: Hospitals/Create
        public async Task<IActionResult> Create()
        {

            hospital = new Hospital();
            hospital.OwnerID = UserManager.GetUserId(User);

            var isAuthorized = await AuthorizationService.AuthorizeAsync(User, hospital, Operations.Create);

            if (!isAuthorized.Succeeded)
            {
                return Forbid();
            }

            #region selectList
            var values = from HospitalType H in Enum.GetValues(typeof(HospitalType))
                         select new { ID = (int)H, Name = H.ToString() };
            ViewBag.HospitalType = new SelectList(values, "ID", "Name"); ;

            var district = from District d in Enum.GetValues(typeof(District))
                         select new { ID = (int)d, Name = d.ToString() };
                       
            ViewBag.Districts = new SelectList(district, "ID", "Name");
            #endregion
            return View();
        }

        // POST: Hospitals/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost, ActionName("Create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateOnPost()
        {
            var isAuthorized = await AuthorizationService.AuthorizeAsync(
                                                User, hospital,
                                                Operations.Create);
            if (!isAuthorized.Succeeded)
            {
                return Forbid();
            }

            hospital.OwnerID = UserManager.GetUserId(User);

            if (ModelState.IsValid)
            {
                Context.Add(hospital);
                await Context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            var values = from HospitalType H in Enum.GetValues(typeof(HospitalType))
                         select new { ID = (int)H, Name = H.ToString() };
            var district = from District d in Enum.GetValues(typeof(District))
                           select new { ID = (int)d, Name = d.ToString() };

            ViewBag.HospitalType = new SelectList(values, "ID", "Name"); ;
            ViewBag.Districts = new SelectList(district, "ID", "Name");

            return View(hospital);
        }

        // GET: Hospitals/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hospital = await Context.Hospitals.FindAsync(id);
            if (hospital == null)
            {
                return NotFound();
            }

            var isAuthorized = await AuthorizationService.AuthorizeAsync(
                                                  User, hospital,
                                                  Operations.Update);
            if (!isAuthorized.Succeeded)
            {
                return Forbid();
            }
            return View(hospital);
        }

        // POST: Hospitals/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id)
        {
            if (id != hospital.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var isAuthorized = await AuthorizationService.AuthorizeAsync(
                                                 User, hospital,
                                                 Operations.Update);
                    if (!isAuthorized.Succeeded)
                    {
                        return Forbid();
                    }

                    if (hospital.IsVerified)
                    {
                        // If the hospital details are updated after verification, 
                        // and the user cannot approve,
                        // set the status back to submitted so the update can be
                        // checked and approved.
                        var canApprove = await AuthorizationService.AuthorizeAsync(User,
                                                hospital,
                                                Operations.Approve);

                        if (!canApprove.Succeeded)
                        {
                            hospital.IsVerified = false;
                        }
                    }
                    Context.Update(hospital);
                    await Context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HospitalExists(hospital.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(hospital);
        }

        // GET: Hospitals/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hospital = await Context.Hospitals
                .FirstOrDefaultAsync(m => m.ID == id);
            if (hospital == null)
            {
                return NotFound();
            }
            var isAuthorized = await AuthorizationService.AuthorizeAsync(
                                                 User, hospital,
                                                 Operations.Delete);
            if (!isAuthorized.Succeeded)
            {
                return Forbid();
            }

            return View(hospital);
        }

        // POST: Hospitals/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var hospital = await Context.Hospitals.FindAsync(id);

            var isAuthorized = await AuthorizationService.AuthorizeAsync(
                                                 User, hospital,
                                                 Operations.Delete);
            if (!isAuthorized.Succeeded)
            {
                return Forbid();
            }
            Context.Hospitals.Remove(hospital);
            await Context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool HospitalExists(int id)
        {
            return Context.Hospitals.Any(e => e.ID == id);
        }
    }
}
