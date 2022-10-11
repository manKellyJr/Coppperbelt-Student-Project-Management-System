using Microsoft.EntityFrameworkCore;
using ProjectManagementSystem.Models;

namespace ProjectManagementSystem.Services
{
    public class ProgrammeService : ServiceBase<Programme>
    {
        public ProgrammeService(ProjectManagementDbContext context) : base(context)
        {
        }
        public IEnumerable<Programme> GetProgrammeDetails()
        {
            return Entities.Include(e => e.Department).ToList();
        }
    }
}
