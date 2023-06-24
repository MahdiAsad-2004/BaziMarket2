using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration.Configuration;
using System.Linq;
using System.Web;
using AutoMapper;
using BaziMarket2.Models;
using BaziMarket2.Models.ViewModel;

namespace BaziMarket2.App_Start
{
    public class AutoMapperConfig
    {
        public static IMapper mapper;

        public static void Configuration()
        {
            MapperConfiguration configuration = new MapperConfiguration(
            t =>
            {
                t.CreateMap<User, UserListViewModel>();
                t.CreateMap<UserListViewModel, User>();
                t.CreateMap<User, UserRegisterViewModel>();
                t.CreateMap<UserRegisterViewModel, User>();
                t.CreateMap<UserRegisterListViewModel, User>();
                t.CreateMap<User,UserRegisterListViewModel>();
                t.CreateMap<ProductAddViewModel, Product>();
                t.CreateMap<Product, ProductAddListViewModel>();
                t.CreateMap<Product, ProductAllViewModel>();
                t.CreateMap<ProductAllViewModel,Product>();
                t.CreateMap<DescriptionAddViewModel,Description>();
                t.CreateMap<PictureAddViewModel,Picture>();
                t.CreateMap<SpecificationAddViewModel,Specification>();
                t.CreateMap<PropertyAddViewModel,Property>();
                t.CreateMap<Comment,CommentEditViewModel>();
                t.CreateMap<Question,QuestionEditViewModel>();
                t.CreateMap<CategoryAddViewModel,Category>();
                t.CreateMap<DiscountAddViewModel,Discount>();
                t.CreateMap<User,UserProfileViewModel>();
                t.CreateMap<UserProfileViewModel,User>();
                t.CreateMap<User,UserChangePasswordViewModel>();
                t.CreateMap<Product,ProductsViewModel>();



            }
            );

            mapper = configuration.CreateMapper();
        }
        


    }
}