using Application.DTOs.Course;
using Application.DTOs.Enrollment;
using Application.DTOs.Semester;
using Application.DTOs.Teacher;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Application.DTOs.CourseSection
{
    public class CourseSectionResponse
    {
        public int Id { get; set; }
        public string? Code { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public TeacherResponse? Teacher { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public CourseResponse? Course { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public SemesterResponse? Semester { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public List<EnrollmentResponse>? Enrollments { get; set; }

        public int DayOfWeek { get; set; }

        public ScheduleSlot Slot { get; set; }
        public DateOnly StartDate { get; set; }
        public DateOnly EndDate { get; set; }
    }
}
