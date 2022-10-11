using Microsoft.EntityFrameworkCore;
using ProjectManagementSystem.Models;

namespace ProjectManagementSystem.Services
{
    public class CourseService : ServiceBase<Course>
    {
        public CourseService(ProjectManagementDbContext context) : base(context)
        {
        }

        public IEnumerable<Course> GetCourseDetails()
        {
            return Entities.Include(e => e.Programme).ToList();
        }
    }
}
