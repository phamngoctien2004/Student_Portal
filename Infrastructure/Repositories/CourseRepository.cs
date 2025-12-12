using Application.DTOs.Course;
using Application.IRepository;
using Domain.Entities;
using Infrastructure.Persistence;
using Infrastructure.Persistence.Extensions;
using Microsoft.EntityFrameworkCore;
using static Microsoft.Extensions.Logging.EventSource.LoggingEventSource;

namespace Infrastructure.Repositories
{
    public class CourseRepository : ICourseRepository
	{
		private readonly AppDbContext _context;

		public CourseRepository(AppDbContext context)
		{
			_context = context;
		}
        public async Task<Course?> GetByIdAsync(int id)
        {
            return await _context.Courses.FindAsync(id);
        }
        public async Task<(List<Course>, int)> GetAllAsync(CourseParams param)
        {
            var query = _context.Courses.AsQueryable();
           
            if(param.Keyword != null)
            {
                var keyword = param.Keyword;
                query = query.Where(x => x.Name.Contains(keyword) || x.Code.Contains(keyword));
            }
       
            query = query.Sort(param.SortColumn, param.SortDirection);

            var count = await query.CountAsync();

            var data = await query
                .Skip((param.Page - 1) * param.PageSize)
                .Take(param.PageSize)
                .ToListAsync();

            return (data, count);
        }
        public async Task AddAsync(Course course)
		{
			await _context.Courses.AddAsync(course);
		}

		public void DeleteAsync(Course course)
		{
			_context.Courses.Remove(course);
		}

        public void UpdateAsync(Course entity)
        {
            _context.Courses.Update(entity);
        }

        public async Task<Course?> GetCourseWithSectionAsync(int id)
        {
            return await _context.Courses
                .Include(c => c.CourseSections)
                .FirstOrDefaultAsync(c => c.Id == id);
        }
    }
}
