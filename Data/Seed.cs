using System;
namespace MasterProject.Data
{
	public class Seed
	{
        public static async Task SeedRoles(IApplicationBuilder ApplicationB)
        {
            using (var Scope = ApplicationB.ApplicationServices.CreateScope())
            {
                {
                    RoleManager<IdentityRole> _roleManager = Scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
                    UserManager<AppUser> _userManager = Scope.ServiceProvider.GetRequiredService<UserManager<AppUser>>();
                    if (!await _roleManager.RoleExistsAsync(Roles.user))
                    {
                        await _roleManager.CreateAsync(new IdentityRole(Roles.user));
                    }
                    if (!await _roleManager.RoleExistsAsync(Roles.admin))
                    {
                        await _roleManager.CreateAsync(new IdentityRole(Roles.admin));
                    }

                    var context = Scope.ServiceProvider.GetRequiredService<AppDbContext>();
                    var SA = new AppUser
                    {

                        Id = "956f0873-10d2-4cb7-8d9c-660705e1ff66",
                        Email = "bahiraeimmai@gmail.com",
                        UserName = "bahiraeimmai@gmail.com",
                        toDoModels = new List<ToDoModel>
                        {
                            new ToDoModel
                            {
                                Title = "Math exam",
                                Description = "lorem ispum for math",
                                DueTime=DateTime.SpecifyKind( new DateTime(2025,9,25), DateTimeKind.Utc),
                                AppUserId="956f0873-10d2-4cb7-8d9c-660705e1ff66"
                            },

                            new ToDoModel
                            {
                                Title = "Physics exam",
                                Description = "lorem ispum for physics",
                                DueTime= DateTime.SpecifyKind( new DateTime(2025,11,30), DateTimeKind.Utc),
                                AppUserId="956f0873-10d2-4cb7-8d9c-660705e1ff66"
                            },

                            new ToDoModel
                            {
                                Title = "Biology exam",
                                Description = "lorem ispum for Biology",
                                DueTime= DateTime.SpecifyKind( new DateTime(2025,8,5), DateTimeKind.Utc),
                                AppUserId="956f0873-10d2-4cb7-8d9c-660705e1ff66"
                            }

                        }
                        
                    };

                    if (await _userManager.FindByEmailAsync(SA.Email) is null)
                    {

                        await _userManager.CreateAsync(SA, "Password_123#");
                        await _userManager.AddToRoleAsync(SA, Roles.admin);
                    }
                    context.SaveChanges();

                }


            }
        }
    }
}

