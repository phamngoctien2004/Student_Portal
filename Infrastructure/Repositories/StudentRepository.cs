using Application.DTOs.Student;
using Application.IRepositories;
using Domain.Entities;
using Infrastructure.Persistence;
using Infrastructure.Persistence.Extensions;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class StudentRepository : IStudentRepository
    {
        private readonly AppDbContext context;

        public StudentRepository(AppDbContext context)
        {
            this.context = context;
        }
        public async Task<(List<Student>, int)> GetAllAsync(StudentParams param)
        {
            var query = context.Students.AsQueryable();

            if(param.Keyword != null)
            {
                var Keyword = param.Keyword;
                query = query.Where(x =>
                                     x.Name.Contains(Keyword) ||
                                     x.Code.Contains(Keyword) ||
                                     x.Address.Contains(Keyword) ||
                                     x.Email.Contains(Keyword)
 );
            }
            if(param.CorhortId != null)
            {
                query = query.Where(x => x.CohortId == param.CorhortId);
            }
            query = query.Sort(param.SortColumn, param.SortDirection);
            var count = await query.CountAsync();

            var students = await query.Skip((param.Page - 1) * param.PageSize)
                              .Take(param.PageSize)
                              .Include(s => s.Cohort)
                              .Include(s => s.User)
                              .ToListAsync();
            return (students, count);
        }

        public async Task<Student?> GetByIdAsync(int id)
        {
            var student = await context.Students
                .Include(s => s.Cohort)
                .Include(s => s.User)
                .FirstOrDefaultAsync(s => s.Id == id);
            return student;
        }
        public async Task AddAsync(Student entity)
        {
           context.Students.Add(entity);
        }
        public void UpdateAsync(Student entity)
        {
            context.Students.Update(entity);
        }
        public void DeleteAsync(Student student)
        {
            context.Students.Remove(student);
        }

        public async Task<int> Count()
        {
            return await context.Students.CountAsync();
        }

        public async Task<Student?> GetByUserId(int userId)
        {
            var student = await context.Students
                .Include(s => s.Cohort)
                .Include(s => s.User)
                .FirstOrDefaultAsync(s => s.UserId == userId);
            return student;
        }

     
    }
}
