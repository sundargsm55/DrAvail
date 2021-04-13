using DrAvail.Data;
using DrAvail.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DrAvail.Services
{
    public class DoctorService: IDoctorService
    {
        private readonly ApplicationDbContext _context;
        private readonly EmailSender _emailSender;
        public DoctorService(ApplicationDbContext context, IConfiguration configuration)
        {
            _context = context;
            _emailSender = new EmailSender(configuration);
        }
        public List<Doctor> GetAllDocotrsAsync()
        {
            var doctors = _context.Doctors
                .OrderBy(d => d.Name)
                .Include(d => d.CommonAvailability)
                .Include(d => d.CurrentAvailability)
                .Include(d => d.Hospital);

            return (doctors.AsNoTracking().ToList());
        }

        public async Task<List<Doctor>> GetDocotrsByVerification(bool isVerified = false)
        {
            var doctors = _context.Doctors
                .Where(d => d.IsVerified == isVerified)
                .OrderBy( d => d.Name)
                .Include(d => d.CommonAvailability)
                .Include(d => d.CurrentAvailability)
                .Include(d => d.Hospital);

            return (await doctors.AsNoTracking().ToListAsync());
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

        public bool SendEmail(string email, string subject, string message)
        {

            try
            {
                _emailSender.SendEmailAsync(email, subject, message);
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }

        public bool DoctorExists(int id)
        {
            return _context.Doctors.Any(e => e.ID == id);
        }

       
        public bool DoctorExistsByOwnerID(string ownerId)
        {
            return _context.Doctors.Any(e => e.OwnerID == ownerId);
        }

        public int GetDoctorIDByOwnerID(string ownerId)
        {
            if (DoctorExistsByOwnerID(ownerId))
            {
                return _context.Doctors.FirstOrDefault(d => d.OwnerID == ownerId).ID;
            }
            return 0;
        }
    }
}
