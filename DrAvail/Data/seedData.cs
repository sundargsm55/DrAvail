using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Identity;
using DrAvail.Models;

namespace DrAvail.Data
{
    //for more information, visit https://docs.microsoft.com/en-us/aspnet/core/security/authorization/secure-data?view=aspnetcore-5.0#secure-user-data
    public static class SeedData
    {
        public static async Task Initialize(IServiceProvider serviceProvider, string testUserPw)
        {
            using (var context = new ApplicationDbContext(
        serviceProvider.GetRequiredService<DbContextOptions<ApplicationDbContext>>()))
            {
                // For sample purposes seed both with the same password.
                // The admin user can do anything

                var adminID = await EnsureUser(serviceProvider, testUserPw, "DrAvail@outlook.com");
                await EnsureRole(serviceProvider, adminID, DrAvail.Authorization.Constants.AdministratorsRole);

                //testUserPw = "admin1@Dr_avail";
                var testDoctorID = await EnsureUser(serviceProvider, testUserPw, "testdoctor@dravail.com");
                await EnsureRole(serviceProvider, testDoctorID, DrAvail.Authorization.Constants.DoctorsRole);

                /*var testUserPw1 = "user1@Dr_avail";

                var testDoctorID1 = await EnsureUser(serviceProvider, testUserPw1, "DrZ@Dravail.com");
                await EnsureRole(serviceProvider, testDoctorID, DrAvail.Authorization.Constants.DoctorsRole);

                var doctor = await context.Doctors.FirstOrDefaultAsync(d => d.EmailId.Equals("DrZ@Dravail.com"));
                doctor.OwnerID = testDoctorID1;
                try
                {
                    context.Update(doctor);
                    await context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    throw;
                }*/
                SeedDB(context, adminID);
            }
        }

        private static async Task<string> EnsureUser(IServiceProvider serviceProvider, string testUserPw, string userName)
        {
            var userManager = serviceProvider.GetService<UserManager<ApplicationUser>>();

            var user = await userManager.FindByNameAsync(userName);
            if (user == null)
            {
                user = new ApplicationUser
                {
                    UserName = userName,
                    Email = userName,
                    EmailConfirmed = true
                };
                await userManager.CreateAsync(user, testUserPw);
            }

            if (user == null)
            {
                throw new Exception("The password is probably not strong enough!");
            }

            return user.Id;
        }

        private static async Task<IdentityResult> EnsureRole(IServiceProvider serviceProvider,
                                                              string uid, string role)
        {
            IdentityResult IR = null;
            var roleManager = serviceProvider.GetService<RoleManager<IdentityRole>>();

            if (roleManager == null)
            {
                throw new Exception("roleManager null");
            }

            if (!await roleManager.RoleExistsAsync(role))
            {
                IR = await roleManager.CreateAsync(new IdentityRole(role));
            }

            var userManager = serviceProvider.GetService<UserManager<ApplicationUser>>();

            var user = await userManager.FindByIdAsync(uid);

            if (user == null)
            {
                throw new Exception("The testUserPw password was probably not strong enough!");
            }

            IR = await userManager.AddToRoleAsync(user, role);

            return IR;
        }

        public static void SeedDB(ApplicationDbContext context, string adminID)
        {
            if (context.Doctors.Any())
            {
                return;   // DB has been seeded
            }

        }
    }
}
