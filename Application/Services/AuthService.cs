using Application.DTOs.Auth;
using Application.DTOs.User;
using Application.Exceptions;
using Application.IServices;
using AutoMapper;
using Core.Entities;
using Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserService _userService;
        private readonly IJwtService _jwtService;
        private readonly IMapper _mapper;
        public AuthService(IUserService UserService, IJwtService JwtService, IMapper mapper) 
        { 
            _userService = UserService;
            _jwtService = JwtService;
            _mapper = mapper;
        }

        public async Task<AuthResponse> Login(LoginRequest loginRequest)
        {
            var user = await _userService.GetEntityByEmail(loginRequest.Email);

            if (user == null || !BCrypt.Net.BCrypt.Verify(loginRequest.Password, user.Password))
            {
                throw new AppException(ErrorStatus.UnAuthentication);
            }
            return new AuthResponse()
            {
                AccessToken = _jwtService.GenerateToken(user.Id.ToString(), user.Role.Name, false),
                RefreshToken = _jwtService.GenerateToken(user.Id.ToString(), user.Role.Name, true),
                User = _mapper.Map<UserResponse>(user)
            };
        }
    }
}
