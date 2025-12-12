using Application.DTOs.Semester;
using Core.Entities;
using Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.IRepositories
{
    public interface ISemesterRepository : IBaseRepository<Semester, SemesterParam>
    {
        Task<List<Semester>> GetAll();
    }
}
