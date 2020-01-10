using MBlog.Services.Helpers;
using MBlog.Services.Interfaces;
using MBlog.Services.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace MBlog.Services.Implements
{
    public class BaseService:IBaseService
    {
        private HttpResponseMessage response;
        private readonly HttpClient client = new HttpClient();

        private StringContent GetHttpContent(object parmModel)
        {
            string jsonPayload = JsonConvert.SerializeObject(parmModel);
            var content = new StringContent(jsonPayload, Encoding.UTF8, "application/json");
            return content;
        }

        public async Task<Result<TSuccess, TError>> GetMethodAsync<TSuccess, TError>(Uri url)
        {
            Result<TSuccess, TError> output = new Result<TSuccess, TError>();

            response = await client.GetAsync(url);

            var outputResult = await SwitchCheckStatusCode(output);

            return outputResult;
        }

        public async Task<Result<TSuccess, TError>> GetMethodBearerAsync<TSuccess, TError>(Uri url, string accessToken)
        {
            Result<TSuccess, TError> output = new Result<TSuccess, TError>();

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

            response = await client.GetAsync(url);

            var outputResult = await SwitchCheckStatusCode(output);

            return outputResult;
        }

        public virtual async Task<Result<TSuccess, TError>> PostMethodAsync<TSuccess, TError>(Uri url, object parmModel)
        {
            Result<TSuccess, TError> output = new Result<TSuccess, TError>();

            StringContent content = GetHttpContent(parmModel);


            response = await client.PostAsync(url, content);


            var outputResult = await SwitchCheckStatusCode(output);

            return outputResult;
        }

        public async Task<Result<TSuccess, TError>> PostMethodBearerAsync<TSuccess, TError>(Uri url, object parmModel, string accessToken)
        {
            Result<TSuccess, TError> output = new Result<TSuccess, TError>();

            StringContent content = GetHttpContent(parmModel);

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

            response = await client.PostAsync(url, content);

            var outputResult = await SwitchCheckStatusCode(output);

            return outputResult;
        }

        private async Task<Result<TSuccess, TError>> SwitchCheckStatusCode<TSuccess, TError>(Result<TSuccess, TError> output)
        {
            JsonSerializer serializer = new JsonSerializer();
            JsonTextReader json;
            using (var stream = await response.Content.ReadAsStreamAsync())
            using (var reader = new StreamReader(stream))
            using (json = new JsonTextReader(reader))

                switch (response.StatusCode)
                {
                    case System.Net.HttpStatusCode.BadRequest:
                        output.Error = serializer.Deserialize<TError>(json);

                        output.StatusCode = Enums.StatusCode.BadRequest;
                        break;

                    case System.Net.HttpStatusCode.InternalServerError:
                        output.Error = serializer.Deserialize<TError>(json);

                        output.StatusCode = Enums.StatusCode.InternalServerError;
                        break;

                    case System.Net.HttpStatusCode.NotFound:
                        output.Error = serializer.Deserialize<TError>(json);

                        output.StatusCode = Enums.StatusCode.NotFound;
                        break;

                    case System.Net.HttpStatusCode.OK:
                        output.Success = serializer.Deserialize<TSuccess>(json);

                        output.StatusCode = Enums.StatusCode.Ok;
                        break;

                    case System.Net.HttpStatusCode.Unauthorized:
                        output.Error = serializer.Deserialize<TError>(json);

                        output.StatusCode = Enums.StatusCode.Unauthorized;
                        break;
                    case System.Net.HttpStatusCode.Forbidden:
                        output.Error = serializer.Deserialize<TError>(json);

                        output.StatusCode = Enums.StatusCode.Forbidden;
                        break;
                    default:
                        break;
                }
            return output;
        }
    }
}
