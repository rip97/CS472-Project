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
                await CreateRole(serviceProvider, "customer");
                await AddUserToRole(serviceProvider, adminID, "admin");
                
                SeedPlays(context);
                SeedSeats(context);
            }
        }

        private static void SeedSeats(ApplicationDbContext context)
        {   
            if(!context.Seat.Any())
            {
                foreach (var item in context.Play)
                {
                    for (int i = 0; i < 80; i++)
                    {
                        context.Seat.Add(
                            new Seat()
                            {
                                PlayId = item.PlayId,
                                SeatNumber = i + 1,
                                IsSold = 0,
                                Price = 25,
                                Play = item

                            }); ;


                    }

                }

                context.SaveChanges();
            }
            
        }
        private static void SeedPlays(ApplicationDbContext context)
        {   

            if(!context.Play.Any())
            {
                    context.Play.AddRange
                (
                    new Play() { PlayName = "Coco", PlayDate = new DateTime(2022, 8, 8), PlayTime = new DateTime(2022, 8, 8, 13, 0, 0) },
                    new Play() { PlayName = "Soul", PlayDate = new DateTime(2022, 6, 10), PlayTime = new DateTime(2022, 6, 10, 13, 0, 0) },
                    new Play() { PlayName = "Soul", PlayDate = new DateTime(2022, 6, 10), PlayTime = new DateTime(2022, 6, 10, 19, 0, 0) }

                );

                context.SaveChanges();
            }
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
