using Application.DTOs.Enrollment;
using Domain.Entities;

namespace Application.IServices
{
    public interface IEnrollmentService : IBaseService<Enrollment, EnrollmentRequest, EnrollmentResponse, EnrollmentParam>
    {
        Task<List<EnrollmentResponse>> GetAllByStudent(ScheduleRequest req);
        Task UpdateScore(UpdateScoreRequest req);
        Task UpdateStatus(UpdateStatusRequest req);

        Task<List<int>> GetCoursePassed(int userId);
    }
}
