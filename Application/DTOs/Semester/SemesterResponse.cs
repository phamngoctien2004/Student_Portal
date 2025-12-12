using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Semester
{
    public class SemesterResponse
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Year { get; set; }
        public string? Description { get; set; }
        public DateOnly StartDate { get; set; }
    }
}
