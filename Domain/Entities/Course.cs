using Core.Entities;
using Domain.Constants;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
	public class Course
	{
		public int Id { get; set; }
		public string Code { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
		public double Price { get; private set; }

        private int _credit;
        public int Credit {
			get => _credit;
			set
			{
				_credit = value;
				Price = EntityConstant.CREDIT_PRICE * Credit;
            } 
		}


		public ICollection<CourseSection> CourseSections { get; set; } = [];
		public ICollection<Major> Majors { get; set; } = [];
		
    }
}
