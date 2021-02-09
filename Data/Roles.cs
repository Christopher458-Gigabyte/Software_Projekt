using Software_Projekt.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Software_Projekt.Authorization;

 
namespace Software_Projekt.Data
{
    public static class Roles
    {
        public static async Task Initialize(IServiceProvider serviceProvider, string testUserPw)
        {
            using (var context = new ApplicationDbContext(
                serviceProvider.GetRequiredService<DbContextOptions<ApplicationDbContext>>()))
            {
                // For sample purposes seed both with the same password.
                // Password is set with the following:
                // dotnet user-secrets set SeedUserPW <pw>
                // The admin user can do anything

                var DekanID = await EnsureUser(serviceProvider, testUserPw, "dekan@outlook.de");
                await EnsureRole(serviceProvider, DekanID, Constants.DekanRole);

               

                // allowed user can create and edit contacts that they create
                var DozentID = await EnsureUser(serviceProvider, testUserPw, "dozent@outlook.de");
                await EnsureRole(serviceProvider, DozentID, Constants.DozentRole);

                var AdminID = await EnsureUser(serviceProvider, testUserPw, "admin@outlook.de");
                await EnsureRole(serviceProvider, AdminID, Constants.AdminRole);

                SeedDB(context, DekanID, DozentID, AdminID);
            }
        }

        private static async Task<string> EnsureUser(IServiceProvider serviceProvider,
                                                    string testUserPw, string UserName)
        {
            var userManager = serviceProvider.GetService<UserManager<IdentityUser>>();

            var user = await userManager.FindByNameAsync(UserName);
            if (user == null)
            {
                user = new IdentityUser
                {
                    UserName = UserName,
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

            var userManager = serviceProvider.GetService<UserManager<IdentityUser>>();

            var user = await userManager.FindByIdAsync(uid);

            if (user == null)
            {
                throw new Exception("The testUserPw password was probably not strong enough!");
            }

            IR = await userManager.AddToRoleAsync(user, role);

            return IR;
        }

        public static void SeedDB(ApplicationDbContext context, string DekanID, string dozentID, string adminID)
        {
            if (context.Modul.Any())
            {
                return;   // DB has been seeded
            }

            context.Modul.AddRange(
              new Modul
              {
                  ModulId = 1,
                  Name = "chris",
                  ModulverantwortlichenId = 1,
                  ModulverantwortlicheR = "WA",
                  Semester = 10999,
                  WorkloadH = 222,
                  Semesterwochenstunden = 3,
                  Credits = 6,
                  Turnus = "WS",
                  CuriculareZuordnung = "WI",
                  Lernziele = "INTERNET",
                  Schlüsselqualifikationen = "INTERNET",
                  InhaltilicheBeschreibung = "INTERNET",
                  Unterrichtsform = "INTERNET",
                  Prüfungsart = "INTERNET",
                  Prüfungsform = "INTERNET",
                  Verbindlichkeit = "INTERNET",
                  Literaturangaben = "INTERNET",
                  ModulhandbuchId = 22,
                
                  OwnerID = DekanID
              }
             );
            context.SaveChanges();
        }

    }
}