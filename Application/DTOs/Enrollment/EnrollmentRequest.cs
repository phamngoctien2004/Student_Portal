using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Enrollment
{
    public class EnrollmentRequest
    {
        public int Id { get; set; }
        public int StudentId { get; set; }
        public int CourseSectionId { get; set; }
    }
}
