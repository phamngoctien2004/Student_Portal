using Application.DTOs.Common;
using Application.DTOs.Enrollment;
using Application.Exceptions;
using Application.IRepositories;
using Application.IServices;
using AutoMapper;
using Domain.Constants;
using Domain.Entities;
using Microsoft.Extensions.Logging;

namespace Application.Services
{
    public class EnrollmentService : IEnrollmentService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICourseSectionService _courseSectionService;
        private readonly IMapper _mapper;
        private readonly ILogger<IEnrollmentService> _logger;
        private readonly IMeService _meService;
        public EnrollmentService(IUnitOfWork unitOfWork, 
            ICourseSectionService courseSectionService, 
            IMapper mapper, 
            ILogger<IEnrollmentService> logger,
            IMeService meService)
        {
            _unitOfWork = unitOfWork;
            _courseSectionService = courseSectionService;
            _mapper = mapper;
            _logger = logger;
            _meService = meService;
        }

        public async Task<EnrollmentResponse> Add(EnrollmentRequest req)
        {
            _logger.LogInformation("Enroll: Add");
            await _unitOfWork.BeginTransactionAsync();

            // kiểm tra điều kiện
            var courseSection = await _courseSectionService.GetById(req.CourseSectionId);

            if(courseSection == null)
            {
                throw new AppException(Errors.DataNotFound);
            }

            var isExisted = await _unitOfWork.Enrollments.ExistByCourseAndStudentAsync(courseSection.Course.Id, req.StudentId);
            if(isExisted)
            {
                throw new AppException(Errors.CannotRegisterCourse);
            }

            var semester = courseSection.Semester ?? throw new AppException(Errors.InternalServer);
            isExisted = await _unitOfWork.Enrollments
                .ExistBySemesterAndDayAndSlotAsync(semester.Id,
                                                            (int) courseSection.Slot,
                                                            courseSection.DayOfWeek,
                                                            req.StudentId);
            if (isExisted)
            {
                throw new AppException(Errors.ScheduleInvalid);
            }
            _logger.LogInformation("Enroll: Request Valid");

            // xử lý
            var enrollment = new Enrollment()
            {
                StudentId = req.StudentId,
                CourseSectionId = courseSection.Id,
                EnrollmentDate = DateTime.Now
            };
            await _unitOfWork.Enrollments.AddAsync(enrollment);

            await _unitOfWork.SaveChangesAsync();
            await _unitOfWork.CommitTransactionAsync();

            _logger.LogInformation("Enroll: commit success");

            return _mapper.Map<EnrollmentResponse>(enrollment);
        }

        public async Task Delete(int id)
        {
            var enrollment = await _unitOfWork.Enrollments.GetByIdAsync(id);

            if (enrollment == null)
            {
                throw new AppException(Errors.BadRequest);
            }

            var canDelete = enrollment.CourseSection!.Semester!.IsJoin;

            if (!canDelete)
            {
                throw new AppException(Errors.CannotCancel);
            }

            _unitOfWork.Enrollments.DeleteAsync(enrollment);
            await _unitOfWork.SaveChangesAsync();
        }

        public Task<BaseResponseDTO<List<EnrollmentResponse>>> GetAll(EnrollmentParam param)
        {
            throw new NotImplementedException();
        }

        public async Task<List<EnrollmentResponse>> GetAllByStudent(ScheduleRequest req)
        {
            var result = await _unitOfWork.Enrollments.GetBySemesterAndStudentAsync(req);
            return _mapper.Map<List<EnrollmentResponse>>(result);
        }

        public Task<EnrollmentResponse?> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<int>> GetCoursePassed(int studentId)
        {
            return await _unitOfWork.Enrollments.GetEnrollmentPassed(studentId);
        }

        public async Task<EnrollmentResponse> Update(EnrollmentRequest req)
        {
            throw new NotImplementedException();
        }

        public async Task UpdateScore(UpdateScoreRequest req)
        {
            var enrollment = await _unitOfWork.Enrollments.GetByIdAsync(req.Id);

            if (enrollment == null)
            {
                throw new AppException(Errors.BadRequest);
            }

            enrollment.Attendance = req.Attendance;
            enrollment.Midterm = req.MidTerm;
            enrollment.FinalExam = req.FinalExam;
            enrollment.SetTotalScore();
            _unitOfWork.Enrollments.UpdateAsync(enrollment);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task UpdateStatus(UpdateStatusRequest req)
        {
            var enrollment = await _unitOfWork.Enrollments.GetByIdAsync(req.Id);

            if (enrollment == null)
            {
                throw new AppException(Errors.BadRequest);
            }
            enrollment.Status = req.Status;

            _unitOfWork.Enrollments.UpdateAsync(enrollment);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}
