using MBlog.Data.Contexts;
using MBlog.Domain.Entities.MBlogEntities;
using MBlog.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MBlog.Data.Repositories
{
	public class TopicRepository : BaseRepository, ITopicRepository
	{
		private readonly MBlogContext _context;

		public TopicRepository(MBlogContext context) : base(context)
		{
			_context = context;
			if (_context.ChangeTracker != null)
			{
				_context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
			}
		}
		public List<Topic> GetAll()
		{
			//throw new NotImplementedException();
			var topic = _context.Topics.ToList();
			return topic;
		}

		public Topic GetByName(string topicName)
		{
			var topic = _context.Topics.Where(t=>t.TopicName==topicName&&t.IsDelete==false).FirstOrDefault();
			return topic;
		}
	}
}
