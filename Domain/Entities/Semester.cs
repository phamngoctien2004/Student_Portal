using Domain.Entities;
using Domain.Enums;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class Semester
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public SemesterType SemesterType { get; set; }
        public DateOnly StartDate { get; set; }
        public int Year { get; set; }

        public bool IsJoin { get; set; } = false;
    }
}
