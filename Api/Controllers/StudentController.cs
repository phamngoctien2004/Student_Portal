using Application.DTOs.Common;

using Application.DTOs.Student;
using Application.DTOs.Upload;
using Application.IServices;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/v{version:apiVersion}/students")]
    [ApiVersion("1.0")]
    [Authorize]
    public class StudentController : BaseController
    {
        private readonly IStudentService _studentService;

        public StudentController(IStudentService studentService) 
        {
            _studentService = studentService;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Get([FromQuery] StudentParams param)
        {
            var response = await _studentService.GetAll(param);
            return Ok(response);
        }
        [HttpGet("{id}")]
        //[Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> GetById(int id)
        {
            var response = await _studentService.GetById(id);
            return Ok(BaseResponseDTO<StudentResponse>.SuccessResponse(response!, Message.GetSuccess("Student", "Get")));
        }
        [HttpPost]
        //[Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> Create([FromBody] StudentRequest req)
        {
            var response = await _studentService.Add(req);
            return StatusCode(201, BaseResponseDTO<StudentResponse>.SuccessResponse(response, Message.GetSuccess("Student", "Create")));
        }
        [HttpPut]
        //[Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> Update([FromBody] StudentRequest req)
        {
            var response = await _studentService.Update(req);
            return StatusCode(200, BaseResponseDTO<StudentResponse>.SuccessResponse(response, Message.GetSuccess("Student", "Update")));
        }
        [HttpDelete("{id}")]
        //[Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> Update(int id)
        {
            await _studentService.Delete(id);
            return NoContent();
        }

        [HttpPost("avatar")]
        public async Task<IActionResult> UploadAvatar([FromForm] IFormFile file)
        {
            if (file != null)
            {
                var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                var uploadReq = new UploadReq()
                {
                    FileName = file.FileName,
                    ContentType = file.ContentType,
                    FileStream = file.OpenReadStream(),
                    Length = file.Length
                };
                var url = await _studentService.UploadAvatar(uploadReq, int.Parse(userId));
                return Ok(BaseResponseDTO<string>.SuccessResponse(url, "Upload Avatar successfully"));
            }
            return BadRequest();
        }
    }
}
