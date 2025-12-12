using Application.DTOs.Auth;
using Application.DTOs.Common;
using Application.IServices;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : BaseController
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest loginRequest)
        {
            var authResponse = await _authService.Login(loginRequest);

            // lưu httponly cookie
            Response.Cookies.Append("refresh_token", authResponse.RefreshToken, new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.Strict,
                Expires = DateTime.UtcNow.AddDays(39)
            });
            authResponse.RefreshToken = string.Empty;

            return Ok(BaseResponseDTO<AuthResponse>.SuccessResponse(authResponse, null, "Login successfully"));
        }
    }
}
