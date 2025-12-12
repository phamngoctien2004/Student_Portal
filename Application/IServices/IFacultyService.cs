using Application.DTOs.Faculty;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.IServices
{
    public interface IFacultyService : IBaseService<Faculty, FacultyRequest, FacultyResponse, FacultyParam>
    {
    }
}
