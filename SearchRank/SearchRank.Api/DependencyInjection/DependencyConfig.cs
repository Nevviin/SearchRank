using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SearchRank.Core.Interface;
using SearchRank.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SearchRank.Api.DependencyInjection
{
    public static class DependencyConfig
    {
        public static IServiceCollection SearchConfig(this IServiceCollection services, IConfiguration configuration)
        {
            var shippingDbConnection = configuration.GetConnectionString("ShippingDbConnection");
            services.AddScoped<ICommonSearch, CustomSearch>();
            services.AddScoped<ICommonParse, ResultParser>();
            return services;
        }
    }
}

