using MBlog.CallApi.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MBlog.CallApi.Service.Interfaces
{
    public interface IBaseService
    {
        Task<Result<TSuccess, TError>> GetMethodAsync<TSuccess, TError>(Uri url);
        Task<Result<TSuccess, TError>> GetMethodBearerAsync<TSuccess, TError>(Uri url, string accessToken);
        Task<Result<TSuccess, TError>> PostMethodAsync<TSuccess, TError>(Uri url, object parmModel);
        Task<Result<TSuccess, TError>> PostMethodBearerAsync<TSuccess, TError>(Uri url, object parmModel, string accessToken);
    }
}
