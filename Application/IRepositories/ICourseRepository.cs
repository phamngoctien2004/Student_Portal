using Application.DTOs.Course;
using Domain.Entities;
using Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.IRepository
{
	public interface ICourseRepository : IBaseRepository<Course, CourseParams>
	{
		Task<Course?> GetCourseWithSectionAsync(int id);
    }
}
