using ProjectManagementSystem.Models;

namespace ProjectManagementSystem.Interfaces
{
    public interface ICourse
    {
        public IEnumerable<Course> GetCourseDetails();
    }
}
