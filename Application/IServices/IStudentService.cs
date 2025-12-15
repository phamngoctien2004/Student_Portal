using Application.DTOs.Student;
using Application.DTOs.Teacher;
using Application.DTOs.Upload;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.IServices
{
    public interface IStudentService : IBaseService<Student, StudentRequest, StudentResponse, StudentParams>
    {
        Task<StudentResponse> GetByUserId(int userId);
        Task<string> UploadAvatar(UploadReq req, int userId);
    }
}
