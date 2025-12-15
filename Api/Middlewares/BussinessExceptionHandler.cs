using Application.DTOs.Common;
using Application.Exceptions;
using Microsoft.AspNetCore.Diagnostics;

namespace Api.Middlewares
{
    public class BussinessExceptionHandler : IExceptionHandler
    {
        private readonly ILogger<BussinessExceptionHandler> _logger;

        public BussinessExceptionHandler(ILogger<BussinessExceptionHandler> logger)
        {
            _logger = logger; 
        }
        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {
            var response = BaseResponseDTO<string>.FailResponse("Internal Server Error");

            var statusCode = StatusCodes.Status500InternalServerError;

            // Xử lý lỗi bussiness
            if(exception is AppException appExp)
            {
                var errorCode = appExp.ErrorCode;
                statusCode = errorCode.HttpStatus;
                response = BaseResponseDTO<string>.FailResponse(errorCode.Message, errorCode.HttpStatus);
                _logger.LogWarning("Business Error: {Code} - {Message}", errorCode.Code, errorCode.Message);

            }
            else
            {
                _logger.LogError(exception, "System Exception: {Message}", exception.Message);
            }
            httpContext.Response.StatusCode = statusCode;
            await httpContext.Response.WriteAsJsonAsync(response, cancellationToken);

            return true;
        }
    }
}
