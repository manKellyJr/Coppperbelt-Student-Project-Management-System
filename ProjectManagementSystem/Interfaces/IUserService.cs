using ProjectManagementSystem.Models;

namespace ProjectManagementSystem.Interfaces
{
    public interface IUserService
    {
        Task<int> AddUser(RegisterRequest register);
        Task<bool> Delete(string userName);
    }
}
