using Microsoft.AspNetCore.Identity;

namespace ProjectManagementSystem.Models
{
    public class Role : IdentityRole<int>
    {
        public Role(string roleName) : base(roleName)
        {
            
        }
        public Role()
        {

        }
    }
}
