using DrAvail.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DrAvail.Services
{
    public interface IDoctorService
    {
        List<Doctor> GetAllDocotrsAsync();

        Task<List<Doctor>> GetDocotrsByVerification(bool isVerified = false);

        Task<bool> UpdateDoctor(Doctor doctor);

        Task<bool> SendEmail(string ip, string email, string subject, string message, string adminEmail, MessageType messageType);

        bool DoctorExists(int id);

        bool DoctorExistsByOwnerID(string id);

        int GetDoctorIDByOwnerID(string ownerId);

        int GetPendingVerificationCount();
    }
}