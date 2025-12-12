using Application.DTOs.Common;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Course
{
    public class CourseRequest
    {
        public int? Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int Credit { get; set; }

        public int FacultyId { get; set; }
    }
}
