using Application.DTOs.Common;
using Application.DTOs.Faculty;
using Application.DTOs.Major;
using Application.IServices;
using Azure;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{

    [ApiController]
    [Route("api/v{version:apiVersion}/faculties")]
    [ApiVersion("1.0")]
    public class FacultyController : BaseController
    {
        private readonly IFacultyService _facultyService;
        public FacultyController(IFacultyService facultyService)
        {
            _facultyService = facultyService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _facultyService.GetAll();
            return Ok(BaseResponseDTO<List<FacultyResponse>>.SuccessResponse(result!, Message.GetSuccess("Major", "Get")));
        }
        //[HttpGet("{id}")]
        //public async Task<IActionResult> GetById(int id)
        //{
        //    var result = await _facultyService.GetById(id);

        //}
    }
}
