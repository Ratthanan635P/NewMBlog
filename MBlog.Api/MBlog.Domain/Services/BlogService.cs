using AutoMapper;
using MBlog.Domain.Dtos;
using MBlog.Domain.Entities.MBlogEntities;
using MBlog.Domain.Interfaces.Repositories;
using MBlog.Domain.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MBlog.Domain.Services
{
	public class BlogService : IBlogService
	{
		private readonly IBlogRepository _blogRepository;
		private readonly IFollowingRepository _followingRepository;
		private readonly IFavoriteRepository _favoriteRepository;
		private readonly IMapper _mapper;
		public BlogService(IBlogRepository blogRepository, IFollowingRepository followingRepository, IFavoriteRepository favoriteRepository,
			IMapper mapper)
		{
			_blogRepository = blogRepository;
			_followingRepository = followingRepository;
			_favoriteRepository = favoriteRepository;
			_mapper = mapper;
		}
		public bool AddBlog(BlogDto blogDto)
		{
			//throw new NotImplementedException();
			Blog blogDetail = new Blog()
			{
				Detail= blogDto.Detail,
				ImageHead = blogDto.ImageHead,
				ImagePath = blogDto.ImagePath,
				OwnerId=blogDto.OwnerId,
				TopicId=blogDto.TopicId,
				Title=blogDto.Title		
			};
			  _blogRepository.Add(blogDetail);
			return true;
		}

		public List<BlogDto> GetBlogByUserId(int userId)
		{
			//throw new NotImplementedException();
			var result = _blogRepository.GetBlogsByUserId(userId);
			var data = result.Select(b => new BlogDto
			{
				Detail=b.Detail,
				ImageHead=b.ImageHead,
				ImagePath=b.ImagePath,
				Owner = _mapper.Map<UserDto>(b.Owner),
				OwnerId =b.OwnerId,
				Title=b.Title,
				TopicId=b.TopicId,
				Topic = _mapper.Map<TopicDto>(b.Topic),
			}).ToList();
			return data;
		}
	}
}
