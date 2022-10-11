using ProjectManagementSystem.Models;

namespace ProjectManagementSystem.Interfaces
{
    public interface IProject
    {
        Project UploadPdf(int ProjectId, byte[] pdf);
        public IEnumerable<Project> GetProjectDetails();
    }
}
