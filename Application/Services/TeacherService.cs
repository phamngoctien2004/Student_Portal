using Application.DTOs.Common;
using Application.DTOs.Teacher;
using Application.DTOs.User;
using Application.Exceptions;
using Application.IRepositories;
using Application.IServices;
using AutoMapper;
using Core.Entities;
using Core.Enums;
using Domain.Entities;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class TeacherService : ITeacherService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        public TeacherService(IUnitOfWork unitOfWork, IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<TeacherResponse> Add(TeacherRequest req)
        {
            // begin
            await _unitOfWork.BeginTransactionAsync();
            var existsTeacher = await _unitOfWork.Teachers.ExistsByPhone(req.Phone);

            if (existsTeacher)
            {
                throw new AppException(ErrorStatus.TeacherExisted);
            }

            // create teacher account 
            var userRequest = new UserRequest()
            {
                RoleId = (int)RoleCode.TEACHER,
                Email = req.EmailUser,
                Password = "hvnh2004"
            };

            var userReponse = await _userService.Add(userRequest);

            // create teacher
            var teacher = new Teacher()
            {
                Name = req.Name,
                Phone = req.Phone,
                Gender = req.Gender,
                Brith = req.Brith,
                Address = req.Address,
                FacultyId = req.FacultyId,
                UserId = userReponse.Id,
                Email = req.EmailUser,
                Code = "TEA2025" + userReponse.Id 
            };

            await _unitOfWork.Teachers.AddAsync(teacher);
            await _unitOfWork.SaveChangesAsync();

            // commit
            await _unitOfWork.CommitTransactionAsync();
            return _mapper.Map<TeacherResponse>(teacher);
        }
        public async Task<TeacherResponse> Update(TeacherRequest req)
        {
            if(req.Id == null)
            {
                throw new AppException(ErrorStatus.INVALID_DATA);
            }

            var teacher = await _unitOfWork.Teachers.GetByIdAsync(req.Id.Value);
            if (teacher == null)
            {
                throw new AppException(ErrorStatus.TeacherNotFound);
            }

            if (!teacher.Phone.Equals(req.Phone) && await _unitOfWork.Teachers.ExistsByPhone(req.Phone))
            {
                throw new AppException(ErrorStatus.PHONE_EXISTED);
            } 

            teacher.Name = req.Name;
            teacher.Brith = req.Brith;
            teacher.Address = req.Address;
            teacher.Phone = req.Phone;
            teacher.Gender = req.Gender;
            teacher.FacultyId = req.FacultyId;

            _unitOfWork.Teachers.UpdateAsync(teacher);
            await _unitOfWork.SaveChangesAsync();
            return _mapper.Map<TeacherResponse>(teacher);
        }

        public async Task<BaseResponseDTO<List<TeacherResponse>>> GetAll(TeacherParams param)
        {
            var result = await _unitOfWork.Teachers.GetAllAsync(param);
            var teachers = result.Item1;
            var count = result.Item2;
            var metaData = new MetaDataDTO
            {
                Page = param.Page,
                PageSize = param.PageSize,
                Total = count
            };
            return BaseResponseDTO<List<TeacherResponse>>.SuccessResponse(
                   _mapper.Map<List<TeacherResponse>>(teachers),
                   metaData,
                   "Get Teachers successfully"
            );
        }

        public async Task<TeacherResponse?> GetById(int id)
        {
            var teacher = await _unitOfWork.Teachers.GetByIdAsync(id);
            return _mapper.Map<TeacherResponse>(teacher);
        }
        public async Task Delete(int id)
        {
            await _unitOfWork.BeginTransactionAsync();
            var teacher = await _unitOfWork.Teachers.GetByIdAsync(id);

            if (teacher == null)
            {
                throw new AppException(ErrorStatus.TeacherNotFound);
            }

            var user = teacher.User;
            _unitOfWork.Teachers.DeleteAsync(teacher);
            _unitOfWork.Users.DeleteAsync(user);

            await _unitOfWork.SaveChangesAsync();
            await _unitOfWork.CommitTransactionAsync();
        }

        public async Task<TeacherResponse> GetByUserId(int userId)
        {
            var teacher = await _unitOfWork.Teachers.GetByUserId(userId);
            return _mapper.Map<TeacherResponse>(teacher);
        }
    }
}
