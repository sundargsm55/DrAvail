using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DrAvail.Data;
using DrAvail.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Identity;
using DrAvail.Authorization;

namespace DrAvail.Controllers
{
    public class DoctorsController : DI_BaseController
    {
        //private readonly ApplicationDbContext _context;
        private readonly IConfiguration _configuration;

        [BindProperty]
        public Doctor doctor { get; set; }

        //public DrAvail.Services.PaginatedList<Doctor> Doctors { get; set; }

        public DoctorsController(ApplicationDbContext context, IAuthorizationService authorizationService, UserManager<IdentityUser> userManager, IConfiguration configuration)
            :base(context, authorizationService,userManager)
        {
            //Context = context;
            _configuration = configuration;
        }

        
        // GET: Doctors
        [AllowAnonymous]
        public async Task<IActionResult> Index(string currentFilter, string searchString, int? pageIndex)
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
            IQueryable<Doctor> doctorsIQ = Context.Doctors
                .Include(d => d.CommonAvailability)
                .Include(d => d.CurrentAvailability)
                .Include(d => d.Hospital);

            if (!string.IsNullOrEmpty(searchString))
            {
                doctorsIQ = doctorsIQ.Where(d => d.Name.Contains(searchString));
            }

            //checks if a user is logged in
            if (User.Identity.Name == null)
            {
                var isAuthorized = User.IsInRole(Constants.AdministratorsRole);

                var currentUserId = UserManager.GetUserId(User);

                // Only verified doctors are shown UNLESS you're authorized to see them
                // or you are the owner.
                if (!isAuthorized)
                {
                    doctorsIQ = doctorsIQ.Where(d => d.IsVerified == true || d.OwnerID == currentUserId);
                }
            }
            else
            {
                doctorsIQ = doctorsIQ.Where(d => d.IsVerified == true);
            }
            var pageSize = _configuration.GetValue("PageSize", 4); //Sets pageSize to 3 from Configuration, 4 if configuration fails.
            return View(await DrAvail.Services.PaginatedList<Doctor>.CreateAsync(
                doctorsIQ.AsNoTracking(), pageIndex ?? 1, pageSize));
            
            //return View(await doctorsIQ.AsNoTracking().ToListAsync());
        }

        // GET: Doctors/Details/5
        [AllowAnonymous]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var doctor = await Context.Doctors
                .Include(d => d.CommonAvailability)
                .Include(d => d.CurrentAvailability)
                .Include(d => d.Hospital)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (doctor == null)
            {
                return NotFound();
            }

            var isAuthorized = User.IsInRole(Constants.AdministratorsRole);

            var currentUserId = UserManager.GetUserId(User);

            if (!isAuthorized
                && currentUserId != doctor.OwnerID
                && !doctor.IsVerified)
            {
                return Forbid();
            }

            return View(doctor);
        }

        [HttpPost, ActionName("Details")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DetailsOnPost(int? id, bool status)
        {
            if (id == null)
            {
                return NotFound();
            }

            var doctor = await Context.Doctors
                .Include(d => d.CommonAvailability)
                .Include(d => d.CurrentAvailability)
                .Include(d => d.Hospital)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (doctor == null)
            {
                return NotFound();
            }

            var operation = (status == true)? Operations.Approve : Operations.Reject;

            var isAuthorized = await AuthorizationService.AuthorizeAsync(User, doctor, operation);

            if (!isAuthorized.Succeeded)
            {
                return Forbid();
            }
            doctor.IsVerified = status;
            Context.Doctors.Update(doctor);
            await Context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));

        }
        // GET: Doctors/Create
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            doctor = new Doctor();
            doctor.OwnerID = UserManager.GetUserId(User);

            var isAuthorized = await AuthorizationService.AuthorizeAsync(User, doctor,Operations.Create);

            if (!isAuthorized.Succeeded)
            {
                return Forbid();
            }


            //if (!User.IsInRole(Constants.AdministratorsRole))
            //{

            //    doctor.OwnerID = UserManager.GetUserId(User);
            //}

            #region selectList
            ViewData["CommonAvaliabilityID"] = new SelectList(Context.Availabilities, "ID", "ID");
            ViewData["CurrentAvaliabilityID"] = new SelectList(Context.Availabilities, "ID", "ID");
            ViewData["HospitalID"] = new SelectList(Context.Hospitals, "ID", "Name");

            var values = from HospitalType H in Enum.GetValues(typeof(HospitalType))
                         select new { ID = (int)H, Name = H.ToString() };
            ViewBag.HospitalType = new SelectList(values, "ID", "Name"); ;

            var district = from District d in Enum.GetValues(typeof(District))
                           select new { ID = (int)d, Name = d.ToString() };
            ViewBag.Districts = new SelectList(district, "ID", "Name");

            var speciality = from Speciality d in Enum.GetValues(typeof(Speciality))
                             select new { ID = (int)d, Name = d.ToString() };
            ViewBag.Speciality = new SelectList(speciality, "Name", "Name");

            var gender = from Gender H in Enum.GetValues(typeof(Gender))
                         select new { Name = H.ToString() };
            ViewBag.Gender = gender.ToList();
            #endregion

            return View();
        }

        // POST: Doctors/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost, ActionName("Create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateOnPost()
        {

            var isAuthorized = await AuthorizationService.AuthorizeAsync(
                                                User, doctor,
                                                Operations.Create);
            if (!isAuthorized.Succeeded)
            {
                return Forbid();
            }

            doctor.OwnerID = UserManager.GetUserId(User);

            //if (!User.IsInRole(Constants.AdministratorsRole))
            //{

            //    doctor.OwnerID = UserManager.GetUserId(User);
            //}
            try
            {
                doctor.CommonAvailability.CommonDays.MorningStartTime = new DateTime(year: 2021, month: 3, day: 24,
                    hour: int.Parse(doctor.CommonAvailability.CommonDays.MorningStartHour),
                    minute: int.Parse(doctor.CommonAvailability.CommonDays.MorningStartMinute),
                    second: 0);
                doctor.CommonAvailability.AvailabilityType = doctor.RegNumber + "Common";
                //Console.WriteLine("----------------------------------------------");
                //Console.WriteLine("Before INSERT -> Hospital Id: " + doctor.HospitalID);
                //Console.WriteLine(doctor);
                //Console.WriteLine("----------------------------------------------");

                if (doctor.HospitalID != 0)
                {
                    doctor.Hospital = null;
                    //_context.Entry(doctor.Hospital).State = EntityState.Unchanged;
                }
                Context.Doctors.Add(doctor);

                await Context.SaveChangesAsync();
                //Console.WriteLine("----------------------------------------------");
                //Console.WriteLine("After INSERT -> doctor Id: " + doctor.ID);
                //Console.WriteLine(doctor);

                return RedirectToAction(nameof(Index));

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            #region selectList
            ViewData["CommonAvaliabilityID"] = new SelectList(Context.Availabilities, "ID", "ID");
            ViewData["CurrentAvaliabilityID"] = new SelectList(Context.Availabilities, "ID", "ID");
            ViewData["HospitalID"] = new SelectList(Context.Hospitals, "ID", "Name");

            var values = from HospitalType H in Enum.GetValues(typeof(HospitalType))
                         select new { ID = (int)H, Name = H.ToString() };
            ViewBag.HospitalType = new SelectList(values, "ID", "Name"); ;

            var district = from District d in Enum.GetValues(typeof(District))
                           select new { ID = (int)d, Name = d.ToString() };
            ViewBag.Districts = new SelectList(district, "ID", "Name");

            var speciality = from Speciality d in Enum.GetValues(typeof(Speciality))
                             select new { ID = (int)d, Name = d.ToString() };
            ViewBag.Speciality = new SelectList(speciality, "ID", "Name");
            //var gender = from Gender H in Enum.GetValues(typeof(Gender))
            //             select new { Name = H.ToString() };
            //ViewBag.Gender = gender.ToList();
            ViewBag.Gender = Enum.GetNames(typeof(Gender)).Cast<Gender>().ToList();
            #endregion

            return View(doctor);
        }

        // GET: Doctors/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
                     
            if (id == null)
            {
                return NotFound();
            }

            var doctor = await Context.Doctors.FindAsync(id);
            if (doctor == null)
            {
                return NotFound();
            }

            var isAuthorized = await AuthorizationService.AuthorizeAsync(
                                                  User, doctor,
                                                  Operations.Update);
            if (!isAuthorized.Succeeded)
            {
                return Forbid();
            }

            ViewData["CommonAvaliabilityID"] = new SelectList(Context.Availabilities, "ID", "ID", doctor.CommonAvaliabilityID);
            ViewData["CurrentAvaliabilityID"] = new SelectList(Context.Availabilities, "ID", "ID", doctor.CurrentAvaliabilityID);
            ViewData["HospitalID"] = new SelectList(Context.Hospitals, "ID", "Name", doctor.HospitalID);
            var district = from District d in Enum.GetValues(typeof(District))
                           select new { ID = (int)d, Name = d.ToString() };
            //ViewBag.Districts = new SelectList(district, "ID", "Name");

            var speciality = from Speciality d in Enum.GetValues(typeof(Speciality))
                             select new { ID = (int)d, Name = d.ToString() };
            ViewBag.Speciality = new SelectList(speciality, "Name", "Name");
            return View(doctor);
        }

        // POST: Doctors/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id)
        {
            if (id != doctor.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var isAuthorized = await AuthorizationService.AuthorizeAsync(
                                                 User, doctor,
                                                 Operations.Update);
                    if (!isAuthorized.Succeeded)
                    {
                        return Forbid();
                    }

                    if (doctor.IsVerified)
                    {
                        // If the doctor details are updated after verification, 
                        // and the user cannot approve,
                        // set the status back to submitted so the update can be
                        // checked and approved.
                        var canApprove = await AuthorizationService.AuthorizeAsync(User,
                                                doctor,
                                                Operations.Approve);

                        if (!canApprove.Succeeded)
                        {
                            doctor.IsVerified = false;
                        }
                    }
                    Context.Update(doctor);
                    await Context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DoctorExists(doctor.ID))
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
            ViewData["CommonAvaliabilityID"] = new SelectList(Context.Availabilities, "ID", "ID", doctor.CommonAvaliabilityID);
            ViewData["CurrentAvaliabilityID"] = new SelectList(Context.Availabilities, "ID", "ID", doctor.CurrentAvaliabilityID);
            ViewData["HospitalID"] = new SelectList(Context.Hospitals, "ID", "Name", doctor.HospitalID);
            var district = from District d in Enum.GetValues(typeof(District))
                           select new { ID = (int)d, Name = d.ToString() };
            ViewBag.Districts = new SelectList(district, "ID", "Name");

            var speciality = from Speciality d in Enum.GetValues(typeof(Speciality))
                             select new { ID = (int)d, Name = d.ToString() };
            ViewBag.Speciality = new SelectList(speciality, "Name", "Name");
            return View(doctor);
        }

        // GET: Doctors/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var doctor = await Context.Doctors
                .Include(d => d.CommonAvailability)
                .Include(d => d.CurrentAvailability)
                .Include(d => d.Hospital)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (doctor == null)
            {
                return NotFound();
            }

            var isAuthorized = await AuthorizationService.AuthorizeAsync(
                                                 User, doctor,
                                                 Operations.Delete);
            if (!isAuthorized.Succeeded)
            {
                return Forbid();
            }

            return View(doctor);
        }

        // POST: Doctors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var doctor = await Context.Doctors.FindAsync(id);

            var isAuthorized = await AuthorizationService.AuthorizeAsync(
                                                 User, doctor,
                                                 Operations.Delete);
            if (!isAuthorized.Succeeded)
            {
                return Forbid();
            }

            Context.Doctors.Remove(doctor);
            await Context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DoctorExists(int id)
        {
            return Context.Doctors.Any(e => e.ID == id);
        }

        public IActionResult AvailabilityCreate()
        {
            return View();
        }

        public JsonResult GetLocations(int Pincode)
        {
            
            var locations2 = from location in Context.Locations
                             where location.Pincode == Pincode
                             select new { Locality = location.Locality, Dis = location.District };
            
            return Json(locations2.ToList());
        }

    }

}