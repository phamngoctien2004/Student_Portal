
using Application.DTOs.Corhot;
using Application.DTOs.Student;
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
    public class CohortMapper : Profile
    {
        public CohortMapper()
        {
            CreateMap<Cohort, CohortResponse>();
        }
    }
}
