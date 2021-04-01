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

namespace DrAvail.Controllers
{
    public class HospitalsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IConfiguration _configuration;

        public HospitalsController(ApplicationDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        // GET: Hospitals
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

            IQueryable<Hospital> hospitalsIQ = _context.Hospitals.Include(h => h.Doctors);

            if (!string.IsNullOrEmpty(searchString))
            {
                hospitalsIQ = hospitalsIQ.Where(d => d.Name.Contains(searchString));
            }

            var pageSize = _configuration.GetValue("PageSize", 4); //Sets pageSize to 3 from Configuration, 4 if configuration fails.
            
            return View(await DrAvail.Services.PaginatedList<Hospital>.CreateAsync(
                hospitalsIQ.AsNoTracking(), pageIndex ?? 1, pageSize));

            //return View(await _context.Hospitals.Include(h => h.Doctors).ToListAsync());
        }

        // GET: Hospitals/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hospital = await _context.Hospitals
                .Include(h => h.Doctors)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (hospital == null)
            {
                return NotFound();
            }

            return View(hospital);
        }

        // GET: Hospitals/Create
        public IActionResult Create()
        {
            var values = from HospitalType H in Enum.GetValues(typeof(HospitalType))
                         select new { ID = (int)H, Name = H.ToString() };
            ViewBag.HospitalType = new SelectList(values, "ID", "Name"); ;

            var district = from District d in Enum.GetValues(typeof(District))
                         select new { ID = (int)d, Name = d.ToString() };
                       
            ViewBag.Districts = new SelectList(district, "ID", "Name");
            return View();
        }

        // POST: Hospitals/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Name,Type,Address,City,District,EmailId,PhoneNo")] Hospital hospital)
        {
            if (ModelState.IsValid)
            {
                _context.Add(hospital);
                await _context.SaveChangesAsync();
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

            var hospital = await _context.Hospitals.FindAsync(id);
            if (hospital == null)
            {
                return NotFound();
            }
            return View(hospital);
        }

        // POST: Hospitals/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Name,Type,Address,City,District,EmailId,PhoneNo")] Hospital hospital)
        {
            if (id != hospital.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(hospital);
                    await _context.SaveChangesAsync();
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

            var hospital = await _context.Hospitals
                .FirstOrDefaultAsync(m => m.ID == id);
            if (hospital == null)
            {
                return NotFound();
            }

            return View(hospital);
        }

        // POST: Hospitals/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var hospital = await _context.Hospitals.FindAsync(id);
            _context.Hospitals.Remove(hospital);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool HospitalExists(int id)
        {
            return _context.Hospitals.Any(e => e.ID == id);
        }
    }
}
