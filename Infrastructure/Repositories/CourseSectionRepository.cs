using Application.DTOs.CourseSection;
using Application.IRepositories;
using Domain.Entities;
using Infrastructure.Persistence;
using Infrastructure.Persistence.Extensions;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class CourseSectionRepository : ICourseSectionRepository
    {
        private readonly AppDbContext _context;

        public CourseSectionRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task AddAsync(CourseSection entity)
        {
            await _context.CourseSections.AddAsync(entity);
        }

        public void DeleteAsync(CourseSection entity)
        {
            throw new NotImplementedException();
        }

        public async Task<(List<CourseSection>, int)> GetAllAsync(CourseSectionParam param)
        {
            var query = _context.CourseSections.AsQueryable();

            if(param.Keyword != null)
            {
                var keyword = param.Keyword;
                query = query.Where(x => x.Code.Contains(keyword));
            }
            if(param.TeacherId != null)
            {
                query = query.Where(x => x.TeacherId == param.TeacherId);
            }
            if (param.CourseId != null)
            {
                query = query.Where(x => x.CourseId == param.CourseId);
            }
            if (param.SemesterId != null){
                query = query.Where(x => x.SemesterId == param.SemesterId);

            }

            var count = await query.CountAsync();

            query = query.Sort(param.SortColumn, param.SortDirection);

            var result = await query.Skip((param.Page - 1) * param.PageSize)
                .Take(param.PageSize)
                .Include(c => c.Semester)
                .Include(c => c.Course)
                .Include(c => c.Teacher)
                .ToListAsync();
            return (result, count);
        }

        public async Task<CourseSection?> GetByIdAsync(int id)
        {
            return await _context.CourseSections
                .Include(c => c.Teacher)
                .Include(c => c.Course)
                .Include(c => c.Semester)

                .FirstOrDefaultAsync(c => c.Id == id);  
        }

        public async Task<CourseSection?> GetParticipants(int id)
        {
            return await _context.CourseSections
                 .Include(c => c.Teacher)
                 .Include(c => c.Course)
                 .Include(c => c.Semester)
                 .Include(c => c.Enrollments).ThenInclude(e => e.Student)
                 .FirstOrDefaultAsync(c => c.Id == id);
        }

        public void UpdateAsync(CourseSection entity)
        {
            _context.CourseSections.Update(entity);
        }
    }
}
