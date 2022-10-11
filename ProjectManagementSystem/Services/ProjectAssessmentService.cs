//using Microsoft.EntityFrameworkCore;
//using ProjectManagementSystem.Models;

//namespace ProjectManagementSystem.Services
//{
//    public class ProjectAssessmentService : ServiceBase<ProjectAssessment>
//    {
//        public ProjectAssessmentService(ProjectManagementDbContext context) : base(context)
//        {
//        }
//        public IEnumerable<ProjectAssessment> GetProjectAssessmentDetails()
//        {
//            return Entities.Include(e => e.Project).ThenInclude(c => c.Student).ThenInclude(s=>s.Programme).ToList();
//        }
//    }
//}
