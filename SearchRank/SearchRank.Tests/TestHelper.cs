using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SearchRank.Core.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchRank.Tests
{
    public class TestHelper
    {
        public static IConfigurationRoot GetIConfigurationRoot(string outputPath)
        {
            return new ConfigurationBuilder()
                .SetBasePath(outputPath)
                .AddJsonFile("appsettings.json", optional: true)
                .Build();
        }

       public static IConfigurationRoot GetApiConfig ()
        {
            var configuration = new ConfigurationBuilder()
         .AddJsonFile("appsettings.json").Build();
            return configuration;
        }

        public static SearchParameter GetSearchParam()
        {
            return new SearchParameter { KeyWords = "best men's shaver australia",
                NoOfResults = "5", 
                SearchEngine = "google", 
                UrlToSearch = "www.shavershop.com.au"
            };
        }
        public static SearchParameter GetBingParam()
        {
            return new SearchParameter
            {
                KeyWords = "e-settlements",
                NoOfResults = "10",
                SearchEngine = "bing",
                UrlToSearch = "www.anz.com.au"
            };
        }

        public static IMemoryCache GetGoogleCache()
        {
            var services = new ServiceCollection();
            services.AddMemoryCache();
            var serviceProvider = services.BuildServiceProvider();

            var memoryCache = serviceProvider.GetService<IMemoryCache>();
            return  memoryCache;
        }
    }


}

