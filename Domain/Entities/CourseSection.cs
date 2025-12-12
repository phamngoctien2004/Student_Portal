using Core.Entities;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class CourseSection
    {
        public int Id { get; set; }
        public string Code { get; set; } = string.Empty;

        public int TeacherId { get; set; }
        public string TeacherName { get; set; } = string.Empty;
        public Teacher? Teacher { get; set; }
        public int CourseId { get; set; }
        public Course? Course { get; set; }

        public int SemesterId { get; set; }
        public Semester? Semester { get; set; }

        public int DayOfWeek { get; set; }

        public int Slot { get; set; }
        public DateOnly StartDate { get; set; }
        public DateOnly EndDate { get; set; }
        public ICollection<Enrollment> Enrollments { get; set; } = [];

    }
}
