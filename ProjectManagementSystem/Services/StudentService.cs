using Microsoft.EntityFrameworkCore;
using ProjectManagementSystem.Models;

namespace ProjectManagementSystem.Services
{
    public class StudentService : ServiceBase<Student>
    {
        public StudentService(ProjectManagementDbContext context) : base(context)
        {
        }
        public IEnumerable<Student> GetStudentDetails()
        {
            return Entities.Include(e => e.Programme).Include(c=>c.Supervisor).ToList();
        }
    }
}
