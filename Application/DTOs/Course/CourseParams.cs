using Application.DTOs.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Course
{
    public class CourseParams : BaseQueryDTO
    {
        // filter
        public int? FacultyId { get; set; }



    }
}
