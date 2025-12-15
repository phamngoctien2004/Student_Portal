using Application.DTOs.Faculty;
using Domain.Entities;
using Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.IRepositories
{
    public interface IFacultyRepository : IBaseRepository<Faculty, FacultyParam>
    {
        Task<List<Faculty>> GetAll();
    }
}
