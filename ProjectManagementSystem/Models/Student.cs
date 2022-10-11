namespace ProjectManagementSystem.Models
{
    public class Student
    {
        public int StudentId { get; set; }
        public string StudentIDNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string StudentName => FirstName +" "+ LastName;
        public int YearOfStudy { get; set; }
        public Programme Programme { get; set; }
        public int ProgrammeId { get; set; }
        public IEnumerable<Project> Projects { get; set; }
        public Supervisor Supervisor { get; set; }
        public int SupervisorId { get; set; }
        public int? GroupId { get; set; }
        public Group? Group { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
    }
}
