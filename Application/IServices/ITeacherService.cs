using Application.DTOs.Teacher;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.IServices
{
    public interface ITeacherService : IBaseService<Teacher, TeacherRequest, TeacherResponse, TeacherParams>
    {
        Task<TeacherResponse> GetByUserId(int userId);
    }
}
