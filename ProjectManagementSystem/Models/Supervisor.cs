namespace ProjectManagementSystem.Models
{
    public class Supervisor
    {
        public int SupervisorId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string SupervisorName => FirstName +" " + LastName;
        public string SupervisorManNumber { get; set; }
        public IEnumerable<Student> Students { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
    }
}
