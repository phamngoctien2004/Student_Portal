using Application.DTOs.Common;
using Application.DTOs.Student;
using Application.IRepositories;
using Core.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/v{version:apiVersion}/semesters")]
    [ApiVersion("1.0")]
    public class SemesterController : BaseController
    {
        private readonly ISemesterRepository _semester;

        public SemesterController(ISemesterRepository repository) 
        {
            _semester = repository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var response = await _semester.GetAll();

            return Ok(BaseResponseDTO<List<Semester>>.SuccessResponse(response!, Message.GetSuccess("Student", "Get")));
        }
    }
}
