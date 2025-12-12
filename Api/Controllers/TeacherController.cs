using Application.DTOs.Common;

using Application.DTOs.Teacher;
using Application.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/v{version:apiVersion}/teachers")]
    [ApiVersion("1.0")]
    //[Authorize]
    public class TeacherController : BaseController
    {
        private readonly ITeacherService _teacherService;

        public TeacherController(ITeacherService teacherService) 
        {
            _teacherService = teacherService;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Get([FromQuery] TeacherParams teacherParams)
        {
            var response = await _teacherService.GetAll(teacherParams);
            return Ok(response);
        }
        [HttpGet("{id}")]
        //[Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> GetById(int id)
        {
            var response = await _teacherService.GetById(id);
            return Ok(BaseResponseDTO<TeacherResponse>.SuccessResponse(response!, Message.GetSuccess("Teacher", "Get")));
        }
        [HttpPost]
        //[Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> Create([FromBody] TeacherRequest req)
        {
            var response = await _teacherService.Add(req);
            return StatusCode(201, BaseResponseDTO<TeacherResponse>.SuccessResponse(response, Message.GetSuccess("Teacher", "Create")));
        }
        [HttpPut]
        //[Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> Update([FromBody] TeacherRequest req)
        {
            var response = await _teacherService.Update(req);
            return StatusCode(200, BaseResponseDTO<TeacherResponse>.SuccessResponse(response, Message.GetSuccess("Teacher", "Update")));
        }
        [HttpDelete("{id}")]
        //[Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> Delete(int id)
        {
            await _teacherService.Delete(id);
            return NoContent();
        }

        [HttpPut("avatar")]
        public async Task<IActionResult> UploadAvatar([FromForm] IFormFile formFile)
        {

            return NoContent();
        }
    }
}
