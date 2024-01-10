using HackerNews.Server.Models;
using HackerNews.Server.Services;
using HackerNews.Tests.ApiMockData;
using HackerNews.Tests.helper;
using HackerNewsAPI.Models;
using Moq;
using Moq.Protected;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HackerNews.Tests.Services
{
    public class HackerNewsServicesTest
    {
        public HackerNewsServicesTest() {
        }

        [Fact]
        public async Task HackerNewsServices_NewestStories_Result()
        {
            var mockData = StoriesDetails.Stories;
            var mockHandler = HttpClientHelper.GetResults<List<int>>(mockData);
            var httpClient = new HttpClient(mockHandler.Object);
            var hackerNewsServices = new HackerNewsServices(httpClient);

            var actualResult = await hackerNewsServices.NewestStories();


            Assert.NotNull(actualResult);
            Assert.Equal(1, ((List<int>)actualResult.Result).Count);

            mockHandler.Protected().Verify("SendAsync", Times.Exactly(1), ItExpr.Is<HttpRequestMessage>(
                req => req.Method == HttpMethod.Get && req.RequestUri == new Uri("https://hacker-news.firebaseio.com/v0/newstories.json?print=pretty")
                ),
                ItExpr.IsAny<CancellationToken>());
            
        }
        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public async Task HackerNewsServices_GetStoryByID_Result(int id)
        {
            var mockData = StoriesDetails.Get();
            var mockHandler = HttpClientHelper.GetResults<StoryDto>(mockData);
            var httpClient = new HttpClient(mockHandler.Object);
            var hackerNewsServices = new HackerNewsServices(httpClient);

            var result = await hackerNewsServices.GetStoryByID(id);
            var expectedValue = new StoryDto() {
                Id = 1,
                Score = 1,
                By = "stevefan1999",
                Time = 1704439734,
                Title = "Microsoft Announces AppCAT: Simplifying Azure Migration for .NET Apps",
                Type = "story",
                Url = "https://www.infoq.com/news/2024/01/appcat-azure-dotnet/",
                Descendants = 0
            };

            Assert.NotNull(result);
            Assert.Equivalent(expectedValue, result);

            mockHandler.Protected().Verify("SendAsync", Times.Exactly(1), ItExpr.Is<HttpRequestMessage>(
                req => req.Method == HttpMethod.Get && req.RequestUri == new Uri(string.Format("https://hacker-news.firebaseio.com/v0/item/{0}.json?print=pretty",id))
                ),
                ItExpr.IsAny<CancellationToken>());

        }

        [Fact]
        public async Task HackerNewsServices_GetNewestStories_Result()
        {
            var _httpClient = new HttpClient();
            var hackerNewsServices = new HackerNewsServices(_httpClient);
            

            var actualResult = await hackerNewsServices.GetNewestStories();


            Assert.NotNull(actualResult);
            Assert.NotEmpty((List<StoryDto>)actualResult.Result);
            Assert.True(((List<StoryDto>)actualResult.Result).Count > 0, "The actualResult was not greater than 0");

        }
    }
}
