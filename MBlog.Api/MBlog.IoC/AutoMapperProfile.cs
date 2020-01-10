using System;
using AutoMapper;
using MBlog.Domain.Dtos;
using MBlog.Domain.Entities.MBlogEntities;

namespace MBlog.IoC
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            #region Entity2Dto

            CreateMap<User, UserDto>();

            #endregion

            #region Dto2Entity

            CreateMap<UserDto, User>();


            #endregion

            #region Dto2ViewModel



            #endregion

            #region ViewModel2Dto



            #endregion

        }
    }
}
