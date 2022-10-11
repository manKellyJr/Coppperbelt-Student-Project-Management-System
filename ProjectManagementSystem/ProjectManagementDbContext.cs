using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ProjectManagementSystem.Models;

namespace ProjectManagementSystem
{
    public class ProjectManagementDbContext : IdentityDbContext<User, Role, int>
    {
    public ProjectManagementDbContext(DbContextOptions<ProjectManagementDbContext> options) : base(options) { }
        public virtual DbSet<School> School { get; set; }
        public virtual DbSet<Department> Department { get; set; }
        public virtual DbSet<Programme> Programme { get; set; }
        public virtual DbSet<Student> Student { get; set; }
        public virtual DbSet<Supervisor> Supervisor { get; set; }
        public virtual DbSet<Project> Project { get; set; }
        public virtual DbSet<Coordinator> Coordinator { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Project>(e =>
            {
                e.OwnsMany(e => e.Assessments, a =>
                {
                    a.ToTable("ProjectAssessment");
                    a.Property(a => a.ProjectId);
                    a.WithOwner(a => a.Project);
                });
            });
            base.OnModelCreating(builder); 
        }

    }
}
