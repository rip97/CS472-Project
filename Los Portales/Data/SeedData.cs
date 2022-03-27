using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Los_Portales.Models;


namespace Los_Portales.Data
{
    public static class SeedData
    {
        public static async Task Initialize(IServiceProvider serviceProvider, string testUserPw)
        {
            using (var context = new ApplicationDbContext(
                serviceProvider.GetRequiredService<DbContextOptions<ApplicationDbContext>>()))
            {
                // Password is set with the following at the command line:
                // dotnet user-secrets set SeedUserPW <pw>
                // password is 2020Theater!

                // sets up a test admin users 
                var adminID = await CreateUser(serviceProvider, testUserPw, "admin");
                await CreateRole(serviceProvider, "admin");
                await AddUserToRole(serviceProvider, adminID, "admin");
                SeedDB(context, adminID);
                //SeedDB(context);
            }
        }

        private static void SeedDB(ApplicationDbContext context, string adminID)
        {
            //TODO: Seed database here if needed.
        }

        // creates the theater admin owner 
        private static void SeedDB(ApplicationDbContext context)
        {   
            /*
            if (context.Admin.Any())
            {
                return;   // DB has been seeded
            }
            else
            {
                context.Admin.AddRange(
                    new Admin
                    {
                        FirstName = "V",
                        LastName = "Edurado",
                        UserName = "vEdurado",
                        Role = "admin"
                    });
            }*/
        }

        public static async Task<string> CreateUser(IServiceProvider serviceProvider,
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

        private static async Task<IdentityResult> CreateRole(IServiceProvider serviceProvider, string role)
        {
            var roleManager = serviceProvider.GetService<RoleManager<IdentityRole>>();

            if (roleManager == null)
            {
                throw new Exception("roleManager null");
            }

            if (!await roleManager.RoleExistsAsync(role))
            {
                return await roleManager.CreateAsync(new IdentityRole(role));
            }

            return null;
        }

        public static async Task<IdentityResult> AddUserToRole(IServiceProvider serviceProvider, string uid, string role)
        {
            var userManager = serviceProvider.GetService<UserManager<IdentityUser>>();

            var user = await userManager.FindByIdAsync(uid);

            if (user == null)
            {
                throw new Exception("The testUserPw password was probably not strong enough!");
            }

            return await userManager.AddToRoleAsync(user, role);
        }

    }
}
