using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.CourseSection
{
    public class CourseSectionRequest
    {
        public int Id { get; set; }
        public int TeacherId { get; set; }
        public int CourseId { get; set; }
        public DateOnly StartDate { get; set; }
        public int SemesterId { get; set; }
        public int Slot { get; set; }
    }
}
