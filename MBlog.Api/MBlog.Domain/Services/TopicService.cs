using AutoMapper;
using MBlog.Domain.Dtos;
using MBlog.Domain.Entities.MBlogEntities;
using MBlog.Domain.Interfaces.Repositories;
using MBlog.Domain.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace MBlog.Domain.Services
{
	public class TopicService : ITopicService
	{
		private readonly ITopicRepository _topicRepository;
		private readonly IMapper _mapper;
		public TopicService(ITopicRepository userRepository,
			IMapper mapper)
		{
			_topicRepository = userRepository;
			_mapper = mapper;
		}
		public bool Add(TopicDto topicDto)
		{
			try
			{
				var result = _topicRepository.GetByName(topicDto.TopicName);
				if (result == null)
				{
					Topic topic = new Topic()
					{
						TopicName = topicDto.TopicName,
						TopicDetail = topicDto.TopicDetail
					};
					_topicRepository.Add(topic);
					_topicRepository.SaveChange();
				}
				else
				{
					return false;
				}
				return true;
			}
			catch (Exception ex)
			{
				return false;
			}
			
		}

		public List<TopicDto> GetAll()
		{
			var topics = _topicRepository.GetAll();
			List<TopicDto> topicDtos = _mapper.Map<List<TopicDto>>(topics);
			if (topicDtos == null)
			{
				return null;
			}

			return topicDtos;
		}

		public bool Update(TopicDto topicDto)
		{
			try
			{
				var result = _topicRepository.GetByName(topicDto.TopicName);
				if (result != null)
				{
					result.TopicName = topicDto.TopicName;
					result.TopicDetail = topicDto.TopicDetail;
					
					_topicRepository.Update(result);
					_topicRepository.SaveChange();
					return true;
				}
				else
				{
					return false;
				}
				
			}
			catch (Exception ex)
			{
				return false;
			}
		}
	}
}
