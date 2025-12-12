using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Enums
{
    public enum ErrorStatus
    {
        UnAuthentication = 401,
        UnAuthorization = 403,

        // user
        EmailExisted = 1000,
        AccountNotFound = 1001,
        OldPasswordInCorrect = 1002,
        // teacher
        TeacherExisted = 2000,
        TeacherNotFound = 404,
        StudentNotFound = 404,
        //file 
        FileNotEmpty = 2000,
        FileTooLarge = 2001,
        FileNotAllowed = 2002,

        // course register
        CannotRegisterCourse =3000,
        ScheduleInvalid = 3001,
        CannotCancel = 3002,
        //
        INVALID_DATA = 9000,
        PHONE_EXISTED = 9001,
        
        //
        EmailInvalid = 9002,
        CannotDeleteHasChild = 9003,

        //system
        DataNotFound = 9998,
        BadRequest = 9999,
        InternalServer = 9997
    }
}
