using Application.DTOs.Enrollment;
using Domain.Entities;
using Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.IRepositories
{
    public interface IEnrollmentRepository : IBaseRepository<Enrollment, EnrollmentParam>
    {
        Task<bool> ExistByCourseAndStudentAsync(int courseId, int studentId);
        Task<bool> ExistBySemesterAndDayAndSlotAsync(int semesterId, int slotId, int dayOfWeek, int studentId);
        Task<List<Enrollment>> GetBySemesterAndStudentAsync(ScheduleRequest req);
    }
}
