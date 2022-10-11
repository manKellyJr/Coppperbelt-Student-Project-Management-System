using ProjectManagementSystem.Models;

namespace ProjectManagementSystem.Interfaces
{
    public interface IDepartment
    {
        public IEnumerable<Department> GetDepartmentDetails();
    }
}
