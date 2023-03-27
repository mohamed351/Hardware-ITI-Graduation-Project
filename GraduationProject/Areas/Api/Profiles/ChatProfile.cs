using AutoMapper;
using GraduationProject.Areas.Api.ViewModels;
using GraduationProject.Models;
using GraduationProject.Repositry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GraduationProject.Areas.Api.Profiles
{
    public class ChatProfile:Profile
    {
        public ChatProfile()
        {
            CreateMap<ChatMessages, ChatMessageViewModel>()
                .ForMember(a => a.SourceID, c => c.MapFrom(c => c.SourceUserId))
                .ForMember(a => a.SourceName, c => c.MapFrom(c => $"{c.SourceUser.FirstName} {c.SourceUser.LastName}"))
                .ForMember(a => a.DestinationID, c => c.MapFrom(c => c.DestinationUserID))
                .ForMember(a => a.DestinationName, c => c.MapFrom(c => $"{c.DestinationUser.FirstName} {c.DestinationUser.LastName}"));
            CreateMap<ApplicationUser, UsersUnReadMessageViewModel>()
                .ForMember(a => a.Name, c => c.MapFrom(c => $"{c.FirstName} {c.LastName}"))
                .ForMember(a => a.UserId, c => c.MapFrom(c => c.Id));


        }
      
    }
}
