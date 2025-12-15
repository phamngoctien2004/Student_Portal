using Application.IServices;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class MeService : IMeService
    {
        private readonly IUserService _userService;
        private readonly ITeacherService _teacherService;
        private readonly IStudentService _studentService;

        public MeService(IUserService userService, 
            ITeacherService teacherService, 
            IStudentService studentService
            )
        {
            _userService = userService;
            _teacherService = teacherService;
            _studentService = studentService;
        }

        public async Task<object?> GetMeAsync(int userId, string role)
        {
            return role switch
            {
                "ADMIN" => await _userService.GetById(userId),
                "STUDENT" => await _studentService.GetByUserId(userId),
                "TEACHER" => await _teacherService.GetByUserId(userId),
                _ => throw new Exception("Role không hợp lệ")
            };
        }
    }
}
