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
			Blog blogDetail = new Blog()
			{
				Detail = blogDto.Detail,
				ImageHead = blogDto.ImageHead,
				ImagePath = blogDto.ImagePath,
				OwnerId = blogDto.OwnerId,
				TopicId = blogDto.TopicId,
				Title = blogDto.Title
			};
			_blogRepository.Add(blogDetail);
			_blogRepository.SaveChange();
			return true;
		}

		public List<BlogDto> GetBlogByUserId(int userId)
		{
			var result = _blogRepository.GetBlogsByUserId(userId);
			var data = result.Select(b => new BlogDto
			{
				Id = b.Id,
				Detail = b.Detail,
				ImageHead = b.ImageHead,
				ImagePath = b.ImagePath,
				Owner = _mapper.Map<ProfileDto>(b.Owner),
				OwnerId = b.OwnerId,
				Title = b.Title,
				TopicId = b.TopicId,
				Createtime = b.CreateDateTime,
				Topic = _mapper.Map<TopicDto>(b.Topic),
			}).ToList();
			return data;
		}

		public List<ProfileDto> GetSubscribesByUserId(int userId)
		{
			var Followings = _followingRepository.GetDataFollowingByUserId(userId);
			List<ProfileDto> profileDtos = new List<ProfileDto>();
			Followings = Followings.Where(x => x.IsDelete == false).ToList();
			profileDtos = Followings.Select(f => new ProfileDto()
			{
				About = f.About,
				Email = f.Email,
				FullName = f.FullName,
				Id = f.Id,
				ImageProfile = f.ImageProfile,
				ImageProfilePath = f.ImageProfilePath,
				Following = true
			}).ToList();

			return profileDtos;
		}

		public bool SubscribesByUserId(int unSubUserId, int myUserId)
		{
			Following following = new Following()
			{
				FollowerId = myUserId,
				FollowingId = unSubUserId
			};
			var result = _followingRepository.GetFollowByUserId(unSubUserId, myUserId);
			if (result == null)
			{
				_followingRepository.Add(following);
				_followingRepository.SaveChange();
			}
			else
			{
				if (result.IsDelete == true)
				{
					result.IsDelete = false;
					_followingRepository.Update(result);
					_followingRepository.SaveChange();
				}
			}
			return true;
		}

		public bool UnSubscribesByUserId(int unSubUserId, int myUserId)
		{
			Following following = _followingRepository.GetFollowByUserId(unSubUserId, myUserId);
			if (following != null)
			{
				following.IsDelete = true;
				_followingRepository.Update(following);
				_followingRepository.SaveChange();
			}
			return true;
		}
		public bool UnFavoritesByUserId(int blogId, int UserId)
		{
			Favorite favorite = _favoriteRepository.GetFavoritesByUserId(blogId, UserId);
			if (favorite != null)
			{
				favorite.IsDelete = true;
				_favoriteRepository.Update(favorite);
				_favoriteRepository.SaveChange();
			}
			return true;
		}
		public bool FavoritesByUserId(int blogId, int UserId)
		{
			Favorite following = new Favorite()
			{
				BlogId = blogId,
				UserId = UserId
			};
			var result = _favoriteRepository.GetFavoritesByUserId(blogId, UserId);
			if (result == null)
			{
				_favoriteRepository.Add(following);
				_favoriteRepository.SaveChange();
			}
			else
			{
				if (result.IsDelete == true)
				{
					result.IsDelete = false;
					_favoriteRepository.Update(result);
					_favoriteRepository.SaveChange();
				}
			}
			return true;
		}

		public List<BlogDto> GetFavoritesByUserId(int userId)
		{
			var favorites = _favoriteRepository.GetDataFavoritesByUserId(userId);
			List<BlogDto> blogDtos = new List<BlogDto>();
			blogDtos = favorites.Where(x => x.IsDelete == false).Select(f => new BlogDto()
			{
				Id = f.Id,
				Detail = f.Detail,
				ImageHead = f.ImageHead,
				ImagePath = f.ImagePath,
				Owner = _mapper.Map<ProfileDto>(f.Owner),
				OwnerId = f.OwnerId,
				Title = f.Title,
				Createtime = f.CreateDateTime,
				Topic = _mapper.Map<TopicDto>(f.Topic),
				TopicId = f.TopicId
			}).ToList();
			return blogDtos;
		}

		public int GetFollowerByUserId(int Userid)
		{
			var Followers = _followingRepository.GetFollowerByUserId(Userid);
			return Followers;
		}

		public int GetFollowingByUserId(int Userid)
		{
			var Followings = _followingRepository.GetFollowingByUserId(Userid);
			return Followings;
		}
	}
}