using Application.DTOs.Common;
using Application.DTOs.Major;
using Application.IServices;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/v{version:apiVersion}/majors")]
    [ApiVersion("1.0")]
    public class MajorController : BaseController
    {
        private readonly IMajorService _service;

        public MajorController(IMajorService service)
        {
            _service = service;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] MajorParam param)
        {
            var response = await _service.GetAll(param);
            return Ok(response);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetDetail(int id)
        {
            var response = await _service.GetById(id);
            return Ok(BaseResponseDTO<MajorResponse>.SuccessResponse(response!, Message.GetSuccess("Major", "Get")));
        }
    }
}
