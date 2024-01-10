using HackerNews.Server.Controllers;
using HackerNews.Server.Models;
using HackerNews.Server.Services;
using HackerNewsAPI.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Moq;
using Moq.Protected;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HackerNews.Tests.Controllers
{
    public class HackerNewsControllerTest
    {
        public HackerNewsControllerTest()
        {

        }

        [Fact]
        public async void HackerNewsController_GetNewestStories_ZeroReturn()
        {
            Mock<IHackerNews> _mockHackerNews = new Mock<IHackerNews>();
            var mockMemoryCache = new Mock<IMemoryCache>();
            mockMemoryCache
                            .Setup(x => x.CreateEntry(It.IsAny<object>()))
                            .Returns(Mock.Of<ICacheEntry>);
            HackerNewsController _hackerNewsController = new HackerNewsController(_mockHackerNews.Object, mockMemoryCache.Object);
            var emptyList = new List<StoryDto>();
            _mockHackerNews.Setup(x => x.GetNewestStories()).ReturnsAsync(new ResponseMessage() { Status=true, Result=emptyList});

            var stories = await _hackerNewsController.Get();
            emptyList = (stories as OkObjectResult).Value as List<StoryDto>;

            Assert.NotNull(emptyList);

        }

        [Fact]
        public async void HackerNewsController_GetNewestStories_SingleList()
        {
            Mock<IHackerNews> _mockHackerNews = new Mock<IHackerNews>();
            var mockMemoryCache = new Mock<IMemoryCache>();
            mockMemoryCache
                            .Setup(x => x.CreateEntry(It.IsAny<object>()))
                            .Returns(Mock.Of<ICacheEntry>);
            HackerNewsController _hackerNewsController = new HackerNewsController(_mockHackerNews.Object, mockMemoryCache.Object);
            var emptyList = new List<StoryDto>();
            emptyList.Add(new StoryDto());
            _mockHackerNews.Setup(x => x.GetNewestStories()).ReturnsAsync(new ResponseMessage() { Status=true,Result=emptyList});

            var stories = await _hackerNewsController.Get();
            var actual = (stories as OkObjectResult).Value as List<StoryDto>;

            Assert.NotNull(actual);
            Assert.Equal(1, actual.Count);

        }

    }
}
