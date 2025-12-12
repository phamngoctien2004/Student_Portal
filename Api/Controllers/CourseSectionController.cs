using Application.DTOs.Common;
using Application.DTOs.CourseSection;
using Application.IServices;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/v{version:apiVersion}/courseSections")]
    [ApiVersion("1.0")]
    //[Authorize]
    public class CourseSectionController : BaseController
    {
        private readonly ICourseSectionService _service;

        public CourseSectionController(ICourseSectionService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] CourseSectionParam param)
        {
            var response = await _service.GetAll(param);
            return Ok(response);
        }
 
        [HttpGet("participants/{id}")]
        public async Task<IActionResult> GetParticipants(int id)
        {
            var response = await _service.GetParticipants(id);
            return Ok(response);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetDetails(int id)
        {
            var response = await _service.GetById(id);
            return Ok(response);
        }
        [HttpPost]
        public async Task<IActionResult> OpenCourseSection(CourseSectionRequest request)
        {
            var response = await _service.Add(request);
            return StatusCode(201, BaseResponseDTO<CourseSectionResponse>.SuccessResponse(response!, Message.GetSuccess("Course Section ", "Add")));
        }
        [HttpPut]
        public async Task<IActionResult> UpdateCourseSection(CourseSectionRequest request)
        {
            var response = await _service.Update(request);
            return StatusCode(201, BaseResponseDTO<CourseSectionResponse>.SuccessResponse(response!, Message.GetSuccess("Course Section ", "Update")));
        }
    }
}
