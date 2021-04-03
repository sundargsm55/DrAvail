using DrAvail.Data;
using DrAvail.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DrAvail.Services
{
    public class DoctorService
    {
        private readonly ApplicationDbContext _context;
        public DoctorService()
        {
            _context = new ApplicationDbContext();
        }
        public List<Doctor> GetAllDocotrsAsync()
        {
            var doctors = _context.Doctors
                .Include(d => d.CommonAvailability)
                .Include(d => d.CurrentAvailability)
                .Include(d => d.Hospital);

            return (doctors.AsNoTracking().ToList());
        }

        public List<Doctor> GetDocotrsByVerification(bool isVerified = false)
        {
            var doctors = _context.Doctors
                .Where(d => d.IsVerified == isVerified)
                .Include(d => d.CommonAvailability)
                .Include(d => d.CurrentAvailability)
                .Include(d => d.Hospital);

            return (doctors.AsNoTracking().ToList());
        }

        public async Task<bool> UpdateDoctor(Doctor doctor)
        {
            try
            {                
                _context.Update(doctor);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DoctorExists(doctor.ID))
                {
                    return false;
                }
                else
                {
                    throw;
                }
            }
            
        }

        public bool EmailDoctor(string email, string subject, string message, Microsoft.Extensions.Configuration.IConfiguration configuration)
        {

            try
            {
                EmailSender emailSender = new EmailSender(configuration);
                emailSender.SendEmailAsync(email, subject, message);
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }

        private bool DoctorExists(int id)
        {
            return _context.Doctors.Any(e => e.ID == id);
        }

    }
}
