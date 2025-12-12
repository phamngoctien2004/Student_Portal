using Core.Entities;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public interface IBaseRepository<T, TParam>
    {

        Task<T?> GetByIdAsync(int id);
        Task<(List<T>, int)> GetAllAsync(TParam param);
        Task AddAsync(T entity);
        void UpdateAsync(T entity);
        void DeleteAsync(T entity);
    }
}
