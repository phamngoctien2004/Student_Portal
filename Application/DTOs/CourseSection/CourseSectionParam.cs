using Application.DTOs.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.CourseSection
{
    public class CourseSectionParam : BaseQueryDTO
    {
        public int? SemesterId { get; set; }
        public int? CourseId { get; set; }
        public int? TeacherId { get; set; }
    }
}
