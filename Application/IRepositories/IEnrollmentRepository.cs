using Application.DTOs.Enrollment;
using Domain.Entities;
using Infrastructure.Repositories;

namespace Application.IRepositories
{
    public interface IEnrollmentRepository : IBaseRepository<Enrollment, EnrollmentParam>
    {
        Task<List<int>> GetEnrollmentPassed(int studentId);
        Task<bool> ExistByCourseAndStudentAsync(int courseId, int studentId);
        Task<bool> ExistBySemesterAndDayAndSlotAsync(int semesterId, int slotId, int dayOfWeek, int studentId);
        Task<List<Enrollment>> GetBySemesterAndStudentAsync(ScheduleRequest req);
    }
}
