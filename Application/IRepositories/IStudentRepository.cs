using Application.DTOs.Student;
using Domain.Entities;
using Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.IRepositories
{
    public interface IStudentRepository : IBaseRepository<Student, StudentParams>
    {
        Task<int> Count();
        Task<Student?> GetByUserId(int userId);
    }
}
