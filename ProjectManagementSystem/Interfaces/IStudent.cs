using ProjectManagementSystem.Models;

namespace ProjectManagementSystem.Interfaces
{
    public interface IStudent
    {
        public IEnumerable<Student> GetStudentDetails();
    }
}
