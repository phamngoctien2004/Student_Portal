namespace Domain.Constants
{
    public class ErrorCode
    {
        public string? Code { get; set; }
        public string? Message { get; set; }
        public int HttpStatus { get; set; }

        public static ErrorCode Init(string code, string message, int httpStatus)
        {
            return new ErrorCode() 
            { 
                Code = code,
                Message = message,
                HttpStatus = httpStatus
            };
        }
    }
}
