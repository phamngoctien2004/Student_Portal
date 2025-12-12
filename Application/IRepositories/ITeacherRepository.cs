using Application.DTOs.Teacher;
using Domain.Entities;
using Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.IRepositories
{
    public interface ITeacherRepository : IBaseRepository<Teacher, TeacherParams>
    {
        Task<bool> ExistsByPhone(string phone);
        Task<Teacher?> GetByUserId(int userId);
    }
}
