using Application.DTOs.CourseSection;
using Application.DTOs.Semester;
using AutoMapper;
using Core.Entities;
using Domain.Entities;

namespace Application.Mappers
{
    public class SemesterMapper : Profile
    {
        public SemesterMapper()
        {
            CreateMap<CourseSection, CourseSectionResponse>();

            CreateMap<Semester, SemesterResponse>();
        }
    }
}
