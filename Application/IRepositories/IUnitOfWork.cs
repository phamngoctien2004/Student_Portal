using Application.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.IRepositories
{
    public interface IUnitOfWork
    {
        ICourseRepository Courses { get; }
        IUserRepository Users { get; }
        ITeacherRepository Teachers { get; }
        IStudentRepository Students { get; }
        IFacultyRepository Faculties { get; }
        ICourseSectionRepository CourseSections { get; }
        ISemesterRepository Semesters { get; }
        IEnrollmentRepository Enrollments { get; }

        IMajorRepository Majors { get; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
        Task BeginTransactionAsync();
        Task CommitTransactionAsync();
        Task RollbackTransactionAsync();
    }
}
