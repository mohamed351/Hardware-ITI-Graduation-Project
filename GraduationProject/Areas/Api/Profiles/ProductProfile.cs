using AutoMapper;
using GraduationProject.Areas.Api.ViewModels;
using GraduationProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GraduationProject.Areas.Api.Profiles
{
    public class ProductProfile:Profile
    {

        public ProductProfile()
        {
            CreateMap<ProductAttributes, ProductAttributesViewModel>()
                .ForMember(a => a.AttributeName, a => a.MapFrom(a => a.AttributeName))
                .ForMember(a => a.Value, a => a.MapFrom(a => a.Value));

            CreateMap<Product, SearchProductViewModel>()
                .ForMember(a => a.ProductId, a => a.MapFrom(a => a.Id))
                .ForMember(a => a.Name, a => a.MapFrom(a => a.Name))
                .ForMember(a => a.ModelID, a => a.MapFrom(a => a.ModelId))
                .ForMember(a => a.ModelName, a => a.MapFrom(a => a.Model.Name))
                .ForMember(a => a.BrandID, a => a.MapFrom(a => a.BrandId))
                .ForMember(a => a.BrandName, a => a.MapFrom(a => a.Brand.Name))
                .ForMember(a => a.Attributes, a => a.MapFrom(a => a.ProductAttributes));

            CreateMap<UserProductImages, ViewModelUserProductImages>()
             .ForMember(a => a.Image, a => a.MapFrom(a => a.Images));

            CreateMap<UserProduct, UserProductViewModel>()
                .ForMember(a => a.UserProductId, a => a.MapFrom(a => a.Id))
                .ForMember(a => a.ProductId, a => a.MapFrom(a => a.ProductId))
                .ForMember(a => a.Price, a => a.MapFrom(a => a.Price))
                .ForMember(a => a.Name, a => a.MapFrom(a => a.Name))
                .ForMember(a => a.Condition, a => a.MapFrom(a => a.Condition))
                .ForMember(a => a.Images, a => a.MapFrom(a => a.UserProductImages))
                .ForMember(a => a.UserId, a => a.MapFrom(a => a.UserId));
          

               

        }
    }
}
