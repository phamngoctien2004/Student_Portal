using Application.IRepositories;
using Application.IRepository;
using Application.IServices;
using Application.IServices.ExternalServices;
using Application.Mappers;
using Application.Services;
using Domain.Entities;
using Infrastructure.ExternalServices;
using Infrastructure.Persistence;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure
{
	public static class DependencyInjection
	{
		public static IServiceCollection AddInfrastructure(this IServiceCollection services, string connectionString)
		{
			services.AddDbContext<AppDbContext>(opt => opt.UseSqlServer(connectionString));
			
			//repository
			services.AddScoped<ICourseRepository, CourseRepository>();
			services.AddScoped<IUserRepository, UserRepository>();
			services.AddScoped<IUnitOfWork, UnitOfWork>();
			services.AddScoped<IService1, Service1>();
			services.AddScoped<ITeacherRepository, TeacherRepository>();
			services.AddScoped<IStudentRepository, StudentRepository>();
            services.AddScoped<IFacultyRepository, FacultyRepository>();
            services.AddScoped<ICourseSectionRepository, CourseSectionRepository>();
            services.AddScoped<ISemesterRepository, SemesterRepository>();
            services.AddScoped<IEnrollmentRepository, EnrollmentRepository>();
            services.AddScoped<IMajorRepository, MajorRepository>();
            //services
            services.AddScoped<ICourseService, CourseService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IJwtService, JwtService>();
            services.AddScoped<IUploadService, S3UploadService>();
            services.AddScoped<ITeacherService, TeacherService>();
            services.AddScoped<IStudentService, StudentService>();
            services.AddScoped<IFacultyService, FacultyService>();
            services.AddScoped<ICourseSectionService, CourseSectionService>();
            services.AddScoped<IMeService, MeService>();
            services.AddScoped<IEnrollmentService, EnrollmentService>();
            services.AddScoped<IMajorService, MajorService>();
            // mapper profile
            services.AddAutoMapper(c =>
            {
                c.AddProfile<CourseMapper>();
                c.AddProfile<SemesterMapper>();
                c.AddProfile<UserMapper>();
                c.AddProfile<TeacherMapper>();
                c.AddProfile<StudentMapper>();
                c.AddProfile<CohortMapper>();
                c.AddProfile<FacultyMapper>();
            });

            return services;
		}
	}
}
