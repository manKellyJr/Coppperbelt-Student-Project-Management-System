using Microsoft.AspNetCore.Identity;

using ProjectManagementSystem.Interfaces;
using ProjectManagementSystem.Models;

namespace ProjectManagementSystem.Services
{
    public class UserService : IUserService
    {   readonly UserManager<User> userManager;
        readonly RoleManager<Role> roleManager;
        public UserService(UserManager<User> userManager, RoleManager<Role> roleManager)
        {
            this.userManager=userManager;
            this.roleManager=roleManager;
        }
    
        public async Task<int> AddUser(RegisterRequest register)
        {
            try
            {
                var role = await roleManager.FindByNameAsync(register.Role);
                if (role==null)
                {
                    await roleManager.CreateAsync(new Role(register.Role));
                }
                else { }
                var user = new User() { Email = register.Email, UserName = register.Email, EmailConfirmed=true };
                await userManager.CreateAsync(user,register.Password);
                await userManager.AddToRoleAsync(user, register.Role);
                return Convert.ToInt32(await userManager.GetUserIdAsync(user));

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message+e.StackTrace);
                return 0;
            }
        }

        public async Task<bool> Delete(string userName)
        {
           var user = await userManager.FindByNameAsync(userName);
            if(user==null)
            {
                return false;
            }
            return (await userManager.DeleteAsync(user)).Succeeded;
        }
    }
}
