
using Microsoft.EntityFrameworkCore;

using ProjectManagementSystem.Models;

namespace ProjectManagementSystem.Services
{
    public class SupervisorService : ServiceBase<Supervisor>
    {
        public SupervisorService(ProjectManagementDbContext context) : base(context)
        {
        }
        public string GetName(string email)
        {
            var user = Entities.Include(i=>i.User).FirstOrDefault(x=>x.User.Email==email);
            return user.FirstName +" "+ user.LastName;
        }
    }
}

