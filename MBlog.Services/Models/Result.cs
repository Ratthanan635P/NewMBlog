using MBlog.CallApi.Helpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace MBlog.CallApi.Models
{
    public class Result<TSuccess, TError>
    {
        public Enums.StatusCode StatusCode { get; set; }
        public TSuccess Success { get; set; }
        public TError Error { get; set; }
    }
}
