using Microsoft.EntityFrameworkCore;
using ProjectManagementSystem.Models;

namespace ProjectManagementSystem.Services
{
    public class CoordinatorService : ServiceBase<Coordinator>
    {
        public CoordinatorService(ProjectManagementDbContext context) : base(context)
        {
        }
        public IEnumerable<Coordinator> GetCoordinatorDetails()
        {
            return Entities.Include(e => e.School).ToList();
        }
        public string GetName(string email)
        {
            var user = Entities.Include(i => i.User).FirstOrDefault(x => x.User.Email==email);
            return user.FirstName +" "+ user.LastName;
        }
    }
    
}
