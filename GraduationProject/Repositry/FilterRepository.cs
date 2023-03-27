using GraduationProject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace GraduationProject.Repositry
{
    public class FilterRepository : Repositry<Filter, int>, IFIlterRepository
    {
        private readonly ApplicationDbContext context;
        public FilterRepository(ApplicationDbContext context)
         : base(context)
        {
            this.context = context;
        }

        public List<Dictionary<string, Object>> GetFilterByCategory(int ID)
        {
            string query = "select * from Filters where CategoryId = " + ID;
            List<Filter> filters = context.Filters.FromSqlRaw(query).ToList();
            List<Dictionary<string, Object>> JObect = new List<Dictionary<string, Object>>();
           // List<string> titleNames = new List<string>();
            foreach (Filter f in filters)
            {
             //   titleNames.Add(f.Name);

                Dictionary<string, Object> dic = new Dictionary<string, Object>()
                {
                {
                    "TitleName",
                    f.Name
                },
                {
                    "FilterType",
                    f.FilterType
                },
                {
                    "QueryKey",
                    f.QueryKey
                }
                };
               

                if (f.FilterType == "StaticChoice")
                {
                    // GetStaticChoices(ID);
                    List<FilterChoice> filterChoices = GetStaticChoicesByCategory(ID);
                    List<string> choices = new List<string>();
                    foreach (FilterChoice fc in filterChoices)
                    {
                        choices.Add(fc.Choice);
                    }
                    dic.Add("Choices", choices);
                }
                if (f.FilterType== "DynamicChoice")
                {
                    string method = " select * from DynamicChoice where FilterId = " + f.FilterId;
                    List<DynamicChoice> dynamicChoice = context.dynamicChoices.FromSqlRaw(method).ToList();
                    if (dynamicChoice[0].Procedure == "GetBrandsByCategory")
                    {
                        List<Brand> brands = GetBrandsByCategory(ID);
                        List<string> choices = new List<string>();
                        foreach (Brand b in brands)
                        {
                            choices.Add(b.Name);
                        }
                        string[] distChoices = choices.Distinct().ToArray();
                        dic.Add("Choices", distChoices);
                    }            
                }
                JObect.Add(dic);
            }
            return JObect;
        }

        //public List<string> GetStaticChoices(int id)
        //{
        //    List<FilterChoice> filterChoices = GetStaticChoicesByCategory(id);
        //    List<string> choices = new List<string>();
        //    foreach (FilterChoice fc in filterChoices)
        //    {
        //        choices.Add(fc.Choice);
        //    }
        //    return choices;
        // dic.Add("Choices", choices);
        //}

        //public List<string> GetDynamicChoices(int ID)
        //{
        //    string query = "select * from Filters where CategoryId = " + ID;
        //    List<Filter> filters = context.Filters.FromSqlRaw(query).ToList();
        //    List<string> choices = new List<string>();
        //    foreach (Filter f in filters)
        //    {
        //        if (f.FilterType == "DynamicChoice")
        //        {
        //            string method = " select * from DynamicChoice where FilterId = " + f.FilterId;
        //            List<DynamicChoice> dynamicChoice = context.dynamicChoices.FromSqlRaw(method).ToList();
        //            if (dynamicChoice[0].Procedure == "GetBrandsByCategory")
        //            {
        //                List<Brand> brands = GetBrandsByCategory(ID);
        //                //List<string> choices = new List<string>();
        //                foreach (Brand b in brands)
        //                {
        //                    choices.Add(b.Name);
        //                }
        //                // dic.Add("Choices", choices);
        //            }
        //        }
        //    }return choices;
        //}

        //public int GetProductImages(UserProduct products)
        //{
        //           }

        public List<Brand> GetBrandsByCategory(int categoryId)
        {
            string query = " Select b.* from Brand b " +
                            " inner join Product p" +
                            " on p.BrandId = b.Id " +
                            " inner join Model d " +
                            " on d.Id = p.ModelId " +
                            " inner join Category c " +
                            " on c.Id = d.CategoryId and c.Id = " + categoryId;
            List<Brand> brands = context.Brand.FromSqlRaw(query).ToList();
            return brands;
        }

        public List<FilterChoice> GetStaticChoicesByCategory(int categoryId)
        {
            string query = " select c.* " +
                          " from FilterChoices c " +
                          " inner join Filter_FilterChoice f " +
                          " on f.FilterChoiceId = c.ID " +
                          " inner join Filters s " +
                          " on s.FilterId = f.FilterId and s.CategoryId = " + categoryId;
            List<FilterChoice> conditions = context.FilterChoices.FromSqlRaw(query).ToList();
            return conditions;
        }


        //public decimal GetMaximumPrice(int categoryId)
        //{
        //    string query = " select max(Price) from [dbo].[UserProduct] u " +
        //                   " inner join[dbo].[Product] p " +
        //                   " on p.[Id]=u.[ProductId] " +
        //                   " inner join[dbo].[Model] m " +
        //                   " on m.[Id] = p.[ModelId] " +
        //                   " inner join[dbo].[Category] c " +
        //                   " on c.[Id] = m.[CategoryId] and c.[Id]= " + categoryId;
        //    var MaxPrice = context.;
        //}

        public List<UsersRatings> GetRatings(int rates)
        {
            string rateQuery = "select * from Users_Ratings where Rating >= "+ rates;
            return context.UsersRatings.FromSqlRaw(rateQuery).ToList();
        }


        public List<UserProduct> GetFilterdProducts(string query)
        {
            List<UserProduct> products= context.UserProduct.FromSqlRaw(query).ToList();
            foreach (UserProduct p in products)
            {
                string img = " select * from UserProduct_Images " +
                             " where UserProductId = " + p.Id;
                List<UserProductImages> productImg = context.UserProductImages.FromSqlRaw(img).ToList();
                //products..Add(productImg[0]);
                p.UserProductImages.Add(productImg[0]);
            }
            return products;

        }
    }
}
