using HtmlAgilityPack;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using SearchRank.Core.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchRank.Service
{
    public class ResultParser : ICommonParse
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger _logger;

        private readonly IMemoryCache _memoryCache;
      


        public ResultParser(IConfiguration configuration, ILogger<ResultParser> logger, IMemoryCache memoryCache)
        {
            _configuration = configuration;
            _logger = logger;
            _memoryCache = memoryCache;
        }

        public string GetBingPageRank(string resultsHtml, string urlToSearch)
        {
            try
            {
                
                var searchFilter = _configuration.GetValue<string>("Bing:searchFilter");
                var document = new HtmlDocument();
                document.LoadHtml(resultsHtml);
                var htmlNodes = document.DocumentNode.SelectNodes(searchFilter);
                int counter = 0;
                var searchIndex = new StringBuilder();
                foreach (HtmlNode div in htmlNodes)
                {
                    counter = counter + 1;
                    if (div.InnerHtml.ToString().IndexOf(urlToSearch) > -1)
                    {
                        searchIndex.Append((searchIndex.Length != 0 ? ", " : "") + counter.ToString());
                    }
                }
                
                return searchIndex.ToString();
            }
            catch (Exception exception)
            {
                throw;
            }
            

        }
    
        public string GetGooglePageRank(string resultsHtml, string urlToSearch, string keyWords)
        {
            try
            {
                string cacheKey = string.Format("{0}_{1}", "google", keyWords);
                var cacheExpiry = Int32.Parse(_configuration.GetValue<string>("Cache:expiryInMins"));
                var searchFilter = _configuration.GetValue<string>("Google:searchFilter");
                var firstChild = _configuration.GetValue<string>("Google:firstChild");
                var document = new HtmlDocument();
                document.LoadHtml(resultsHtml);
                var filteredResults = document.DocumentNode.SelectNodes(searchFilter);
                var htmlNodes = filteredResults.Where(x => x.FirstChild.Name == firstChild).ToList();
                int counter = 0;
                var searchIndex = new StringBuilder();
                foreach (HtmlNode div in htmlNodes)
                {
                    counter = counter + 1;
                    if (div.InnerHtml.ToString().IndexOf(urlToSearch) > -1)
                    {
                        searchIndex.Append((searchIndex.Length != 0 ? ", " : "") + counter.ToString());
                    }
                }
                _memoryCache.Set(cacheKey, searchIndex.ToString(), TimeSpan.FromMinutes(cacheExpiry));
                return searchIndex.ToString();
            }
            catch (Exception exception)
            {
                throw;
            }
        }
    }
    }
