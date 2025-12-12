using Core.Entities;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Teacher : Person
    {
        public int Id { get; set; }

        public int FacultyId { get; set; }
        public Faculty? Faculty { get; set; }
        public int UserId { get; set; }
        public User? User { get; set; }

        public ICollection<CourseSection> CourseSections { get; set; } = [];
    }
}
