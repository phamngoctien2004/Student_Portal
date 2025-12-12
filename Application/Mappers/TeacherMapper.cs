
using Application.DTOs.Teacher;
using AutoMapper;
using Core.Entities;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Mappers
{
    public class TeacherMapper : Profile
    {
        public TeacherMapper()
        {
            CreateMap<Teacher, TeacherResponse>();
            CreateMap<TeacherRequest, Teacher>();
        }
    }
}
