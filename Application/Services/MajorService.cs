using Application.DTOs.Common;
using Application.DTOs.Major;
using Application.IRepositories;
using Application.IServices;
using AutoMapper;

namespace Application.Services
{
    public class MajorService : IMajorService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public MajorService(IUnitOfWork unitOfWork, IMapper mapper) 
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public Task<MajorResponse> Add(MajorRequest req)
        {
            throw new NotImplementedException();
        }

        public Task Delete(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<BaseResponseDTO<List<MajorResponse>>> GetAll(MajorParam param)
        {
            var response = await _unitOfWork.Majors.GetAllAsync(param);
            var result = response.Item1;
            var count = response.Item2;

            var meta = new MetaDataDTO
            {
                Page = param.Page,
                PageSize = param.PageSize,
                Total = count
            };

            return BaseResponseDTO<List<MajorResponse>>.SuccessResponse(
                _mapper.Map<List<MajorResponse>>(result),
                meta, 
                "Get All Majors Succesfully");
        }

        public async Task<MajorResponse?> GetById(int id)
        {
            var major = await _unitOfWork.Majors.GetByIdAsync(id);
            return _mapper.Map<MajorResponse>(major);
        }

        public async Task<MajorResponse> GetByIdWithCohorts(int majorId)
        {
            var major = await _unitOfWork.Majors.GetByIdWithCohorts(majorId);
            return _mapper.Map<MajorResponse>(major);
        }

        public Task<MajorResponse> Update(MajorRequest req)
        {
            throw new NotImplementedException();
        }
    }
}
