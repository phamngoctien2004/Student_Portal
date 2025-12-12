using Application.DTOs.Teacher;
using Application.IRepositories;
using Domain.Entities;
using Infrastructure.Persistence;
using Infrastructure.Persistence.Extensions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class TeacherRepository : ITeacherRepository
    {
        private readonly AppDbContext context;
        
        public TeacherRepository(AppDbContext context)
        {
            this.context = context;
        }
        public async Task AddAsync(Teacher entity)
        {
            await context.AddAsync(entity);
        }

        public void DeleteAsync(Teacher teacher)
        {
            context.Remove(teacher);
        }

        public async Task<bool> ExistsByPhone(string phone)
        {
            var existed = await context.Teachers.AnyAsync(t => t.Phone.Equals(phone));
            return existed;
        }

        public async Task<(List<Teacher>, int)> GetAllAsync(TeacherParams param)
        {
            var query = context.Teachers.AsQueryable();
            if(param.FacultyId != null)
            {
                query = query.Where(x => x.FacultyId == param.FacultyId);
            }
            if(param.Keyword != null)
            {
                var keyword = param.Keyword;
                query = query.Where(x => x.Code.Contains(keyword)
                                    || x.Name.Contains(keyword)
                                    || x.Email.Contains(keyword)
                                    || x.Address.Contains(keyword));
            }
            query = query.Sort(param.SortColumn, param.SortDirection);
            var count = await query.CountAsync();
            var teachers = await query.Skip((param.Page - 1) * param.PageSize)
                .Take(param.PageSize)
                .Include(t => t.User)
                .Include(t => t.Faculty)
                .ToListAsync();

            return (teachers, count);
        }

        public async Task<Teacher?> GetByIdAsync(int id)
        {
            var teacher = await context.Teachers
                .Include(t => t.User)
                .Include(t => t.Faculty)
                .FirstOrDefaultAsync(t => t.Id == id);
            return teacher;         
        }

        public async Task<Teacher?> GetByUserId(int userId)
        {
            var teacher = await context.Teachers
                .Include(t => t.User)
                .Include(t => t.Faculty)
                .FirstOrDefaultAsync(t => t.UserId == userId);
            return teacher;
        }


        public void UpdateAsync(Teacher entity)
        {
           context.Teachers.Update(entity);
        }
    }
}
