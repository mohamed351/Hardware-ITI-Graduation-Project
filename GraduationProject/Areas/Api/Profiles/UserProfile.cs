using AutoMapper;
using GraduationProject.Areas.Api.ViewModels;
using GraduationProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GraduationProject.Areas.Api.Profiles
{
    public class UserProfile:Profile
    {

        public UserProfile()
        {
            //CreateMap<UserInformationViewModel, ApplicationUser>()
            //    .ForMember(a => $"{a.FirstName} {a.LastName} ", a => a.MapFrom(a => a.Name))
            //    .ForMember(a => a.Address, a => a.MapFrom(a => a.Address))
            //    .ForMember(a => a.Area.AreaName, a => a.MapFrom(a => a.Area))
            //    .ForMember(a => a.Area.City.CityName, a => a.MapFrom(a => a.City));


            CreateMap<ApplicationUser, UserInformationViewModel > ()
            .ForMember(a => a.Name, a => a.MapFrom(a =>$"{a.FirstName} {a.LastName}" ))
            .ForMember(a => a.Address, a => a.MapFrom(a => a.Address))
            .ForMember(a => a.Area, a => a.MapFrom(a => a.Area.AreaName))
            .ForMember(a => a.City, a => a.MapFrom(a => a.Area.City.CityName));



        }
    }
}
