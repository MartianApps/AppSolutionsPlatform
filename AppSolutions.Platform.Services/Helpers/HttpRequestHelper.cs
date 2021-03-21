using Flurl;
using Flurl.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppSolutions.Platform.Services.Helpers
{
    public class HttpRequestHelper
    {
        private string _baseUrl;
        public HttpRequestHelper(string baseUrl)
        {
            _baseUrl = baseUrl;
        }

        public async Task PostData(string endpoint, object data)
        {
            await _baseUrl
                .AppendPathSegment(endpoint)
                .WithHeader("Accept", "application/json")
                .PostJsonAsync(data);
        }

        public async Task<TResponseData> PostData<TResponseData>(string endpoint, object data)
        {
            var response = await _baseUrl
                .AppendPathSegment(endpoint)
                .WithHeader("Accept", "application/json")
                .PostJsonAsync(data)
                .ReceiveJson<TResponseData>();
            return response;
        }
    }
}
