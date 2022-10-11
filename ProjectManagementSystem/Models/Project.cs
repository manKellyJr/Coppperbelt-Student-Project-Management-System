using System.ComponentModel.DataAnnotations;

namespace ProjectManagementSystem.Models
{
    public class Project
    {
        public int ProjectId { get; set; }
        public string ProjectName { get; set; }
        public string ProjectDescritpion { get; set; }
        [StringLength(1000) ] 
        public string Abstract { get; set; }
        public string Keywords { get; set; }
        public string? FileName { get; set; }
        public DateTime? StartDate { get; set; }
        public Student Student { get; set; }
        public int StudentId { get; set; }
        public int? GroupId { get; set; }
        public Group? Group { get; set; }
        public List<ProjectAssessment> Assessments { get; set; }
    }
}
