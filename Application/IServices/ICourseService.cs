using Application.DTOs.Common;
using Application.DTOs.Course;
using Application.IRepository;
using Application.IServices;
using Domain.Entities;

namespace Application.Services
{
	public interface ICourseService : IBaseService<Course, CourseRequest, CourseResponse, CourseParams>
	{

	}
}
