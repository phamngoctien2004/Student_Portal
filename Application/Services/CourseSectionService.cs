using Amazon.Runtime.Internal.Util;
using Application.DTOs.Common;
using Application.DTOs.CourseSection;
using Application.DTOs.Teacher;
using Application.Exceptions;
using Application.Helpers;
using Application.IRepositories;
using Application.IServices;
using AutoMapper;
using Core.Enums;
using Domain.Entities;
using Microsoft.Extensions.Logging;
namespace Application.Services
{
    public class CourseSectionService : ICourseSectionService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITeacherService _teacherService;
        private readonly ICourseService _courseService;
        private readonly IMapper _mapper;
        private readonly ILogger<CourseSectionService> _logger;
        public CourseSectionService(IUnitOfWork unitOfWork, 
            ITeacherService teacherService, 
            ICourseService courseService,
            IMapper mapper,
            ILogger<CourseSectionService> logger)
        {
            _unitOfWork = unitOfWork;
            _teacherService = teacherService;
            _courseService = courseService;
            _mapper = mapper;
            _logger = logger;
        }
        public async Task<CourseSectionResponse> Add(CourseSectionRequest req)
        {
            _logger.LogDebug($"===Course Section Service: Add===");
            await _unitOfWork.BeginTransactionAsync();

            var teacher = await _teacherService.GetById(req.TeacherId);
            if(teacher == null)
            {
                _logger.LogDebug($"===Course Section Service: Teacher Invalid===");
                throw new AppException(ErrorStatus.BadRequest);
            }

            var course = await _courseService.GetById(req.CourseId);
            if (course == null)
            {
                _logger.LogDebug($"===Course Section Service: Course Invalid===");
                throw new AppException(ErrorStatus.BadRequest);
            }
            var semester = await _unitOfWork.Semesters.GetByIdAsync(req.SemesterId);
            if (semester == null)
            {
                _logger.LogDebug($"===Course Section Service: Course Invalid===");
                throw new AppException(ErrorStatus.BadRequest);
            }
            // tạo code 
            string datePart = DateTime.Now.ToString("ddMMyy");
            var random = new Random();
            string randomPart = random.Next(1000, 9999).ToString();
            var courseCode = $"CS{datePart}{randomPart}";

            // tính start date, end date
            

            var courseSection = new CourseSection()
            {
                Code = courseCode,
                DayOfWeek = (int) req.StartDate.DayOfWeek,
                StartDate = req.StartDate,
                EndDate = DateHelpers.GetEndDate(req.StartDate),
                TeacherId = req.TeacherId,
                TeacherName = teacher.Name,
                CourseId = req.CourseId,
                SemesterId = req.SemesterId,
                Slot = req.Slot,
            };
            _logger.LogDebug($"===Course Section Service: Entity {courseCode}===");

            await _unitOfWork.CourseSections.AddAsync(courseSection);
            await _unitOfWork.SaveChangesAsync();
            await _unitOfWork.CommitTransactionAsync();

            return _mapper.Map<CourseSectionResponse>(courseSection);
        }

        public Task Delete(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<BaseResponseDTO<List<CourseSectionResponse>>> GetAll(CourseSectionParam param)
        {
            var result = await _unitOfWork.CourseSections.GetAllAsync(param);
            var coursesSection = result.Item1;
            var count = result.Item2;

            var metaDTO = new MetaDataDTO()
            {
                Page = param.Page,
                PageSize = param.PageSize,
                Total = count
            };

            return BaseResponseDTO<List<CourseSectionResponse>>.SuccessResponse(
                   _mapper.Map<List<CourseSectionResponse>>(coursesSection),
                   metaDTO,
                   "Get List Course Section successfully"
            );
        }

        public async Task<CourseSectionResponse?> GetById(int id)
        {
            var result = await _unitOfWork.CourseSections.GetByIdAsync(id);
            return _mapper.Map<CourseSectionResponse>(result);
        }

        public async Task<CourseSectionResponse> GetParticipants(int id)
        {
            var result = await _unitOfWork.CourseSections.GetParticipants(id);
            return _mapper.Map<CourseSectionResponse>(result);

        }

        public async Task<CourseSectionResponse> Update(CourseSectionRequest req)
        {
            _logger.LogDebug($"===Course Section Service: Add===");
            await _unitOfWork.BeginTransactionAsync();

            var courseSection = await _unitOfWork.CourseSections.GetByIdAsync(req.Id);

            if(courseSection == null)
            {
                throw new AppException(ErrorStatus.BadRequest);
            }

            var semester = await _unitOfWork.Semesters.GetByIdAsync(req.SemesterId);
            if (semester == null)
            {
                _logger.LogDebug($"===Course Section Service: Course Invalid===");
                throw new AppException(ErrorStatus.BadRequest);
            }

            var teacher = await _teacherService.GetById(req.TeacherId);
            if (teacher == null)
            {
                _logger.LogDebug($"===Course Section Service: Teacher Invalid===");
                throw new AppException(ErrorStatus.BadRequest);
            }

            var course = await _courseService.GetById(req.CourseId);
            if (course == null)
            {
                _logger.LogDebug($"===Course Section Service: Course Invalid===");
                throw new AppException(ErrorStatus.BadRequest);
            }

            courseSection.TeacherId = req.TeacherId;
            courseSection.CourseId = req.CourseId; 
            courseSection.SemesterId = req.SemesterId;

            _unitOfWork.CourseSections.UpdateAsync(courseSection);
            await _unitOfWork.SaveChangesAsync();
            await _unitOfWork.CommitTransactionAsync();

            return _mapper.Map<CourseSectionResponse>(courseSection);
        }
    }
}
