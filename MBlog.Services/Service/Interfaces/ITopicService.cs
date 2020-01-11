using MBlog.CallApi.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MBlog.CallApi.Service.Interfaces
{
	public interface ITopicService:IBaseService
	{		
	    Task<Result<List<TopicDto>, ErrorModel>> GetAll();
		Task<Result<SuccessModel, ErrorModel>> AddTopic(TopicDto topicDto);
	}
}
