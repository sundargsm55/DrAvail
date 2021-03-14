using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DrAvail.Data;
using DrAvail.Models;

namespace DrAvail.Controllers
{
    public class DoctorsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DoctorsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Doctors
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Doctors.Include(d => d.CommonAvailability).Include(d => d.CurrentAvailability).Include(d => d.Hospital);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Doctors/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var doctor = await _context.Doctors
                .Include(d => d.CommonAvailability)
                .Include(d => d.CurrentAvailability)
                .Include(d => d.Hospital)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (doctor == null)
            {
                return NotFound();
            }

            return View(doctor);
        }

        // GET: Doctors/Create
        public IActionResult Create()
        {
            ViewData["CommonAvaliabilityID"] = new SelectList(_context.Avaliabilities, "ID", "ID");
            ViewData["CurrentAvaliabilityID"] = new SelectList(_context.Avaliabilities, "ID", "ID");
            ViewData["HospitalID"] = new SelectList(_context.Hospitals, "ID", "Address");
            return View();
        }

        // POST: Doctors/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Name,RegNumber,Speciality,Degree,Age,Gender,Practice,Experience,IsVerified,Summary,City,District,EmailId,PhoneNumber,HospitalID,CommonAvaliabilityID,CurrentAvaliabilityID")] Doctor doctor)
        {
            if (ModelState.IsValid)
            {
                _context.Add(doctor);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CommonAvaliabilityID"] = new SelectList(_context.Avaliabilities, "ID", "ID", doctor.CommonAvaliabilityID);
            ViewData["CurrentAvaliabilityID"] = new SelectList(_context.Avaliabilities, "ID", "ID", doctor.CurrentAvaliabilityID);
            ViewData["HospitalID"] = new SelectList(_context.Hospitals, "ID", "Address", doctor.HospitalID);
            return View(doctor);
        }

        // GET: Doctors/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var doctor = await _context.Doctors.FindAsync(id);
            if (doctor == null)
            {
                return NotFound();
            }
            ViewData["CommonAvaliabilityID"] = new SelectList(_context.Avaliabilities, "ID", "ID", doctor.CommonAvaliabilityID);
            ViewData["CurrentAvaliabilityID"] = new SelectList(_context.Avaliabilities, "ID", "ID", doctor.CurrentAvaliabilityID);
            ViewData["HospitalID"] = new SelectList(_context.Hospitals, "ID", "Address", doctor.HospitalID);
            return View(doctor);
        }

        // POST: Doctors/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Name,RegNumber,Speciality,Degree,Age,Gender,Practice,Experience,IsVerified,Summary,City,District,EmailId,PhoneNumber,HospitalID,CommonAvaliabilityID,CurrentAvaliabilityID")] Doctor doctor)
        {
            if (id != doctor.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(doctor);
                    await _context.SaveChangesAsync();
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
            ViewData["CommonAvaliabilityID"] = new SelectList(_context.Avaliabilities, "ID", "ID", doctor.CommonAvaliabilityID);
            ViewData["CurrentAvaliabilityID"] = new SelectList(_context.Avaliabilities, "ID", "ID", doctor.CurrentAvaliabilityID);
            ViewData["HospitalID"] = new SelectList(_context.Hospitals, "ID", "Address", doctor.HospitalID);
            return View(doctor);
        }

        // GET: Doctors/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var doctor = await _context.Doctors
                .Include(d => d.CommonAvailability)
                .Include(d => d.CurrentAvailability)
                .Include(d => d.Hospital)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (doctor == null)
            {
                return NotFound();
            }

            return View(doctor);
        }

        // POST: Doctors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var doctor = await _context.Doctors.FindAsync(id);
            _context.Doctors.Remove(doctor);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DoctorExists(int id)
        {
            return _context.Doctors.Any(e => e.ID == id);
        }
    }
}
