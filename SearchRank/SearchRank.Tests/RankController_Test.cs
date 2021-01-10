using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using SearchRank.Api.Controllers;
using SearchRank.Core.Interface;
using SearchRank.Service;
using System;
using System.IO;
using Xunit;

namespace SearchRank.Tests
{
    public class RankController_Test
    {
        [Fact]
        public void GoogleRank_HappyFlow()
        {
            // Arrange
            var mockService = new Mock<ICommonSearch>();
            mockService.Setup(service => service.GetGoogleResult("www.shavershop.com.au", "best men's shaver australia", "5"))
                .Returns(GetGoogleResults());
            var logger = Mock.Of<ILogger<RankController>>();
            var parserLogger = Mock.Of<ILogger<ResultParser>>();
            var configuration = TestHelper.GetApiConfig();
            var googleCache = TestHelper.GetGoogleCache();
            var parser = new ResultParser(configuration, parserLogger, googleCache);
            var searchParam = TestHelper.GetSearchParam();
         
            var controller = new RankController(logger, mockService.Object, parser,  googleCache, configuration);
            // Act
            var result = controller.GetGoogleRank(searchParam);

            //Assert
            Assert.Equal("5", ((ObjectResult)result.Result).Value);
        }

        [Fact]
        public void BingRank_HappyFlow()
        {
            // Arrange
            var mockService = new Mock<ICommonSearch>();
            mockService.Setup(service => service.GetBingResult("www.anz.com.au", "e-settlements", "10"))
                .Returns(GetBingResults());
            var logger = Mock.Of<ILogger<RankController>>();
            var parserLogger = Mock.Of<ILogger<ResultParser>>();
            var configuration = TestHelper.GetApiConfig();
            var cache = TestHelper.GetGoogleCache();
            var parser = new ResultParser(configuration, parserLogger, cache);
            var searchParam = TestHelper.GetBingParam();
            var controller = new RankController(logger, mockService.Object, parser, cache, configuration);
            // Act
            var result = controller.GetBingRank(searchParam);

            //Assert
            Assert.Equal("6", ((ObjectResult)result.Result).Value);
        }

        private string GetGoogleResults()
        {
            var htmlFile = File.ReadAllText(@"MockHtml\MensShaverJan08.html");
            return htmlFile;
        }

        private string GetBingResults()
        {
            var htmlFile = File.ReadAllText(@"MockHtml\Binge-Settlements10.html");
            return htmlFile;
        }
    }
}
