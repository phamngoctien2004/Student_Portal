using Application.DTOs.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Teacher
{
    public class TeacherParams : BaseQueryDTO
    {
        public int? FacultyId { get; set; }
    }
}
