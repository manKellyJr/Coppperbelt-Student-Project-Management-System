using Microsoft.EntityFrameworkCore;
using ProjectManagementSystem.Models;

namespace ProjectManagementSystem.Services
{
    public class GroupService :ServiceBase<Group>
    {
        private IEnumerable<Student> students = new List<Student>();
        private IEnumerable<Supervisor> supervisors = new List<Supervisor>();
        StudentService studentService;
        SupervisorService supervisorService;
        public GroupService(ProjectManagementDbContext context, StudentService studentService, SupervisorService supervisorService) : base(context)
        {
            this.studentService = studentService;
            this.supervisorService = supervisorService;

        }
        public IEnumerable<Group> GetGroupDetails()
        {
            students = studentService.GetStudentDetails().ToList();
            supervisors = supervisorService.GetAll().ToList();
            return Entities.Include(e => e.Projects).ToList();
        }
    }
}
