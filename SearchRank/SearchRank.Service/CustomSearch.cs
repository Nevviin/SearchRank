using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using SearchRank.Core.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace SearchRank.Service
{
    public class CustomSearch:ICommonSearch
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger _logger;
        private readonly IMemoryCache _memoryCache;
        public CustomSearch(IConfiguration configuration, 
            ILogger<CustomSearch> logger,
            IMemoryCache memoryCache
            )
        {
            _configuration = configuration;
            _logger = logger;
            _memoryCache = memoryCache;
        }

        public string GetBingResult(string urlToSearch, string searchKeyWords, string noOfResults)
        {
            try
            {
                var formattedKeyWords = Uri.EscapeDataString(searchKeyWords);
                var baseUrl = _configuration.GetValue<string>("Bing:url");
                var searchUrl = string.Format(baseUrl, formattedKeyWords, noOfResults);
                string apiResponse = string.Empty;
                var httpClient = new HttpClient();
                using (Task<HttpResponseMessage> HttpResponseMessage = httpClient.GetAsync(searchUrl))
                {
                    HttpResponseMessage.Wait();
                    Task<string> responseResult = HttpResponseMessage.Result.Content.ReadAsStringAsync();
                    responseResult.Wait();
                    apiResponse = responseResult.Result;
                    HttpResponseMessage.Result.EnsureSuccessStatusCode();
                }
                return apiResponse;
            }
            catch (Exception exception)
            {
                throw;
            }

        }

     
        public string GetGoogleResult(string urlToSearch, string searchKeyWords, string noOfResults)
        {
            try
            {
                var formattedKeyWords = Uri.EscapeDataString(searchKeyWords);
                var baseUrl = _configuration.GetValue<string>("Google:url");
                var searchUrl = string.Format(baseUrl, formattedKeyWords, noOfResults);
                string apiResponse = string.Empty;
                var httpClient = new HttpClient();
                
                using (Task<HttpResponseMessage> HttpResponseMessage = httpClient.GetAsync(searchUrl))
                {
                    HttpResponseMessage.Wait();
                    Task<string> responseResult = HttpResponseMessage.Result.Content.ReadAsStringAsync();
                    responseResult.Wait();
                    apiResponse = responseResult.Result;
                    HttpResponseMessage.Result.EnsureSuccessStatusCode();
                }
                return apiResponse;
            }
            catch (Exception exception)
            {
                throw;
            }
        }
    }
}
