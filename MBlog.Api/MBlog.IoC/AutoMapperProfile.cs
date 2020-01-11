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
            CreateMap<Topic, TopicDto>();
            CreateMap<User, ProfileDto>();

            #endregion

            #region Dto2Entity

            CreateMap<UserDto, User>();
            CreateMap<TopicDto, Topic>();
            CreateMap<ProfileDto, User>();


            #endregion

            #region Dto2ViewModel



            #endregion

            #region ViewModel2Dto

            CreateMap<BlogCommand, BlogDto>();
            CreateMap<BlogDto, BlogCommand>();

            #endregion

        }
    }
}
