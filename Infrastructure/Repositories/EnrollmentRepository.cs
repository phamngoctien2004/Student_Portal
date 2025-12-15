using Application.DTOs.Enrollment;
using Application.IRepositories;
using Domain.Entities;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class EnrollmentRepository : IEnrollmentRepository
    {
        private readonly AppDbContext _context;

        public EnrollmentRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task AddAsync(Enrollment entity)
        {
            await _context.AddAsync(entity);
        }

        public  void DeleteAsync(Enrollment entity)
        {
            _context.Enrollments.Remove(entity);
        }

        public async Task<bool> ExistBySemesterAndDayAndSlotAsync(int semesterId, int slotId, int dayOfWeek, int studentId)
        {
            return await _context.Enrollments.AnyAsync(e => 
                e.CourseSection!.SemesterId == semesterId &&
                e.CourseSection!.Slot == slotId &&
                e.CourseSection!.DayOfWeek == dayOfWeek &&
                e.StudentId == studentId
            );
        }

        public async Task<bool> ExistByCourseAndStudentAsync(int courseId, int studentId)
        {
            return await _context.Enrollments.AnyAsync(
                e => e.CourseSection!.CourseId == courseId && 
                e.StudentId == studentId &&
                e.TotalScore >= 7
            );
                                                                  
        }

        public Task<(List<Enrollment>, int)> GetAllAsync(EnrollmentParam param)
        {
            throw new NotImplementedException();
        }

        public async Task<Enrollment?> GetByIdAsync(int id)
        {
            return await _context.Enrollments
                .Include(e => e.Student)
                .Include(e => e.CourseSection).ThenInclude(cs => cs.Semester)
                .FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task<List<Enrollment>> GetBySemesterAndStudentAsync(ScheduleRequest req)
        {
            return await _context.Enrollments
            .Include(e => e.CourseSection).ThenInclude(cs => cs.Course)
            .Where(x =>
                x.CourseSection!.SemesterId == req.SemesterId &&
                x.StudentId == req.StudentId
            )
            .ToListAsync();
        }

        public void UpdateAsync(Enrollment entity)
        {
            _context.Enrollments.Update(entity);
        }

        public async Task<List<int>> GetEnrollmentPassed(int studentId)
        {
            return await _context.Enrollments
                .Where(e => e.StudentId == studentId && e.IsPass == true)
                .Select(e => e.CourseSection.Course.Id)
                .Distinct()
                .ToListAsync();
        }
    }
}
