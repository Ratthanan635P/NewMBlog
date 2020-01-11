using MBlog.CallApi.Models;
using MBlog.CallApi.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MBlog.CallApi.Service.Implements
{
	public class TopicService : BaseService, ITopicService
	{
		public async Task<Result<SuccessModel, ErrorModel>> AddTopic(TopicDto topicDto)
		{
			Uri url = new Uri(BaseUriUser, $"/Topic/AddTopic");

			Result<SuccessModel, ErrorModel> result = await PostMethodAsync<SuccessModel, ErrorModel>(url, topicDto);

			return result;
		}

		public async Task<Result<List<TopicDto>, ErrorModel>> GetAll()
		{
			Uri url = new Uri(BaseUriUser, $"/Topic/GetAll");

			Result<List<TopicDto>, ErrorModel> result = await GetMethodAsync<List<TopicDto>, ErrorModel>(url);

			return result;
		}
	}
}
