using Microsoft.EntityFrameworkCore;
using ProjectManagementSystem.Models;

namespace ProjectManagementSystem.Services
{
    public class DepartmentService : ServiceBase<Department>
    {
        public DepartmentService(ProjectManagementDbContext context) : base(context)
        {

        }

        public IEnumerable<Department> GetDepartmentDetails()
        {
            return Entities.Include(e => e.School).Include(c => c.Programmes).ToList();
        }
    }
}
