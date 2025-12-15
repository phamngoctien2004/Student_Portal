using Application.DTOs.Common;
using Application.DTOs.Faculty;
using Application.IRepositories;
using Application.IServices;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class FacultyService : IFacultyService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public FacultyService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }
        public Task<FacultyResponse> Add(FacultyRequest req)
        {
            throw new NotImplementedException();
        }

        public Task Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Task<BaseResponseDTO<List<FacultyResponse>>> GetAll(FacultyParam param)
        {
            throw new NotImplementedException();
        }

        public async Task<List<FacultyResponse>> GetAll()
        {
            var list = await _unitOfWork.Faculties.GetAll();
            return _mapper.Map<List<FacultyResponse>>(list);
        }

        public async Task<FacultyResponse?> GetById(int id)
        {
            var faculty = await _unitOfWork.Faculties.GetByIdAsync(id);
            return _mapper.Map<FacultyResponse>(faculty);
        }

        public Task<FacultyResponse> Update(FacultyRequest req)
        {
            throw new NotImplementedException();
        }
    }
}
