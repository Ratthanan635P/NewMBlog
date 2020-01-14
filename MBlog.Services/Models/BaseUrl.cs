using System;
using System.Collections.Generic;
using System.Text;

namespace MBlog.CallApi.Models
{
    public class BaseUrl
    {
        public Uri BaseUriUser { get; private set; } = new Uri("http://192.168.1.29:30000/");
        //public Uri BaseUriUser { get; private set; } = new Uri("http://192.168.136.81:30000/");
    }
}
