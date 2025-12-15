
using Domain.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Exceptions
{
    public class AppException : Exception
    {
        public ErrorCode ErrorCode { get; }
        
        public AppException(ErrorCode errorCode) : base(errorCode.Message) {
            ErrorCode = errorCode;
        }
    }
}
