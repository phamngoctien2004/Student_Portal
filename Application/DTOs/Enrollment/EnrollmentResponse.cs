using Application.DTOs.CourseSection;
using Application.DTOs.Student;
using Domain.Enums;
using System.Text.Json.Serialization;

namespace Application.DTOs.Enrollment
{
    public class EnrollmentResponse
    {
        public int Id { get; set; }
        //[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        //public StudentResponse? Student { get; set; }
        //[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        //public CourseSectionResponse? CourseSection { get; set; }
        //public DateTime EnrollmentDate { get; set; }

        //public double? Attendance { get; set; } = null;
        //public double? Midterm { get; set; } = null;
        //public double? FinalExam { get; set; } = null;
        //public double? TotalScore { get; set; } = null;
        //public bool? IsPass { get; set; } = null;

        public string? TeacherName { get; set; }
        public string? StudentName { get; set; }
        public string? CourseCode { get; set; }
        public string? CourseName { get; set; }
        public int Credit { get; set; }
        public int DayOfWeek { get; set; }

        public int Slot { get; set; }
        public DateOnly StartDate { get; set; }
        public DateOnly EndDate { get; set; }

        public double? Attendance { get; set; } = null;
        public double? Midterm { get; set; } = null;
        public double? FinalExam { get; set; } = null;
        public double? TotalScore { get; set; } = null;
        public EnrollmentStatus Status { get; set; }
        public bool? IsPass { get; set; } = null;
    }
}
