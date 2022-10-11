namespace ProjectManagementSystem.Models
{
    public class Group
    {
        public int GroupId { get; set; }
        public string GroupName { get; set; }
        public int SupervisorId { get; set; }
        public IEnumerable<Project>? Projects { get; set; }
        public Supervisor Supervisor { get; set; }
        public List<Student>? Students { get; set; }
    }
}
