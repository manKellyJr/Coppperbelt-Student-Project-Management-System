using ProjectManagementSystem.Models;

namespace ProjectManagementSystem.Interfaces
{
    public interface IProgramme
    {
        public IEnumerable<Programme> GetProgrammeDetails();
    }
}
