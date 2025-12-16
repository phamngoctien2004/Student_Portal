using Application.DTOs.Common;
using Application.DTOs.User;
using Application.Exceptions;
using Application.IRepositories;
using Application.IRepository;
using Application.IServices;
using Application.Mappers;
using AutoMapper;
using BCrypt.Net;
using Core.Entities;
using Domain.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public UserService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<BaseResponseDTO<List<UserResponse>>> GetAll(UserParams param)
        {
           
            var result = await _unitOfWork.Users.GetAllAsync(param);
            var users = result.Item1;
            var count = result.Item2;

            var metaData = new MetaDataDTO
            {
                Page = param.Page,
                PageSize = param.PageSize,
                Total = count
            };
            return BaseResponseDTO<List<UserResponse>>.SuccessResponse(
                    _mapper.Map<List<UserResponse>>(users),
                    metaData,
                    "Get products successfully"
            );
        }

        public async Task<User?> GetEntityByEmail(string email)
        {
            var user = await _unitOfWork.Users.GetByEmail(email);
            return user;
        }

        public async Task<UserResponse?> GetById(int id)
        {
            var user = await _unitOfWork.Users.GetByIdAsync(id);
            return _mapper.Map<UserResponse>(user);
        }

        public async Task<UserResponse> Add(UserRequest req)
        {
            var user = await _unitOfWork.Users.GetByEmail(req.Email);

            if(user != null)
            {
                throw new AppException(Errors.EmailExisted);
            }
            user = new User()
            {
                Email = req.Email,
                Password = BCrypt.Net.BCrypt.HashPassword(req.Password),
                RoleId = req.RoleId,
            };
            await _unitOfWork.Users.AddAsync(user);
            await _unitOfWork.SaveChangesAsync();
            return _mapper.Map<UserResponse>(user);
        }
        public async Task<UserResponse> Update(UserRequest req)
        {
            var user = await _unitOfWork.Users.GetByEmail(req.Email);

            if(user == null)
            {
                throw new AppException(Errors.AccountNotFound);
            }

            user = new User()
            {
                Id = req.Id != null ? req.Id.Value : throw new AppException(Errors.BadRequest),
                Email = req.Email,
                RoleId = req.RoleId,
            };
            _unitOfWork.Users.UpdateAsync(user);
            await _unitOfWork.SaveChangesAsync();
            return _mapper.Map<UserResponse>(user);
        }
        public async Task Delete(int id)
        {
            var user = await _unitOfWork.Users.GetByIdAsync(id);

            if (user != null)
            {
                _unitOfWork.Users.DeleteAsync(user);
                await _unitOfWork.SaveChangesAsync();
            }
        }

        public async Task ChangePassword(ChangePasswordRequest req)
        {
            var user = await _unitOfWork.Users.GetByIdAsync(req.userId);

            if (user == null)
            {
                throw new AppException(Errors.BadRequest);
            }
            if (!BCrypt.Net.BCrypt.Verify(req.OldPassword, user.Password))
            {
                throw new AppException(Errors.OldPasswordInCorrect);
            }
            var hashPassword = BCrypt.Net.BCrypt.HashPassword(req.Password);
            user.Password = hashPassword;
            _unitOfWork.Users.UpdateAsync(user);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task ResetPassword(ResetPasswordRequest req)
        {
            var user = await _unitOfWork.Users.GetByIdAsync(req.UserId);

            if (user == null)
            {
                throw new AppException(Errors.BadRequest);
            }
            var hashPassword = BCrypt.Net.BCrypt.HashPassword(req.Password);
            user.Password = hashPassword;
            _unitOfWork.Users.UpdateAsync(user);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}
