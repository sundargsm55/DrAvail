using DrAvail.Data;
using DrAvail.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static DrAvail.Services.Utilities;

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

        public async Task<bool> SendEmail(string ip, string email, string subject, string message, string adminEmail, MessageType messageType = MessageType.UserToAdmin)
        {

            try
            {

                await _emailSender.SendEmailAsync(email, subject, message);
                Message messageObj = new Message
                {
                    IP = ip,
                    Email = email,
                    Subject = subject,
                    MessageText = message,
                    AdminEmail = adminEmail,
                    MessageType = messageType,
                    DateSent = DateTime.Now
                };
                _context.Messages.Add(messageObj);
                await _context.SaveChangesAsync();
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

        public int GetPendingVerificationCount()
        {
            return _context.Doctors.Where(d => d.IsVerified == false).Count();
        }

        public bool DoctorExistsByRegistrationNumber(string registrationNumber,int id)
        {
            return _context.Doctors.Any(d => d.RegNumber.Equals(registrationNumber) && d.ID!=id);
        }

        public bool VerifyMorningTiming(Availability.Timings timings, out string errorMessage, string textToAdd)
        {
            textToAdd += " Morning";
            #region AnotherMethod
            /*string[] morningHours = { "00", "01", "02", "03", "04", "05", "06", "07", "08", "09", "10", "11", "12", "13", "14" };
            string[] minutes = { "00", "15", "30", "45" };
            //string[] eveningHours = { "14", "15", "16", "17", "18", "19", "20", "21", "22", "23", "00" };

            //Common Morning TImings
            if (morningHours.Contains(timings.MorningStartHour)
                && minutes.Contains(timings.MorningStartMinute)
                && morningHours.Contains(timings.MorningEndHour)
                && minutes.Contains(timings.MorningEndMinute))
            {
                int morningStartHourIndex = Array.IndexOf(morningHours, timings.MorningStartHour);
                int morningStartMinuteIndex = Array.IndexOf(minutes, timings.MorningStartMinute);
                int morningEndHourIndex = Array.IndexOf(morningHours, timings.MorningEndHour);
                int morningEndMinuteIndex = Array.IndexOf(minutes, timings.MorningEndMinute);


                if (morningEndHourIndex > morningStartHourIndex)
                {
                    if (morningEndHourIndex == morningHours.Length - 1)
                    {
                        if (morningEndMinuteIndex != 0)
                        {
                            ViewBag.ErrorMessage = "Morning End Time must not exceed 14:00";
                            return false;
                            //timings.MorningEndMinute = minutes[0];
                        }
                    }
                }
                else if (morningStartHourIndex == morningEndHourIndex)
                {
                    if (morningEndMinuteIndex <= morningStartMinuteIndex)
                    {
                        ViewBag.ErrorMessage = "Morning End Minute cannot be less than (or) same as Morning Start Minute";
                        return false;
                    }
                }
                else
                {
                    ViewBag.ErrorMessage = "Morning End hour should not be less than Morning start hour";
                    return false;
                }

            }
            else
            {
                ViewBag.ErrorMessage = "Select valid timing options";
                return false;
            }
            return true*/
            ;
            #endregion
            return VerifyTimings(startTime: timings.MorningStartTime,
                        endTime: timings.MorningEndTime, textToAdd, out errorMessage, minHour: 00, maxHour: 14);

        }

        public bool VerifyEveningTiming(Availability.Timings timings, out string errorMessage, string textToAdd)
        {
            textToAdd += " Evening";
            #region AnotherMethod
            /*//string[] morningHours = { "00", "01", "02", "03", "04", "05", "06", "07", "08", "09", "10", "11", "12", "13", "14" };
            string[] minutes = { "00", "15", "30", "45" };
            string[] eveningHours = { "14", "15", "16", "17", "18", "19", "20", "21", "22", "23", "00" };

            if (eveningHours.Contains(timings.EveningStartHour)
                    && minutes.Contains(timings.EveningStartMinute)
                    && eveningHours.Contains(timings.EveningEndHour)
                    && minutes.Contains(timings.EveningEndMinute))
            {
                int eveningStartHourIndex = Array.IndexOf(eveningHours, timings.EveningStartHour);
                int eveningStartMinuteIndex = Array.IndexOf(minutes, timings.EveningStartMinute);
                int eveningEndHourIndex = Array.IndexOf(eveningHours, timings.EveningEndHour);
                int eveningEndMinuteIndex = Array.IndexOf(minutes, timings.EveningEndMinute);


                if (eveningEndHourIndex < eveningStartHourIndex)
                {
                    ViewBag.ErrorMessage = "Evening End hour should not be less than Evening start hour";
                    return false;
                }
                else if (eveningStartHourIndex == eveningEndHourIndex)
                {
                    if (eveningEndMinuteIndex <= eveningStartMinuteIndex)
                    {
                        ViewBag.ErrorMessage = "Evening End Minute cannot be less than (or) same as Evening Start Minute";
                        return false;
                    }
                }
            }
            else
            {
                ViewBag.ErrorMessage = "Select valid timing options";
                return false;
            }
            return true;*/
            #endregion

            return VerifyTimings(startTime: timings.EveningStartTime,
                        endTime: timings.EveningEndTime, textToAdd, out errorMessage, minHour: 14, maxHour: 23, maxMintute: 45);
        }

        public bool VerifyMinutes(Availability.Timings timings, out string errorMessage)
        {
            string[] minutes = { "00", "15", "30", "45" };

            if (minutes.Contains(timings.MorningStartMinute)
                    && minutes.Contains(timings.MorningEndMinute)
                    && minutes.Contains(timings.EveningStartMinute)
                    && minutes.Contains(timings.EveningEndMinute))
            {
                errorMessage = "";
                return true;
            }
            errorMessage = "Invalid Minutes";
            return false;
        }

        public bool VerifyTimings(DateTime startTime, DateTime endTime, string msg, out string errorMessage, int minHour, int maxHour, int minMintue = 00, int maxMintute = 00)
        {
            DateTime Now = DateTime.Now;

            DateTime minTime = new DateTime(year: Now.Year, month: Now.Month, day: Now.Day, hour: minHour, minute: minMintue, second: 0);
            DateTime maxTime = new DateTime(year: Now.Year, month: Now.Month, day: Now.Day, hour: maxHour, minute: maxMintute, second: 0);

            if (CompareDateTime(startTime, minTime) != DateTimeRelation.IsEarlier)
            {
                if (CompareDateTime(endTime, maxTime) != DateTimeRelation.IsLater)
                {
                    if (CompareDateTime(startTime, endTime) == DateTimeRelation.IsEarlier)
                    {
                        errorMessage = "";
                        return true;
                    }
                    errorMessage = $"{msg} Start time should not be greater than {msg} End Time";
                }
                else
                {
                    errorMessage = $"{msg} End Time must not exceed {maxHour:D2}:{maxMintute:D2}";
                }
            }
            else
            {
                errorMessage = $"{msg} Start Time cannot be lesser than {minHour:D2}:{minMintue:D2}";
            }
            return false;
        }

        public async Task AddNewDoctor(string OwnerId, string email)
        {
            Doctor doctor = new()
            {
                OwnerID = OwnerId,
                Name = "Please enter your Full Name",
                RegNumber = "Please enter your Medical Registration Number",
                Speciality = Speciality.GeneralPhysician,
                Degree = " ",
                Age = 26,
                DateOfBirth = DateTime.Now.AddYears(-26),
                Gender = Gender.Male,
                Practice = Practice.Government,
                Experience = 0,
                IsVerified = false,
                City = "Please select City",
                District = District.Virudhunagar,
                Pincode = 123456,
                EmailId = email,
                PhoneNumber = "9000000000",
                DateCreated = DateTime.Now,
                HospitalID = _context.Hospitals.LastOrDefault().ID,
                CommonAvaliabilityID = new()
            };

            _context.Doctors.Add(doctor);
            await _context.SaveChangesAsync();
        }

    }
}
