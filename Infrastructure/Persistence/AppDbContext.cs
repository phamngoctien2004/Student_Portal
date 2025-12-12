using Core.Entities;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence
{
	public class AppDbContext : DbContext
	{
		public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

		public DbSet<Course> Courses => Set<Course>();
		public DbSet<Cohort> Cohorts => Set<Cohort>();
		public DbSet<CourseSection> CourseSections => Set<CourseSection>();
		public DbSet<Enrollment> Enrollments => Set<Enrollment>();
		public DbSet<Faculty> Faculties => Set<Faculty>();

		public DbSet<Role> Roles => Set<Role>();
		public DbSet<Semester> Semesters => Set<Semester>();
		public DbSet<Student> Students => Set<Student>();
		public DbSet<Teacher> Teachers => Set<Teacher>();
        public DbSet<User> Users => Set<User>();
		public DbSet<Major> Majors => Set<Major>();
        protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);

            foreach (var fk in modelBuilder.Model
                .GetEntityTypes()
                .SelectMany(e => e.GetForeignKeys()))
            {
                fk.DeleteBehavior = DeleteBehavior.Restrict;
            }
        }
    }
}
