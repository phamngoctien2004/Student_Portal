using Application.DTOs.User;
using Application.IRepository;
using Core.Entities;
using Infrastructure.Persistence;
using Infrastructure.Persistence.Extensions;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;
        public UserRepository(AppDbContext context) 
        {
            _context = context;
        }
        public async Task AddAsync(User user)
        {
            await _context.Users.AddAsync(user);
        }
        public void UpdateAsync(User user)
        {
            _context.Users.Update(user);
        }
        public void DeleteAsync(User user)
        {
            _context.Users.Remove(user);
        }

        public async Task<(List<User>, int)> GetAllAsync(UserParams userParams)
        {
            var query = _context.Users.AsQueryable();
            
            // Filter and Sort
            if(userParams.Keyword != null)
            {
                query = query.Where(x => x.Email.Contains(userParams.Keyword));
            }
            if(userParams.RoleId != null)
            {
                query = query.Where(x => x.RoleId.Equals(userParams.RoleId.Value));
            }
            query = query.Sort(userParams.SortColumn, userParams.SortDirection);

            // Paging
            var count = await query.CountAsync();
            var users = await query.Skip((userParams.Page - 1) * userParams.PageSize)
                                   .Take(userParams.PageSize)
                                   .ToListAsync();
            return (users, count);
        }

        public async Task<User?> GetByEmail(string email)
        {
            var user = await _context.Users
                .Include(u => u.Role)
                .FirstOrDefaultAsync(u => u.Email == email);
            return user;
        }

        public async Task<User?> GetByIdAsync(int id)
        {
            var user = await _context.Users
                .FindAsync(id);
            return user;
        }

  
    }
}
