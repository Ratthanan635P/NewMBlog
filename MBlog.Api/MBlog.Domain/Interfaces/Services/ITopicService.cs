using MBlog.Domain.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace MBlog.Domain.Interfaces.Services
{
	public interface ITopicService
	{
		List<TopicDto> GetAll();
		bool Add(TopicDto topicDto);
		bool Update(TopicDto topicDto);
	}
}
