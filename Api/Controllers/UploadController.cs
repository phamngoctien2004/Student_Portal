using Application.DTOs.Common;
using Application.DTOs.Upload;
using Application.IServices;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/upload")]
    public class UploadController : BaseController
    {
        private readonly IUploadService _uploadService;
        public UploadController(IUploadService uploadService)
        {
            _uploadService = uploadService;
        }

        [HttpPost()]
        public async Task<IActionResult> Upload([FromForm] IFormFile file)
        {
            if(file != null)
            {
                var uploadReq = new UploadReq()
                {
                    FileName = file.FileName,
                    ContentType = file.ContentType,
                    FileStream = file.OpenReadStream(),
                    Length = file.Length
                };
                var path = await _uploadService.Upload(uploadReq, "avatar");
                return Ok(BaseResponseDTO<string>.SuccessResponse(path, "Upload file successfully"));
            }
            return BadRequest();
        }
    }
}
