
using Application.DTOs.Course;
using Application.DTOs.Enrollment;
using Application.DTOs.Major;
using AutoMapper;
using Domain.Entities;

namespace Application.Mappers
{
    public class CourseMapper : Profile
    {
        public CourseMapper()
        {
            CreateMap<Course, CourseResponse>();
            CreateMap<Major, MajorResponse>();
            CreateMap<Enrollment, EnrollmentResponse>()
                .ForMember(dest => dest.CourseCode, opt => opt.MapFrom(src => src.CourseSection.Course.Code))
                .ForMember(dest => dest.CourseName, opt => opt.MapFrom(src => src.CourseSection.Course.Name))
                .ForMember(dest => dest.TeacherName, opt => opt.MapFrom(src => src.CourseSection.TeacherName))
                .ForMember(dest => dest.Credit, opt => opt.MapFrom(src => src.CourseSection.Course.Credit))
                .ForMember(dest => dest.DayOfWeek, opt => opt.MapFrom(src => src.CourseSection.DayOfWeek))
                .ForMember(dest => dest.StartDate, opt => opt.MapFrom(src => src.CourseSection.StartDate))
                .ForMember(dest => dest.EndDate, opt => opt.MapFrom(src => src.CourseSection.EndDate))
                .ForMember(dest => dest.StudentName, opt => opt.MapFrom(src => src.Student.Name))
                .ForMember(dest => dest.Slot, opt => opt.MapFrom(src => src.CourseSection.Slot));
        }
    }
}
