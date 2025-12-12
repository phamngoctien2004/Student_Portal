using Application.DTOs.Semester;
using Application.IRepositories;
using Core.Entities;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class SemesterRepository : ISemesterRepository
    {
        private readonly AppDbContext _context;

        public SemesterRepository(AppDbContext context)
        {
            _context = context;
        }
        public Task AddAsync(Semester entity)
        {
            throw new NotImplementedException();
        }

        public void DeleteAsync(Semester entity)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Semester>> GetAll()
        {
            return await _context.Semesters.ToListAsync();
        }

        public Task<(List<Semester>, int)> GetAllAsync(SemesterParam param)
        {
            throw new NotImplementedException();
        }

        public async Task<Semester?> GetByIdAsync(int id)
        {
            return await _context.Semesters.FindAsync(id);
        }

        public void UpdateAsync(Semester entity)
        {
            throw new NotImplementedException();
        }
    }
}
