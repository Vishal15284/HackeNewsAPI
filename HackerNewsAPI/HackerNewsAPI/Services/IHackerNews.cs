using HackerNews.Server.Models;
using HackerNewsAPI.Models;

namespace HackerNews.Server.Services
{
    public interface IHackerNews
    {
        Task<ResponseMessage> NewestStories();
        Task<StoryDto> GetStoryByID(int id);
        Task<ResponseMessage> GetNewestStories();
    }
}
