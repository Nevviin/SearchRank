using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using SearchRank.Core.DTO;
using SearchRank.Core.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SearchRank.Api.Controllers
{
    [Route("rank")]
    [ApiController]
    public class RankController : ControllerBase
    {
        private readonly ILogger _logger;
        private readonly ICommonSearch _searchService;
        private readonly ICommonParse _parser;
        private readonly IMemoryCache _memoryCache;
        private readonly IConfiguration _configuration;
        public RankController(ILogger<RankController> logger
            , ICommonSearch searchService
            , ICommonParse parser,
            IMemoryCache memoryCache,
            IConfiguration configuration)
        {
            _logger = logger;
            _searchService = searchService;
            _parser = parser;
            _memoryCache = memoryCache;
            _configuration = configuration;
        }

        [HttpPost]
        [Route("google")]
        
        public ActionResult<string> GetGoogleRank(SearchParameter searchParameter)
        {
            try
            {
                _logger.LogInformation("Entered the get RankController GetGoogleRank function");

                string cachedResult = string.Empty;
                string cacheKey = string.Format("{0}_{1}", "google", searchParameter.KeyWords);
                if (_memoryCache.TryGetValue(cacheKey, out cachedResult))
                {
                    return _memoryCache.Get<string>(cacheKey);
                }

                var searchResults = _searchService.GetGoogleResult(searchParameter.UrlToSearch, searchParameter.KeyWords, searchParameter.NoOfResults);
                var rankings = _parser.GetGooglePageRank(searchResults, searchParameter.UrlToSearch, searchParameter.KeyWords);

                _logger.LogInformation("Executed the get RankController GetGoogleRank function successfully");
                return Ok(rankings);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error in  RankController GetGoogleRank function ");
                throw;
            }
        }
        [HttpPost]
        [Route("bing")]
        public ActionResult<string> GetBingRank(SearchParameter searchParameter)
        {
            try
            {
                _logger.LogInformation("Entered the get RankController GetBingRank function");

                var searchResults = _searchService.GetBingResult(searchParameter.UrlToSearch, searchParameter.KeyWords, searchParameter.NoOfResults);
                var rankings = _parser.GetBingPageRank(searchResults, searchParameter.UrlToSearch);

                _logger.LogInformation("Executed the get RankController GetBingRank function successfully");
                return Ok(rankings);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error in  RankController GetBingRank function ");
                throw;
            }
        }




        [HttpGet]
        [Route("healthcheck")]
        public IActionResult HealthCheck()
        {
            return Ok("api up and runing");

        }


    }
}
