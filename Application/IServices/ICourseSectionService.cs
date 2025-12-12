using Application.DTOs.CourseSection;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.IServices
{
    public interface ICourseSectionService : IBaseService<CourseSection, CourseSectionRequest, CourseSectionResponse, CourseSectionParam>
    {
        Task<CourseSectionResponse> GetParticipants(int id);
    }
}
