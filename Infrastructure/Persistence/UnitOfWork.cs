using Application.IRepositories;
using Application.IRepository;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistence
{
    public class UnitOfWork(AppDbContext context,
        ICourseRepository courses,
        IUserRepository users,
        ITeacherRepository teachers,
        IStudentRepository students,
        IFacultyRepository faculties,
        ICourseSectionRepository courseSections,
        ISemesterRepository semesters,
        IEnrollmentRepository enrollments,
        IMajorRepository majors
        ) : IUnitOfWork
    {
        private readonly AppDbContext _context = context;
        private IDbContextTransaction _transaction;

        public ICourseRepository Courses { get; } = courses;
        public IUserRepository Users { get; } = users;
        public IStudentRepository Students { get; } = students;
        public ITeacherRepository Teachers { get; } = teachers;
        public IFacultyRepository Faculties { get; } = faculties;
        public ICourseSectionRepository CourseSections { get; } = courseSections;
        public ISemesterRepository Semesters { get; } = semesters;

        public IEnrollmentRepository Enrollments { get; } = enrollments;

        public IMajorRepository Majors { get; } = majors;

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return await _context.SaveChangesAsync(cancellationToken);
        }
        public async Task BeginTransactionAsync()
        {
            _transaction = await _context.Database.BeginTransactionAsync();
        }
        public async Task CommitTransactionAsync()
        {
            await _transaction.CommitAsync();
        }
        public async Task RollbackTransactionAsync()
        {
            await _transaction.RollbackAsync();
        }
        public void Dispose()
        {
            _transaction?.Dispose();
            _context.Dispose();
        }
    }
}
