namespace ProjectManagementSystem.Models
{
    public class Course
    {
        public int CourseId { get; set; }
        public string CourseCode { get; set; }
        public string CourseName { get; set; }
        public Programme Programme { get; set; }
        public int ProgrammeId { get; set; }
    }
}
