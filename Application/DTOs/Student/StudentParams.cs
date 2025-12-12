using Application.DTOs.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Student
{
    public class StudentParams : BaseQueryDTO
    {
        public int? CorhortId { get; set; }

    }
}
