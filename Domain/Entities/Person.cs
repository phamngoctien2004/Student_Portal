using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Person
    {
        public string Code { get; set; } = string.Empty;

        public string Name { get; set; } = string.Empty;
        public Gender Gender { get; set; }
        public DateOnly Brith { get; set; }
        public string Address { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string Avatar {get; set; } = string.Empty;
    }
}
