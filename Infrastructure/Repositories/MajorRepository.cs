using Application.DTOs.Major;
using Application.IRepositories;
using Domain.Entities;
using Infrastructure.Persistence;
using Infrastructure.Persistence.Extensions;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class MajorRepository : IMajorRepository
    {
        private readonly AppDbContext _context;

        public MajorRepository(AppDbContext context)
        {
            _context = context;
        }
        public Task AddAsync(Major entity)
        {
            throw new NotImplementedException();
        }

        public void DeleteAsync(Major entity)
        {
            throw new NotImplementedException();
        }

        public async Task<(List<Major>, int)> GetAllAsync(MajorParam param)
        {
            var query = _context.Majors.AsQueryable();

            if(param.Keyword != null)
            {
                var keyword = param.Keyword;
                query = query.Where(x => keyword.Contains(x.Name) || x.Code.Equals(keyword));
            }

            query = query.Sort(param.SortColumn, param.SortDirection);

            var count = await query.CountAsync();
            var majors = await query.Skip((param.Page - 1) * param.PageSize)
                                    .Take(param.PageSize)
                                    .ToListAsync();
            return (majors, count);
        }

        public async Task<Major?> GetByIdAsync(int id)
        {
            return await _context.Majors.Include(m => m.Courses)
                .FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task<Major?> GetByIdWithCohorts(int majorId)
        {
            return await _context.Majors.Include(m => m.Cohorts)
                            .FirstOrDefaultAsync(m => m.Id == majorId);        }

        public void UpdateAsync(Major entity)
        {
            throw new NotImplementedException();
        }
    }
}
