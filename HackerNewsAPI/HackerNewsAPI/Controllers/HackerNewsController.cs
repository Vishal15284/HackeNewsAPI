using HackerNews.Server.Models;
using HackerNews.Server.Services;
using HackerNewsAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using System.Xml.Linq;

namespace HackerNews.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HackerNewsController : ControllerBase
    {
        private IHackerNews _hackerNews;
        private IMemoryCache _memoryCache;
        public HackerNewsController(IHackerNews hackerNews, IMemoryCache memoryCache)
        {
            _hackerNews = hackerNews;
            _memoryCache = memoryCache;
        }
        /// <summary>
        /// To get 200 hacker news stories
        /// </summary>
        /// <param name="searchInput"></param>
        /// <response code="200">Returns the hacker news stories</response>
        /// <response code="500">Server Error</response>
        /// <returns>ResponseMessage</returns>
        [HttpGet]
        [Route("GetStories")]
        [ProducesResponseType<List<StoryDto>>(StatusCodes.Status200OK)]
        public async Task<IActionResult> Get(string searchInput = "")
        {
            List<StoryDto> hackerStories = new List<StoryDto>();
            try
            {
                if (!_memoryCache.TryGetValue("hackerNewsList", out hackerStories))
                {
                    var stories = await _hackerNews.GetNewestStories();
                    if (stories.Status)
                    {
                        hackerStories = ((List<StoryDto>)stories.Result).Take(200).ToList();
                        var cacheEntryOptions = new MemoryCacheEntryOptions()
                            .SetSlidingExpiration(TimeSpan.FromSeconds(60))
                            .SetAbsoluteExpiration(TimeSpan.FromSeconds(3600))
                            .SetPriority(CacheItemPriority.Normal)
                            .SetSize(1024);
                        _memoryCache.Set("hackerNewsList", hackerStories, cacheEntryOptions);
                    }
                   
                }
                //filter records
                if (!string.IsNullOrEmpty(searchInput.Trim()))
                {
                    hackerStories = hackerStories.Where(x => x.Title.ToLower().Contains(searchInput.Trim().ToLower())).ToList();
                }
            }
            catch (Exception ex)
            {
                
            }

            return Ok(hackerStories);

        }
    }
}
