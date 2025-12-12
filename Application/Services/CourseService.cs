using Application.DTOs.Common;
using Application.DTOs.Course;
using Application.Exceptions;
using Application.IRepositories;
using Application.IRepository;
using Application.IServices;
using Application.IServices.ExternalServices;
using Application.Mappers;
using AutoMapper;
using Core.Enums;
using Domain.Entities;
using Microsoft.Extensions.Logging;
using static Domain.Constants.MessageConstant;

namespace Application.Services
{
	public class CourseService : ICourseService
	{
		private readonly ILogger<CourseService> _logger;
		private readonly ICourseRepository _repository;
		private readonly IService1 _service1;
		private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IFacultyService _facultyService;

		public CourseService(ILogger<CourseService> logger, 
            ICourseRepository repository, 
            IService1 service1, 
            IMapper mapper,
            IUnitOfWork unitOfWork,
            IFacultyService facultyService)
		{
			_logger = logger;
			_repository = repository;
			_service1 = service1;
			_mapper = mapper;
            _unitOfWork = unitOfWork;
            _facultyService = facultyService;
        }

		public async Task<CourseResponse?> GetById(int id)
		{
			_logger.LogInformation($"Course id: {id}");

			var result = await _repository.GetByIdAsync(id) ?? throw new KeyNotFoundException(CommonMessage.NOT_FOUND); ;

            return _mapper.Map<CourseResponse>(result); 
        }
        public async Task<BaseResponseDTO<List<CourseResponse>>> GetAll(CourseParams param)
        {
            var result = await _repository.GetAllAsync(param);

            var courses = result.Item1;
            var count = result.Item2;

            var metaData = new MetaDataDTO
            {
                Page = param.Page,
                PageSize = param.PageSize,
                Total = count
            };
            return BaseResponseDTO<List<CourseResponse>>.SuccessResponse(
                    _mapper.Map<List<CourseResponse>>(courses),
                    metaData,
                    "Get courses successfully"
            );
        }

        public async Task<CourseResponse> Add(CourseRequest req)
        {
            // tạo code 
            string datePart = DateTime.Now.ToString("ddMMyy");
            var random = new Random();
            string randomPart = random.Next(1000, 9999).ToString();
            var courseCode = $"IT{datePart}{randomPart}";

            // 
            Course course = new()
            {
                Code = courseCode,
                Name = req.Name,
                Credit = req.Credit,
            };
			await _repository.AddAsync(course);
            await _unitOfWork.SaveChangesAsync();
            return _mapper.Map<CourseResponse>(course);
        }
        
        public async Task<CourseResponse> Update(CourseRequest req)
        {
            
            var course = await _repository.GetByIdAsync(req.Id 
                ?? throw new AppException(ErrorStatus.BadRequest));

            if(course == null)
            {
                throw new AppException(ErrorStatus.DataNotFound);
            }

            course.Name = req.Name;
            course.Credit = req.Credit;
            _repository.UpdateAsync(course);
            await _unitOfWork.SaveChangesAsync();

            return _mapper.Map<CourseResponse>(course);
        }

        public async Task Delete(int id)
        {
            // check
            var course = await _repository.GetCourseWithSectionAsync(id);

            if (course == null)
            {
                throw new AppException(ErrorStatus.DataNotFound);
            }
            if (course.CourseSections.Any())
            {
                throw new AppException(ErrorStatus.CannotDeleteHasChild);
            }

            _repository.DeleteAsync(course);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}
