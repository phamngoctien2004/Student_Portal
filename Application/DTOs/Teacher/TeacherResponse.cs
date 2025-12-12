using Application.DTOs.Faculty;
using Application.DTOs.User;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Teacher
{
    public class TeacherResponse
    {
        public int Id { get; set; }
        public string? Name { get; set; } 
        public Gender Gender { get; set; }
        public DateOnly Brith { get; set; }
        public string? Address { get; set; }
        public string? Phone { get; set; }
        public string? Avatar { get; set; }
        public UserResponse? User { get; set; }
        public FacultyResponse? Faculty { get; set; }
    }
}
