using Application.DTOs.Major;
using Domain.Entities;
using Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.IRepositories
{
    public interface IMajorRepository : IBaseRepository<Major, MajorParam>
    {
        Task<Major?> GetByIdWithCohorts(int majorId); 
    }
}
