using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Enrollment
{
    public class ScheduleRequest
    {
        public int SemesterId { get; set; }
        public int StudentId { get; set; }
    }
}
