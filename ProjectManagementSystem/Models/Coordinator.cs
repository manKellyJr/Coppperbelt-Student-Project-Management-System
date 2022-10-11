namespace ProjectManagementSystem.Models
{
    public class Coordinator
    {
        public int CoordinatorId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string CoordinatorName => FirstName + " " + LastName;
        public int AcademicYear { get; set; }
        public School School { get; set; }
        public int SchoolId { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
    }
}
