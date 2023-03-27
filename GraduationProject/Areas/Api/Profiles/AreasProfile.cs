using AutoMapper;
using GraduationProject.Areas.Api.VIewModels;
using GraduationProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GraduationProject.Areas.Api.Profiles
{
    public class AreasProfile:Profile
    {


        public AreasProfile()
        {
            CreateMap<AreasViewModel, Area>()
              .ForMember(a => a.AreaName, a => a.MapFrom(b=>b.AreaName))
              .ForMember(a => a.ID, a => a.MapFrom(a => a.ID));

            CreateMap<AreasViewModel, Area>().ReverseMap()
            .ForMember(a => a.AreaName, a => a.MapFrom(b => b.AreaName))
            .ForMember(a => a.ID, a => a.MapFrom(a => a.ID));

        }
      
    }
}
