using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Constants
{
    public static class Errors
    {
        public static readonly ErrorCode UnAuthentication = ErrorCode.Init(
                "AUTH_001",
                "Người dùng chưa đăng nhập",
                401
        );
        public static readonly ErrorCode UnAuthorization = ErrorCode.Init(
                "AUTH_002",
                "Người dùng không có quyền hạn truy cập chức năng",
                403
        );
        public static readonly ErrorCode EmailExisted = ErrorCode.Init(
                "USER_001",
                "Email đã tồn tại",
                409
        );
        public static readonly ErrorCode AccountNotFound = ErrorCode.Init(
                "USER_002",
                "Không tìm thấy tài khoản",
                404
        );
        public static readonly ErrorCode OldPasswordInCorrect = ErrorCode.Init(
                "USER_003",
                "Mật khẩu cũ không chính xác",
                400
        );
        public static readonly ErrorCode EmailInvalid = ErrorCode.Init(
                "USER_004",
                "Email không hợp lệ",
                400
        );

        public static readonly ErrorCode PhoneExisted = ErrorCode.Init(
                "USER_005",
                "Số điện thoại đã tồn tại",
                409
        );

        // ================= TEACHER / STUDENT =================
        public static readonly ErrorCode TeacherExisted = ErrorCode.Init(
                "TEACHER_001",
                "Giáo viên đã tồn tại",
                409
        );

        public static readonly ErrorCode TeacherNotFound = ErrorCode.Init(
                "TEACHER_002",
                "Không tìm thấy giáo viên",
                404
        );

        public static readonly ErrorCode StudentNotFound = ErrorCode.Init(
                "STUDENT_001",
                "Không tìm thấy sinh viên",
                404
        );

        // ================= FILE =================
        public static readonly ErrorCode FileNotEmpty = ErrorCode.Init(
                "FILE_001",
                "File không được để trống",
                400
        );

        public static readonly ErrorCode FileTooLarge = ErrorCode.Init(
                "FILE_002",
                "Dung lượng file vượt quá giới hạn cho phép",
                400
        );

        public static readonly ErrorCode FileNotAllowed = ErrorCode.Init(
                "FILE_003",
                "Định dạng file không được hỗ trợ",
                400
        );

        // ================= COURSE REGISTER =================
        public static readonly ErrorCode CannotRegisterCourse = ErrorCode.Init(
                "COURSE_001",
                "Không thể đăng ký môn học",
                400
        );

        public static readonly ErrorCode ScheduleInvalid = ErrorCode.Init(
                "COURSE_002",
                "Thời khóa biểu không hợp lệ",
                400
        );

        public static readonly ErrorCode CannotCancel = ErrorCode.Init(
                "COURSE_003",
                "Không thể hủy đăng ký môn học",
                400
        );

        // ================= COMMON / VALIDATION =================
        public static readonly ErrorCode InvalidData = ErrorCode.Init(
                "COMMON_001",
                "Dữ liệu không hợp lệ",
                400
        );

        public static readonly ErrorCode CannotDeleteHasChild = ErrorCode.Init(
                "COMMON_002",
                "Không thể xóa do đang tồn tại dữ liệu liên quan",
                409
        );

        public static readonly ErrorCode DataNotFound = ErrorCode.Init(
                "COMMON_003",
                "Không tìm thấy dữ liệu",
                404
        );

        public static readonly ErrorCode BadRequest = ErrorCode.Init(
                "COMMON_004",
                "Yêu cầu không hợp lệ",
                400
        );

        // ================= SYSTEM =================
        public static readonly ErrorCode InternalServer = ErrorCode.Init(
                "SYS_001",
                "Lỗi hệ thống, vui lòng thử lại sau",
                500
        );

    }
}
