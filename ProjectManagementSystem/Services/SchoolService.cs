using ProjectManagementSystem.Models;

namespace ProjectManagementSystem.Services
{
    public class SchoolService : ServiceBase<School>
    {
        public SchoolService(ProjectManagementDbContext context) : base(context)
        {
        }
    }
}
