using Application.DTOs.Common;
using Application.DTOs.Enrollment;
using Application.IServices;
using Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/v{version:apiVersion}/enrollments")]
    [ApiVersion("1.0")]
    [Authorize]
    public class EnrollmentController : BaseController
    {
        private readonly IMeService _meService;
        private readonly IEnrollmentService _service;

        public EnrollmentController(IEnrollmentService service, IMeService meService)
        {
            _service = service;
            _meService = meService;
        }
        
        [HttpGet("schedules")]
        [AllowAnonymous]
        public async Task<IActionResult> GetScheduleByStudent([FromQuery] ScheduleRequest req)
        {
            var result = await _service.GetAllByStudent(req);
            return Ok(BaseResponseDTO<List<EnrollmentResponse>>.SuccessResponse(result, Message.GetSuccess("Enroll", "Get")));
        }
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Enroll([FromBody] EnrollmentRequest req)
        {
            var result = await _service.Add(req);
            return StatusCode(201, BaseResponseDTO<EnrollmentResponse>.SuccessResponse(result, Message.GetSuccess("Enroll", "")));
        }

        [HttpDelete("{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> Cancel( int id)
        {
            await _service.Delete(id);
            return NoContent();
        }

        [HttpPut("scores")]
        [AllowAnonymous]
        public async Task<IActionResult> UpdateScore([FromBody] UpdateScoreRequest request)
        {
            await _service.UpdateScore(request);
            return NoContent();
        }

        [HttpPut("status")]
        [AllowAnonymous]
        public async Task<IActionResult> UpdateStatus([FromBody] UpdateStatusRequest request)
        {
            await _service.UpdateStatus(request);
            return NoContent();
        }
    }
}
