using Application.DTOs.Common;
using Application.DTOs.Student;
using Application.DTOs.Teacher;
using Application.DTOs.User;
using Application.Exceptions;
using Application.IRepositories;
using Application.IServices;
using AutoMapper;
using Core.Enums;
using Domain.Entities;

namespace Application.Services
{
    public class StudentService : IStudentService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IUserService _userService;
        public StudentService(IUnitOfWork unitOfWork, IMapper mapper, IUserService userService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _userService = userService;
        }

        public async Task<BaseResponseDTO<List<StudentResponse>>> GetAll(StudentParams param)
        {
            var result = await _unitOfWork.Students.GetAllAsync(param);
            var students = result.Item1;
            var count = result.Item2;

            var metaData = new MetaDataDTO
            {
                Page = param.Page,
                PageSize = param.PageSize,
                Total = count
            };
            return BaseResponseDTO<List<StudentResponse>>.SuccessResponse(
                   _mapper.Map<List<StudentResponse>>(students),
                   metaData,
                   "Get Students successfully"
            );
        }

        public async Task<StudentResponse?> GetById(int id)
        {
            var student = await _unitOfWork.Students.GetByIdAsync(id);
            return _mapper.Map<StudentResponse>(student);
        }

        public async Task<StudentResponse> Add(StudentRequest req)
        {
            await _unitOfWork.BeginTransactionAsync(); 

            var count = await _unitOfWork.Students.Count();
            var studentCode = "25A40" + count.ToString("D5");

            var user = new UserRequest()
            {
                Email = $"{studentCode}@hvnh.edu.vn",
                Password = studentCode,
                RoleId = 2
            };
            var userResponse = await _userService.Add(user);
            var student = new Student()
            {
                Name = req.Name,
                Gender = req.Gender,
                Brith = req.Brith,
                Address = req.Address,
                Phone = req.Phone,
                CohortId = req.CohortId,
                Code = studentCode,
                UserId = userResponse.Id,
            };

            await _unitOfWork.Students.AddAsync(student);
            await _unitOfWork.SaveChangesAsync();
            await _unitOfWork.CommitTransactionAsync();
            return _mapper.Map<StudentResponse>(student);
        }
        public async Task<StudentResponse> Update(StudentRequest req)
        {
            // check
            var id = req.Id;
            if(id == null || id != 0)
            {
                throw new AppException(ErrorStatus.BadRequest);
            }

            var student = await _unitOfWork.Students.GetByIdAsync(id.Value);
            if(student == null)
            {
                throw new AppException(ErrorStatus.BadRequest); 
            }
            student.Address = req.Address;
            student.Phone = req.Phone;

            await _unitOfWork.SaveChangesAsync();
            return _mapper.Map<StudentResponse>(student);
        }
        public async Task Delete(int id)
        {
            await _unitOfWork.BeginTransactionAsync();

            var student = await _unitOfWork.Students.GetByIdAsync(id);
            if(student == null)
            {
                throw new AppException(ErrorStatus.StudentNotFound);
            }

            _unitOfWork.Students.DeleteAsync(student);
            if(student.User != null)
            {
                _unitOfWork.Users.DeleteAsync(student.User);
            }

            await _unitOfWork.SaveChangesAsync();
            await _unitOfWork.CommitTransactionAsync();
        }

        public async Task<StudentResponse> GetByUserId(int userId)
        {
            var student = await _unitOfWork.Students.GetByUserId(userId);
            return _mapper.Map<StudentResponse>(student);
        }
    }
}
