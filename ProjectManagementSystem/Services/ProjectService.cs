using Microsoft.EntityFrameworkCore;
using ProjectManagementSystem.Models;

namespace ProjectManagementSystem.Services
{
    public class ProjectService : ServiceBase<Project> 
    {
        private readonly ProjectManagementDbContext _context;
        readonly IWebHostEnvironment env;
        public ProjectService(ProjectManagementDbContext context, IWebHostEnvironment _env) : base(context)
        {
            _context = context;
             env = _env;
        }
        public bool UploadPdf(int ProjectId,  MemoryStream data,string fn)
        {
            try
            {   var project = Entities.Single(x => x.ProjectId == ProjectId);
                if (project == null) return false;
                var file = Path.GetRandomFileName().Replace(".","_")+Path.GetExtension(fn);
                var filePath = Path.Combine(env.WebRootPath, "files", file);
                var fileStream = new FileStream(path: filePath, FileMode.OpenOrCreate,FileAccess.ReadWrite);
                Task.Run( ()=> data.WriteTo(fileStream));
                project.FileName = file;                 
                return Update(project);
            }   
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message+ex.StackTrace);
                return false;
            }
        }

        public IEnumerable<Project> GetProjectDetails()
        {
            return Entities.Include(e => e.Student).ThenInclude(s=>s.Programme).ToList();
        }
    }
}
