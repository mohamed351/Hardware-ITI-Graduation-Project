using AutoMapper;
using GraduationProject.Areas.Api.VIewModels;
using GraduationProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GraduationProject.Areas.Api.Profiles
{
    public class CitiesProfile:Profile
    {
        public CitiesProfile()
        {
            CreateMap<City, CitiesViewModel>()
                .ForMember(a => a.CityName, a => a.MapFrom(a => a.CityName))
                .ForMember(a => a.ID, a => a.MapFrom(a => a.ID));

            CreateMap<City, CitiesViewModel>().ReverseMap()
               .ForMember(a => a.CityName, a => a.MapFrom(a => a.CityName))
               .ForMember(a => a.ID, a => a.MapFrom(a => a.ID));
        }

    }
}
