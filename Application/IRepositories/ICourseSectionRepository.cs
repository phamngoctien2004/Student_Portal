using Application.DTOs.CourseSection;
using Domain.Entities;
using Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.IRepositories
{
    public interface ICourseSectionRepository : IBaseRepository<CourseSection, CourseSectionParam>
    {
        Task<CourseSection?> GetParticipants(int id);
    }
}
