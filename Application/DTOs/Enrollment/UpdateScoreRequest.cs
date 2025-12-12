using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Enrollment
{
    public class UpdateScoreRequest
    {
        public int Id { get; set; }
        public double Attendance { get; set; }
        public double MidTerm { get; set; }
        public double FinalExam { get; set; }

    }
}
