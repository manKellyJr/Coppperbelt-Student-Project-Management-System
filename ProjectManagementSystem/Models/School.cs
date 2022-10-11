namespace ProjectManagementSystem.Models
{
    public class School
    {
        public int SchoolId { get; set; }
        public string SchoolName { get; set; }
        public string Campus { get; set; }
        public IEnumerable<Department> Departments { get; set; }

    }
}
