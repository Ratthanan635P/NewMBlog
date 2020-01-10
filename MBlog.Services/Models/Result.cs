using MBlog.Services.Helpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace MBlog.Services.Models
{
    public class Result<TSuccess, TError>
    {
        public Enums.StatusCode StatusCode { get; set; }
        public TSuccess Success { get; set; }
        public TError Error { get; set; }
    }
}
