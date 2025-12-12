using Application.DTOs.Common;

using Application.DTOs.User;
using Application.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/v{version:apiVersion}/users")]
    [ApiVersion("1.0")]
    [Authorize]
    public class UserController : BaseController
    {
        private readonly IUserService _userService;
        private readonly IMeService _meService;
        public UserController(IUserService userService, IMeService meService) 
        {
            _userService = userService;
            _meService = meService;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Get([FromQuery] UserParams userParams)
        {
            var response = await _userService.GetAll(userParams);
            return Ok(response);
        }
        [HttpGet("{id}")]
        [AllowAnonymous]

        //[Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> GetById(int id)
        {
            var response = await _userService.GetById(id);
            return Ok(BaseResponseDTO<UserResponse>.SuccessResponse(response!, Message.GetSuccess("user", "Get")));
        }
        [HttpGet("/me")]
        public async Task<IActionResult> GetMe()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var role = User.FindFirst(ClaimTypes.Role)?.Value;

            var response = await _meService.GetMeAsync(int.Parse(userId!), role!);
            return Ok(BaseResponseDTO<object>.SuccessResponse(response!, Message.GetSuccess("user", "Get")));
        }
        [HttpPost("password")]
        public async Task<IActionResult> ChangePassword(ChangePasswordRequest req)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            req.userId = int.Parse(userId!);
            await _userService.ChangePassword(req);
            return NoContent();
        }
        [HttpPost]
        [AllowAnonymous]

        //[Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> Create([FromBody] UserRequest req)
        {
            var response = await _userService.Add(req);
            return StatusCode(201, BaseResponseDTO<UserResponse>.SuccessResponse(response, Message.GetSuccess("user", "Create")));
        }
        [HttpPut]
        [AllowAnonymous]

        //[Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> Update([FromBody] UserRequest req)
        {
            var response = await _userService.Update(req);
            return StatusCode(200, BaseResponseDTO<UserResponse>.SuccessResponse(response, Message.GetSuccess("user", "Update")));
        }
        [HttpDelete("{id}")]
        [AllowAnonymous]

        //[Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> Update(int id)
        {
            await _userService.Delete(id);
            return NoContent();
        }


        
    }
}
