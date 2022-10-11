namespace ProjectManagementSystem.Models
{
    public class Programme
    {
        public int ProgrammeId { get; set; }
        public string ProgrammeName { get; set; }
        public string ProgrammeCode { get; set; }
        public Department Department { get; set; }
        public int DepartmentId { get; set; }
        public IEnumerable<Student> Students { get; set; }
        public IEnumerable<Course> Courses { get; set; }

    }
}
