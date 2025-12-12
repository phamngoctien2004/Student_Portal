using Application.DTOs.User;
using Core.Entities;
using Domain.Entities;
using Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.IRepository
{
	public interface IUserRepository : IBaseRepository<User, UserParams>
    {
		Task<User?> GetByEmail(string email);
    }
}
