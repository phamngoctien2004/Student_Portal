using Application.DTOs.Common;
using Application.DTOs.Course;
using Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
	[ApiController]
	[Route("api/v{version:apiVersion}/courses")]
	[ApiVersion("1.0")]
	[Authorize]
	public class CourseController : BaseController
	{
		private readonly ICourseService _service;

		public CourseController(ICourseService service)
		{
			_service = service;
		}

		[HttpGet]
		[AllowAnonymous]
		public async Task<IActionResult> GetAll([FromQuery] CourseParams subjectParams)
		{
			var response = await _service.GetAll(subjectParams);
			return Ok(response);
		}

		[HttpGet("{id}")]
		[AllowAnonymous]
		public async Task<IActionResult> Get(int id)
		{
			var response = await _service.GetById(id);
			return Ok(BaseResponseDTO<CourseResponse>.SuccessResponse(response!, Message.GetSuccess("Course", "Get")));
		}

		[HttpPost]
		[Authorize(Roles = "ADMIN")]
		public async Task<IActionResult> Create([FromBody] CourseRequest req)
		{
			var response = await _service.Add(req);
			return StatusCode(201, BaseResponseDTO<CourseResponse>.SuccessResponse(response, Message.GetSuccess("Course", "Create")));
		}

		[HttpPut]
		[Authorize(Roles = "ADMIN")]
		public async Task<IActionResult> Update([FromBody] CourseRequest req)
		{
			var response = await _service.Update(req);
			return StatusCode(200, BaseResponseDTO<CourseResponse>.SuccessResponse(response, Message.GetSuccess("Course", "Update")));
		}

		[HttpDelete("{id}")]
		[Authorize(Roles = "ADMIN")]
		public async Task<IActionResult> Delete(int id)
		{
			await _service.Delete(id);
			return NoContent();
		}
	}
}
