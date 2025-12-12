using Core.Entities;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Enrollment
    {
        public int Id { get; set; }
        public int StudentId { get; set; }
        public Student? Student { get; set; }
        public int CourseSectionId { get; set; }
        public CourseSection? CourseSection { get; set; }
        public DateTime EnrollmentDate { get; set; }

        public double Attendance { get; set; }
        public double Midterm { get; set; }
        public double FinalExam { get; set; }
        public double TotalScore { get; set; }
        public bool? IsPass { get; set; } = false;
        public EnrollmentStatus Status { get; set; } = EnrollmentStatus.Inprogress;

        public void SetTotalScore()
        {
            TotalScore = (0.1 * Attendance + 0.3 * Midterm + 0.6 * FinalExam);
            IsPass = TotalScore > 5;
        }
    }
}
