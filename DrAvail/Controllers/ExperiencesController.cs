using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DrAvail.Data;
using DrAvail.Models;
using DrAvail.Authorization;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace DrAvail.Controllers
{
    public class ExperiencesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IAuthorizationService AuthorizationService;
        private readonly UserManager<ApplicationUser> userManager;

        public ExperiencesController(ApplicationDbContext context, IAuthorizationService authorizationService, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            AuthorizationService = authorizationService;
            this.userManager = userManager;
        }

        // GET: Experiences
        [Authorize(Roles = "Administrators")]
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Experiences.Include(e => e.Doctor);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Experiences/Details/5
        [Authorize(Roles = "Administrators")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var experience = await _context.Experiences
                .FirstOrDefaultAsync(m => m.ID == id);
            if (experience == null)
            {
                return NotFound();
            }

            return View(experience);
        }

        // GET: Experiences/Create
        [Authorize(Roles = "Administrators")]
        public IActionResult Create()
        {
            ViewData["DoctorID"] = new SelectList(_context.Doctors, "ID", "Name");
            return View();
        }

        // POST: Experiences/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrators")]
        public async Task<IActionResult> Create([Bind("Title,EmployementType,HospitalClinicName,Location,StartDate,EndDate,DoctorID")] Experience experience)
        {
            if (ModelState.IsValid)
            {
                _context.Add(experience);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));

            }
            ViewData["DoctorID"] = new SelectList(_context.Doctors, "ID", "Name", experience.DoctorID);
            return View(experience);
        }

        // GET: Experiences/Edit/5
        [Authorize(Roles = "Administrators")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var experience = await _context.Experiences.FindAsync(id);
            if (experience == null)
            {
                return NotFound();
            }
            ViewData["DoctorID"] = new SelectList(_context.Doctors, "ID", "Name", experience.DoctorID);
            return View(experience);
        }

        // POST: Experiences/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrators")]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Title,EmployementType,HospitalClinicName,Location,StartDate,EndDate,DoctorID,IsEndDatePresent")] Experience experience)
        {
            if (id != experience.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(experience);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ExperienceExists(experience.ID))
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
            ViewData["DoctorID"] = new SelectList(_context.Doctors, "ID", "City", experience.DoctorID);
            return View(experience);
        }

        // GET: Experiences/Delete/5
        [Authorize(Roles = "Administrators")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var experience = await _context.Experiences
                .Include(e => e.Doctor)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (experience == null)
            {
                return NotFound();
            }

            return View(experience);
        }

        // POST: Experiences/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrators")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var experience = await _context.Experiences.FindAsync(id);
            _context.Experiences.Remove(experience);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ExperienceExists(int id)
        {
            return _context.Experiences.Any(e => e.ID == id);
        }

        [HttpPost]
        [Authorize(Roles = "Administrators, Doctors")]
        public async Task<JsonResult> AddExperience(string Title, string EmployementType, string HospitalClinicName, string Location, DateTime StartDate, DateTime EndDate, int DoctorID, bool IsEndDatePresent)
        {
            

            try
            {
                Experience experience = new()
                {
                    Title = Title,
                    EmployementType = EmployementType,
                    HospitalClinicName = HospitalClinicName,
                    Location = Location,
                    StartDate = StartDate,
                    EndDate = EndDate,
                    DoctorID = DoctorID,
                    IsEndDatePresent = IsEndDatePresent,
                    OwnerID = userManager.GetUserId(User)
                };

                var isAuthorized = await AuthorizationService.AuthorizeAsync(
                                                  User, experience,
                                                  Operations.Create);
                if (!isAuthorized.Succeeded)
                {
                    return Json(false);
                }
                _context.Add(experience);
                await _context.SaveChangesAsync();
                return Json(true);
            }
            catch (Exception)
            {
                return Json(false);
            }

        }

        [HttpPost]
        [Authorize(Roles = "Administrators, Doctors")]
        public async Task<JsonResult> EditExperience(int Id, string Title, string EmployementType, string HospitalClinicName, string Location, DateTime StartDate, DateTime EndDate, int DoctorID, bool IsEndDatePresent)
        {
            var experience = await _context.Experiences.FindAsync(Id);
            if(experience != null)
            {
                experience.Title = Title;
                experience.EmployementType = EmployementType;
                experience.HospitalClinicName = HospitalClinicName;
                experience.Location = Location;
                experience.StartDate = StartDate;
                experience.EndDate = EndDate;
                experience.IsEndDatePresent = IsEndDatePresent;
            }
            else
            {
                return Json(false);
            }
            var isAuthorized = await AuthorizationService.AuthorizeAsync(
                                                  User, experience,
                                                  Operations.Update);
            if (!isAuthorized.Succeeded)
            {
                return Json(false);
            }
            try
                {
                    _context.Update(experience);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ExperienceExists(experience.ID))
                    {
                    return Json(false);
                    }
                    else
                    {
                        throw;
                    }
                }
                return Json(true);
            
        }

        [HttpPost]
        [Authorize(Roles = "Administrators, Doctors")]
        public async Task<JsonResult> DeleteExperience(int id)
        {
            var experience = await _context.Experiences.FindAsync(id);
            var isAuthorized = await AuthorizationService.AuthorizeAsync(
                                                  User, experience,
                                                  Operations.Delete);
            if (!isAuthorized.Succeeded)
            {
                return Json(false);
            }
            _context.Experiences.Remove(experience);
            await _context.SaveChangesAsync();
            return Json(true);
        }

        [HttpGet]
        [Authorize(Roles = "Administrators, Doctors")]
        public async Task<JsonResult> GetExperiences(int doctorID)
        {
            var experiences = await _context.Experiences.Where(e => e.DoctorID == doctorID).OrderBy(e => e.StartDate).ToListAsync();
            var isAuthorized = await AuthorizationService.AuthorizeAsync(
                                                  User, experiences.First(),
                                                  Operations.Read);
            if (!isAuthorized.Succeeded)
            {
                return Json(false);
            }
            return Json(experiences);
        }

        [HttpGet]
        [Authorize(Roles = "Administrators, Doctors")]
        public async Task<JsonResult> GetExperienceById(int ID)
        {
            var experience = await _context.Experiences.FindAsync(ID);
            var isAuthorized = await AuthorizationService.AuthorizeAsync(
                                                  User, experience,
                                                  Operations.Read);
            if (!isAuthorized.Succeeded)
            {
                return Json(false);
            }
            return Json(experience);
        }
    }
}
