using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ThedoorCode.Authorization;
using ThedoorCode.Data;

namespace ThedoorCode.Models
{
    public static class SeedData2
    {
        public static async Task Initialize(IServiceProvider serviceProvider, string testUserPw)
        {
            using (var context = new UserDbContext(
                serviceProvider.GetRequiredService<DbContextOptions<UserDbContext>>()))
            {
                var adminID = await EnsureUser(serviceProvider, testUserPw, "ida@gmail.com");
                await EnsureRole(serviceProvider, adminID, Constants.ContactAdministratorsRole);

                var managerID = await EnsureUser(serviceProvider, testUserPw, "tresor@firework.co.za");
                await EnsureRole(serviceProvider, managerID, Constants.ContactManagersRole);

                SeedDB(context, adminID);
            }
        }

        private static void SeedDB(UserDbContext context, string adminID)
        {
            if (context.UserModels.Any())
            {
                return; // DB has been seeded
            }
            context.UserModels.AddRange(
                new UserModel {
                    Name = "Tresor Raul",
                    Gender = "Male",
                    Age = 38,
                    Qualification = "Chemical Engineer",
                    Email = "tshims79@gmail.com",
                    PhotoUrl = "",
                    Status = UserStatus.Approved,
                    OwnerID = adminID
                },
                new UserModel
                {
                    Name = "Tresor Raul",
                    Gender = "Male",
                    Age = 38,
                    Qualification = "Chemical Engineer",
                    Email = "tresor@fireworkx.com",
                    PhotoUrl = "",
                    Status = UserStatus.Approved,
                    OwnerID = adminID
                }) ;
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
            var roleManager = serviceProvider.GetService<RoleManager<IdentityRole>>();

            if (roleManager == null)
            {
                throw new Exception("roleManager null");
            }

            IdentityResult IR;
            if (!await roleManager.RoleExistsAsync(role))
            {
                IR = await roleManager.CreateAsync(new IdentityRole(role));
            }

            var userManager = serviceProvider.GetService<UserManager<IdentityUser>>();

            //if (userManager == null)
            //{
            //    throw new Exception("userManager is null");
            //}

            var user = await userManager.FindByIdAsync(uid);

            if (user == null)
            {
                throw new Exception("The testUserPw password was probably not strong enough!");
            }

            IR = await userManager.AddToRoleAsync(user, role);

            return IR;
        }
    }
}
