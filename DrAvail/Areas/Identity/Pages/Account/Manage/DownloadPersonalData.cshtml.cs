using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using DrAvail.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace DrAvail.Areas.Identity.Pages.Account.Manage
{
    public class DownloadPersonalDataModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<DownloadPersonalDataModel> _logger;
        private readonly Data.ApplicationDbContext _context;

        public DownloadPersonalDataModel(
            UserManager<ApplicationUser> userManager,
            ILogger<DownloadPersonalDataModel> logger, Data.ApplicationDbContext context)
        {
            _userManager = userManager;
            _logger = logger;
            _context = context;
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            _logger.LogInformation("User with ID '{UserId}' asked for their personal data.", _userManager.GetUserId(User));

            // Only include personal data for download
            var personalData = new Dictionary<string, string>();
            var personalDataProps = typeof(ApplicationUser).GetProperties().Where(
                            prop => Attribute.IsDefined(prop, typeof(PersonalDataAttribute)));
            foreach (var p in personalDataProps)
            {
                personalData.Add(p.Name, p.GetValue(user)?.ToString() ?? "null");
            }
            var doctor = await _context.Doctors.Include(d => d.CommonAvailability)
                .Include(d => d.CurrentAvailability)
                .Include(d => d.CurrentAvailability.Hospital)
                .Include(d => d.Hospital)
                .FirstOrDefaultAsync(d => d.OwnerID == user.Id);
            var doctorDataProps = typeof(Doctor).GetProperties();
            string propertyName;
            try
            {
                object[] customAttribute;
                foreach (var d in doctorDataProps)
                {
                    customAttribute = d.GetCustomAttributes(typeof(System.ComponentModel.DataAnnotations.DisplayAttribute), true);
                    if (customAttribute.Length > 0)
                    {
                        propertyName = customAttribute.Cast<System.ComponentModel.DataAnnotations.DisplayAttribute>().Single().GetName();
                        personalData.Add(propertyName, d.GetValue(doctor)?.ToString() ?? "null");
                    }
                    else
                    {
                        continue;
                    }
                }

            }
            catch (Exception e)
            {
                _logger.LogError(e.Message, e);
            }

            var logins = await _userManager.GetLoginsAsync(user);
            foreach (var l in logins)
            {
                personalData.Add($"{l.LoginProvider} external login provider key", l.ProviderKey);
            }
            personalData.Remove("Id");
            Response.Headers.Add("Content-Disposition", "attachment; filename=PersonalData.json");
            return new FileContentResult(JsonSerializer.SerializeToUtf8Bytes(personalData), "application/json");
        }
    }
}
