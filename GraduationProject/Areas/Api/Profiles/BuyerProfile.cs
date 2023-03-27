using AutoMapper;
using GraduationProject.Areas.Api.ViewModels;
using GraduationProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GraduationProject.Areas.Api.Profiles
{
    public class BuyerProfile:Profile
    {
        public BuyerProfile()
        {
            CreateMap<Buys, ProductBuyersViewModel>()
                .ForMember(a => a.UserId, a => a.MapFrom(a => a.User.Id))
                .ForMember(a => a.UserName, a => a.MapFrom(a => $"{a.User.FirstName} {a.User.LastName}"))
                .ForMember(a => a.ProductId, a => a.MapFrom(a => a.UserProductId))
                .ForMember(a => a.ProductName, a => a.MapFrom(a => a.UserProduct.Name))
                .ForMember(a => a.Price, a => a.MapFrom(a => a.UserProduct.Price))
                .ForMember(a => a.Condition, a => a.MapFrom(a => a.UserProduct.Condition))
                .ForMember(a => a.Description, a => a.MapFrom(a => a.UserProduct.Description))
                .ForMember(a => a.ProductOwnerID, a => a.MapFrom(a => a.UserProduct.UserId));


            CreateMap<Buys, ProductSellersViewModel>()
              .ForMember(a => a.UserId, a => a.MapFrom(a => a.UserProduct.User.Id))
              .ForMember(a => a.UserName, a => a.MapFrom(a => $"{a.UserProduct.User.FirstName} {a.UserProduct.User.LastName}"))
              .ForMember(a => a.ProductId, a => a.MapFrom(a => a.UserProductId))
              .ForMember(a => a.ProductName, a => a.MapFrom(a => a.UserProduct.Name))
              .ForMember(a => a.Price, a => a.MapFrom(a => a.UserProduct.Price))
              .ForMember(a => a.Condition, a => a.MapFrom(a => a.UserProduct.Condition))
              .ForMember(a => a.Description, a => a.MapFrom(a => a.UserProduct.Description))
              .ForMember(a => a.IsReviewed, a => a.MapFrom(a => a.IsReview));

              
              


            //CreateMap<ApplicationUser, ProductBuyersViewModel>()
            //    .ForMember(a => a.UserId, a => a.MapFrom(c => c.Id))
            //    .ForMember(a => a.UserName, a => a.MapFrom(a => $"{a.FirstName} {a.LastName}"));


            //CreateMap<Buys, ProductBuyerInfoViewModel>()
            //    .ForMember(a => a.ProductOwnerID, a => a.MapFrom(a => a.UserId))
            //     .ForMember(a => a.ProductName, a => a.MapFrom(a => a.UserProduct.Name))
            //     .ForMember(a => a.Price, a => a.MapFrom(a => a.UserProduct.Price))
            //     .ForMember(a => a.Condition, a => a.MapFrom(a => a.UserProduct.Condition))
            //     .ForMember(a => a.Description, a => a.MapFrom(a => a.UserProduct.Description));



        }
    }
}
