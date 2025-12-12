using Application.DTOs.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.IServices
{
    public interface IBaseService<T, TReq, TRes, TParam> 
    {
        Task<TRes?> GetById(int id);
        Task<BaseResponseDTO<List<TRes>>> GetAll(TParam param);
        Task<TRes> Add(TReq req);
        Task<TRes> Update(TReq req);
        Task Delete(int id);
    }
}
