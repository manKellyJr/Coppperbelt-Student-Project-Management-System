namespace ProjectManagementSystem.Models
{
    public class Department
    {
        public int DepartmentId { get; set; }
        public string DepartmentName { get; set; }
        public School School { get; set; }
        public int SchoolId { get; set; }
        public IEnumerable<Programme> Programmes { get; set; }

    }
}
