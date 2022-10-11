using ProjectManagementSystem.Models;

namespace ProjectManagementSystem.Interfaces
{
    public interface ICoordinator
    {
        public IEnumerable<Coordinator> GetCoordinatorDetails();
    }
}
