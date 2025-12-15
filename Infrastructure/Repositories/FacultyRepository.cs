using Application.DTOs.Faculty;
using Application.IRepositories;
using Domain.Entities;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class FacultyRepository : IFacultyRepository
    {
        private readonly AppDbContext context;
        
        public FacultyRepository(AppDbContext context)
        {
            this.context = context;
        }
        public async Task AddAsync(Faculty entity)
        {
            await context.AddAsync(entity);
        }

        public void DeleteAsync(Faculty entity)
        {
            context.Faculties.Remove(entity);
        }

        public async Task<List<Faculty>> GetAll()
        {
            return await context.Faculties.ToListAsync();
        }

        public Task<(List<Faculty>, int)> GetAllAsync(FacultyParam param)
        {
            throw new NotImplementedException();
        }

        public async Task<Faculty?> GetByIdAsync(int id)
        {
            return await context.Faculties.FindAsync(id);
        }

        public void UpdateAsync(Faculty entity)
        {
            context.Update(entity);
        }
    }
}
